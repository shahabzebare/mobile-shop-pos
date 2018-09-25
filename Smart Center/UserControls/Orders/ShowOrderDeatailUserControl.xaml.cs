using Microsoft.EntityFrameworkCore;
using Smart_Center.Models;
using System;
using System.Collections.Generic;
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

namespace Smart_Center.UserControls.Orders
{
    /// <summary>
    /// Interaction logic for ShowOrderDeatailUserControl.xaml
    /// </summary>
    public partial class ShowOrderDeatailUserControl : UserControl
    {
        public MainWindow MainWindow = null;
        public Order Order = null;

        public ShowOrderDeatailUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Order != null)
            {
                txtIdCompany.Text = Order.CustomerId.ToString();
                txtNameCompany.Text = Order.Customer.Name;
                txtDisc.Text = Order.Disc;
                txtPurchesId.Text = Order.Id.ToString();
                dtPurches.Text = Order.Date.ToString();
                SmartDbContext db = new SmartDbContext();
                List<OrderDetail> x = db.OrderDetails.Where(j => j.OrderId == Order.Id).Include(j => j.Product).ToList();
                PurchesGV.ItemsSource = x;

                txtTotalAmoount.Text = x.Sum(j => j.TotalAmount).ToString();
                if (Order.myDebts.Count > 0)
                {
                    txtPayAmount.Text = Order.myDebts.Sum(j => j.Pay).ToString();
                    txtRemAmount.Text = Order.myDebts.Sum(j => j.Rem).ToString();
                    payPanel.Visibility = Visibility.Visible;
                    remPanel.Visibility = Visibility.Visible;
                    isDebats.IsChecked = true;
                }
            }
        }

        private void PurchesGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlOrder x = new UserControlOrder();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }
    }
}
