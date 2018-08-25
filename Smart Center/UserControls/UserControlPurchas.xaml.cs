using BespokeFusion;
using Microsoft.EntityFrameworkCore;
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

namespace Smart_Center.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlPurchas.xaml
    /// </summary>
    public partial class UserControlPurchas : UserControl
    {
        public MainWindow MainWindow = null;
        List<Models.Purchas> Purchas = null;
        Models.SmartDbContext smartDb;
        public UserControlPurchas()
        {
            InitializeComponent();
            smartDb = new Models.SmartDbContext();
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Purchases.AddPurchesUserControl x = new Purchases.AddPurchesUserControl();
                x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

            if (result.ToString() == "OK")
            {
                Models.Purchas product = (AdminGV.SelectedItem as Models.Purchas);
                List<Models.PurchasDetail> purchasDetail = smartDb.PurchasDetail.Where(x => x.purchasId == product.Id).Include(x=>x.IMEIs).ToList();
                List<Models.Debt> debts = smartDb.Debts.Where(x => x.Purchas_Id == product.Id).ToList();
                float qte = purchasDetail.Sum(x => x.Qte);
                float qtech = purchasDetail.Sum(x => x.QteCh);
                if (qte == qtech)
                {
                    foreach (Models.PurchasDetail  item in purchasDetail)
                    {
                        smartDb.RemoveRange(item.IMEIs);
                        smartDb.PurchasDetail.Remove(item);
                    }
                    smartDb.Debts.RemoveRange(debts);
                    smartDb.Remove(product);
                    smartDb.SaveChanges();
                    loadData();
                }
                else
                {
                    MaterialMessageBox.ShowError("ناتوانی ئه‌مه‌ بسریته‌وه‌!");
                }
            }
        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtStart.Text = DateTime.Now.ToString();
            dtEnd.Text = DateTime.Now.ToString();
            loadData();
        }

        public void loadData()
        {
            Purchas = smartDb.Purchas.Where(x=>x.Date.Date>=dtStart.SelectedDate.Value.Date && x.Date.Date<=dtEnd.SelectedDate.Value.Date).Include(x =>x.Company).Include(x=>x.purchasDetails).Include(x=>x.debt).ToList();
            AdminGV.ItemsSource = Purchas;
        }

        private void onShowClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Purchases.ShowPurchesDetailsUserControl x = new Purchases.ShowPurchesDetailsUserControl();
            x.MainWindow = this.MainWindow;
            x.Purchas = AdminGV.CurrentItem as Models.Purchas;
            MainWindow.GridMain.Children.Add(x);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }

    public class CalcPurchesTotal : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                ICollection<Models.PurchasDetail> d = (ICollection<Models.PurchasDetail>)value;

                return d.Sum(x => x.TotalAmount);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CalcPurchesPay : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                Models.Purchas x = (Models.Purchas)value;
                if(x.debt==null)
                {
                    return x.purchasDetails.Sum(d => d.TotalAmount);
                }
                else
                {
                    return x.debt.Sum(d => d.Pay);
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class CalcPurchesRem : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                Models.Purchas x = (Models.Purchas)value;
                if (x.debt == null)
                {
                    return 0;
                }
                else
                {
                    float total = x.purchasDetails.Sum(d => d.TotalAmount);
                    float pay =  x.debt.Sum(d => d.Pay);
                    return total - pay;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}


