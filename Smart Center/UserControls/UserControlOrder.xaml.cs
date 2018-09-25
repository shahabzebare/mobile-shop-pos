using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Smart_Center.Models;

namespace Smart_Center.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlOrder.xaml
    /// </summary>
    public partial class UserControlOrder : UserControl
    {
        SmartDbContext SmartDb = new SmartDbContext();
        List<Order> orders = null;
        public MainWindow MainWindow = null;
        public UserControlOrder()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtStart.Text = DateTime.Now.ToString();
            dtEnd.Text = DateTime.Now.ToString();
            loadData();
        }

        public void loadData()
        {
            orders = SmartDb.Orders.Where(x => x.Date.Date >= dtStart.SelectedDate.Value.Date && x.Date.Date <= dtEnd.SelectedDate.Value.Date).Include(x => x.Customer).Include(x => x.OrderDetails).Include(c=>c.myDebts).ToList();
            OrderGV.ItemsSource = orders;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void onShowClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Orders.ShowOrderDeatailUserControl x = new Orders.ShowOrderDeatailUserControl();
            x.MainWindow = this.MainWindow;
            x.Order = OrderGV.CurrentItem as Order;
            MainWindow.GridMain.Children.Add(x);
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Orders.AddOrderUserControl x = new Orders.AddOrderUserControl();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void onPrint(object sender, RoutedEventArgs e)
        {
            Report.Report rwindow = new Report.Report();

            Report.CrystalReportOrder crystalReportOrder = new Report.CrystalReportOrder();
            int orderId = (OrderGV.CurrentItem as Order).Id;
            crystalReportOrder.SetDataSource(SmartDb.OrderDetails.Where(x => x.OrderId == orderId).
                Select(p => new {
                    OrderId = orderId,
                    OrderDate = p.Order.Date.ToShortDateString(),
                    OrderCustomerName=p.Order.Customer.Name,
                    OrderCustomerPhone=p.Order.Customer.Phone,
                    OrderProductName=p.Product.Name,
                    OrderQte=p.Qte,
                    OrderPrice=p.SalesPrice,
                    OrderTotal=p.TotalAmount,
                    OrderTotalAmount = p.Order.OrderDetails.Sum(x=>x.TotalAmount),
                    OrderDebt = p.Order.myDebts.Sum(x=>x.Pay),
                    OrderRem = p.Order.myDebts.Sum(x=>x.Rem)>0? p.Order.myDebts.Sum(x => x.Rem) : p.Order.OrderDetails.Sum(x => x.TotalAmount),
                }).ToList());

            rwindow.crystalReportViewer1.ReportSource = crystalReportOrder;

            rwindow.Show();
        }
    }

    public class CalcOrderTotal : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                ICollection<Models.OrderDetail> d = (ICollection<Models.OrderDetail>)value;

                return d.Sum(x => x.TotalAmount);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CalcOrdersPay : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                Models.Order x = (Models.Order)value;
                if (x.myDebts == null)
                {
                    return x.OrderDetails.Sum(d => d.TotalAmount);
                }
                else
                {
                    return x.myDebts.Sum(d => d.Pay);
                }
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class CalcOrdersRem : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                Models.Order x = (Models.Order)value;
                if (x.myDebts == null)
                {
                    return 0;
                }
                else
                {
                    float total = x.OrderDetails.Sum(d => d.TotalAmount);
                    float pay = x.myDebts.Sum(d => d.Pay);
                    return total - pay;
                }
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
