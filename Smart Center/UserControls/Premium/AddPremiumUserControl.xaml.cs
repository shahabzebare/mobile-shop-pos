using BespokeFusion;
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

namespace Smart_Center.UserControls.Premium
{
    /// <summary>
    /// Interaction logic for AddPremiumUserControl.xaml
    /// </summary>
    public partial class AddPremiumUserControl : UserControl
    {
        public MainWindow MainWindow = null;
        SmartDbContext dbContext;
        List<PremiumsDetail> premiumsDetails;
        public AddPremiumUserControl()
        {
            InitializeComponent();
            dbContext = new SmartDbContext();
           premiumsDetails = new List<PremiumsDetail>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCustomer.ItemsSource = dbContext.Customers.ToList();
            cmbWork.ItemsSource = dbContext.works.ToList();
            dtFirstPay.SelectedDate = DateTime.Today;
            dtDatePre.SelectedDate = DateTime.Today;
            prDetailGV.ItemsSource = premiumsDetails;
        }

        private void cmbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Customer customer = cmbCustomer.SelectedItem as Customer;
            if (customer != null)
            {
                txtAmount.Text = "0";
                calcAmount();
                List<Order> orders = dbContext.Orders.Where(c => c.CustomerId == customer.Id && c.Premiums.Count()<=0 &&  c.myDebts.Count()>0).Include(j=>j.myDebts).ToList();
                    
                if(orders.Count>0)
                {
                    cmbOrder.IsEnabled = true;
                    cmbOrder.ItemsSource = orders;
                }
                else
                {
                    cmbOrder.ItemsSource = null;
                    cmbOrder.IsEnabled = false;
                   
                }
            }
        }

        private void cmbOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = cmbOrder.SelectedItem as Order;
            if (order != null)
            {

                txtAmount.Text = order.myDebts.Sum(x => x.Rem).ToString();
                calcAmount();
            }
        }

        private void txtEveryPay_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox x = (TextBox)sender;
            string removed = "";
            foreach (var item in x.Text)
            {
                if (item >= '0' && item <= '9')
                    removed += item;
                if (item == '.' && !removed.Contains('.'))
                    removed += item;
            }
            x.Text = removed;
            x.Select(removed.Length, 0);
        }


        void calcAmount()
        {
            try
            {
                float amount = float.Parse(txtAmount.Text.ToString());
                int number = int.Parse(txtPrCount.Text.ToString());
                float last = amount / number;
                txtEveryPay.Text = last.ToString();
                calcTable();
            }
            catch (Exception)
            {
                txtEveryPay.Text = "0";
            }
        }

        void calcTable()
        {
            try
            {
                premiumsDetails.Clear();
                float amount = float.Parse(txtAmount.Text.ToString());
                int number = int.Parse(txtPrCount.Text.ToString());
                DateTime dateTime = (DateTime)dtFirstPay.SelectedDate;
                float eryPay = float.Parse(txtEveryPay.Text);
                float dayes = int.Parse(txtDayPerQst.Text);
                for (int i = 0; i < number; i++)
                {
                    if (i == number - 1)
                    {
                        eryPay = amount - premiumsDetails.Sum(p => p.price);
                    }
                    premiumsDetails.Add(new PremiumsDetail()
                    {
                        payDate = dateTime,
                        price = eryPay,
                        price_pay = 0
                    });
                    if (dayes == 30)
                        dateTime = dateTime.AddMonths(1);
                    else
                        dateTime = dateTime.AddDays(dayes);
                }
                prDetailGV.Items.Refresh();
            }
            catch
            {

            }
        }

        private void txtPrCount_KeyUp(object sender, KeyEventArgs e)
        {
            calcAmount();
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlPremium x = new UserControlPremium();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (cmbCustomer.SelectedItem == null)
            {
                error += "پێتڤیه‌ كریاره‌كێ هه‌لبژێری\n";
            }

            if (cmbOrder.SelectedItem == null)
            {
                error += "پێتڤیه‌ كرینه‌كێ هه‌لبژێری\n";
            }

            if (cmbWork.SelectedItem == null && cmbWork.Text == "")
            {
                error += "پێتڤیه‌ شولی هه‌لبژێری هه‌لبژێری\n";
            }

            if (txtAmount.Text == "0" || txtAmount.Text == "")
            {
                error += "پێتڤیه‌ پاره‌ی ‌ هه‌لبژێری\n";
            }

            if (txtPrCount.Text == "0" || txtPrCount.Text == "")
            {
                error += "پێتڤیه‌ ژمارا قستا هه‌لبژێری\n";
            }

            if (txtDayPerQst.Text == "0" || txtDayPerQst.Text == "")
            {
                error += "پێتڤیه‌ روژێن قستی هه‌لبژێری\n";
            }

            if (txtEveryPay.Text == "0" || txtEveryPay.Text == "")
            {
                error += "پێتڤیه‌ كوژمێ هه‌ر قسته‌كێ بنڤیسی هه‌لبژێری\n";
            }

            if (error != "" || premiumsDetails.Count<=0)
            {
                BespokeFusion.MaterialMessageBox.ShowError(error);
                return;
            }
            Work work = null; 
            if (cmbWork.SelectedItem==null)
            {
               work = new Work(){ Name = cmbWork.Text };
                dbContext.works.Add(work);
                dbContext.SaveChanges();
            }
            else
            {
                work = cmbWork.SelectedItem as Work;
            }


            Premiums premiums = new Premiums()
            {
                Note = txtNote.Text,
                OrderID = (cmbOrder.SelectedItem as Order).Id,
                CustomerID = (cmbCustomer.SelectedItem as Customer).Id,
                 WorkId = work.Id,
                 first_Pay = dtFirstPay.SelectedDate.Value,
                 price = float.Parse(txtAmount.Text),
                 Prem_time = dtDatePre.SelectedDate.Value
            };


            premiums.premiumsDetails = premiumsDetails;

            dbContext.Add<Premiums>(premiums);

            try
            {
                dbContext.SaveChanges();
                MaterialMessageBox.Show("ب سه‌ركه‌فتیانه‌ هاتنه‌ هه‌لگرتن");

                MainWindow.GridMain.Children.Clear();
                UserControlPremium x = new UserControlPremium();
                x.MainWindow = this.MainWindow;
                MainWindow.GridMain.Children.Add(x);
            }
            catch (Exception ex)
            {
                MaterialMessageBox.ShowError(ex.Message);
            }


        }

        private void txtEveryPay_KeyUp(object sender, KeyEventArgs e)
        {
            calcTable();
        }

        private void dtFirstPay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            calcTable();
        }
    }
}
