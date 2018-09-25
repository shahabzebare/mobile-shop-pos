using BespokeFusion;
using Microsoft.EntityFrameworkCore;
using Smart_Center.Models;
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

namespace Smart_Center.UserControls.Cutomers
{
    /// <summary>
    /// Interaction logic for CustomerDebtsUserControl.xaml
    /// </summary>
    public partial class CustomerDebtsUserControl : UserControl
    {
        public MainWindow MainWindow = null;
        public Customer customer = null;
        SmartDbContext dbContext = new SmartDbContext();
        List<MyDebt> Debts = new List<MyDebt>();
        List<MyDebtPay> debtPays = new List<MyDebtPay>();

        public CustomerDebtsUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = customer.Name;
            txtPhone.Text = customer.Phone;
            LoadDebt();
            LoadDebtsPay();
            CompanyVG.ItemsSource = this.Debts;
            calc();
        }

        private void LoadDebt()
        {
            Debts = dbContext.myDebts.Where(x => x.Customer_Id == customer.Id).
                Include(x => x.Order).ThenInclude(x => x.OrderDetails).ToList();
        }

        public void LoadDebtsPay()
        {
            debtPays = dbContext.MyDebtPays.Where(x => x.Customer_id == customer.Id).ToList();
            CompanyPayGV.ItemsSource = this.debtPays;
        }

        public void calc()
        {
            float totalDebts = Debts.Sum(x => x.Rem);
            float totalpay = debtPays.Sum(x => x.Pay);
            txtTotalDebts.Text = (totalDebts - totalpay).ToString();
            txtCountDebts.Text = Debts.Count.ToString();
            txtCountPay.Text = debtPays.Count.ToString();
        }


        private void payLoanClick(object sender, RoutedEventArgs e)
        {
            ContentPayDilog.Children.Clear();
            DebtPayUserControl ch = new DebtPayUserControl();
            ch.parent = this;
            ch.txtRem.Text = ch.txtDebts.Text = txtTotalDebts.Text;

            ContentPayDilog.Children.Add(ch);
            PayDilog.IsOpen = true;
        }

        private void deletDebts(object sender, RoutedEventArgs e)
        {
            if (CompanyPayGV.SelectedItem == null)
            {
                MaterialMessageBox.ShowError("دانا ده‌ینی هه‌لبژێره‌");
            }
            else
            {
                MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

                if (result.ToString() == "OK")
                {
                    MyDebtPay d = CompanyPayGV.SelectedItem as MyDebtPay;
                    dbContext.MyDebtPays.Remove(d);
                    try
                    {
                        dbContext.SaveChanges();
                        LoadDebtsPay();
                        calc();
                    }
                    catch (Exception)
                    {
                        MaterialMessageBox.Show("Error");
                    }
                }
            }
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlCustomer x = new UserControlCustomer();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }
    }


    public class CalcOrderTotal : IValueConverter
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
}
