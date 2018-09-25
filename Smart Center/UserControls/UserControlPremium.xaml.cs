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

namespace Smart_Center.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlPremium.xaml
    /// </summary>
    public partial class UserControlPremium : UserControl
    {
        public MainWindow MainWindow = null;
        List<Models.Premiums> Premiums = null;
        Models.SmartDbContext smartDb;
        public UserControlPremium()
        {
            InitializeComponent();
            smartDb = new Models.SmartDbContext();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
  
            dtStart.Text = DateTime.Now.ToString();
            dtEnd.Text = DateTime.Now.ToString();
            loadData();
        }

        public void loadData()
        {
            Premiums = smartDb.Premiums.Where(x => x.Prem_time.Date >= dtStart.SelectedDate.Value.Date && x.Prem_time.Date <= dtEnd.SelectedDate.Value.Date).Include(l =>l.Customer).ToList();
            OrderGV.ItemsSource = Premiums;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Premium.AddPremiumUserControl x = new Premium.AddPremiumUserControl();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void onPrint(object sender, RoutedEventArgs e)
        {
         
        }

        private void onShowClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Premium.ShowPreUserControl x = new Premium.ShowPreUserControl();
            x.MainWindow = this.MainWindow;
            x.Premiums = (OrderGV.CurrentItem as Models.Premiums);
            MainWindow.GridMain.Children.Add(x);
        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

            if (result.ToString() == "OK")
            {
                Models.Premiums premiums = (OrderGV.SelectedItem as Models.Premiums);
                List<Models.PremiumsDetail> premiumsDetails = smartDb.PremiumsDetails.Where(x => x.PremiumID == premiums.Id).ToList();
                float qtech = premiumsDetails.Sum(x => x.price_pay);
                if (qtech<=0)
                {
                    foreach (Models.PremiumsDetail item in premiumsDetails)
                    {
                        smartDb.PremiumsDetails.Remove(item);
                    }
                    smartDb.Remove(premiums);
                    smartDb.SaveChanges();
                    loadData();
                }
                else
                {
                    MaterialMessageBox.ShowError("ناتوانی ئه‌مه‌ بسریته‌وه‌!");
                }
            }
        }
    }
}
