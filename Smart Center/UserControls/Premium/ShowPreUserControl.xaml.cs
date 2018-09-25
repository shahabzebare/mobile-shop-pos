using BespokeFusion;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ShowPreUserControl.xaml
    /// </summary>
    public partial class ShowPreUserControl : UserControl
    {
        public MainWindow MainWindow = null;
        public Models.Premiums Premiums = null;
        Models.SmartDbContext SmartDb = new Models.SmartDbContext();
        public ShowPreUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Models.Premiums premiums = SmartDb.Premiums.Where(x => x.Id == Premiums.Id).Include(l => l.premiumsDetails).Include(j => j.Customer).Include(k => k.Work).Single();

            txtCustomerName.Text = premiums.Customer.Name;
            txtOrderID.Text = premiums.OrderID.ToString();
            txtWork.Text = premiums.Work.Name;
            txtAmount.Text = premiums.price.ToString() ;
            txtPrCount.Text = premiums.premiumsDetails.Count.ToString();
            prDetailGV.ItemsSource = premiums.premiumsDetails;
           
            dtFirstPay.SelectedDate = premiums.first_Pay;
            dtDatePre.SelectedDate = premiums.Prem_time;
            txtNote.Text = premiums.Note;
            txtEveryPay.Text = premiums.premiumsDetails.First().price.ToString();
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlPremium x = new UserControlPremium();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void onPay(object sender, RoutedEventArgs e)
        {
            Models.PremiumsDetail premiumsDetail = prDetailGV.SelectedItem as Models.PremiumsDetail;
            DateTime dateTime = DateTime.Now;
            if (premiumsDetail.price_pay <= 0)
            {
                if (premiumsDetail.payDate <= dateTime)
                {
                    ContentPayDilog.Children.Clear();
                    PayQstUserControl ch = new PayQstUserControl();
                    ch.parent = this;
                    ch.txtRem.Text = ch.txtDebts.Text = premiumsDetail.price.ToString();
                    ch.PremiumsDetail = premiumsDetail;
                    ContentPayDilog.Children.Add(ch);
                    PayDilog.IsOpen = true;
                }
                else
                {
                    MaterialMessageBox.ShowError("ئه‌ڤ قسته‌ی وه‌ختێ وی نه‌گه‌شتیێ ‌ ");
                }
            }
            else
            {
                MaterialMessageBox.ShowError("ئه‌ڤ قسته‌ یێ هاتیه‌ دان");
            }
        }
    }
}
