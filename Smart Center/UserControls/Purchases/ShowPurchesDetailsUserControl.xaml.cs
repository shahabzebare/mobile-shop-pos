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
using BespokeFusion;
using Microsoft.EntityFrameworkCore;
using Smart_Center.Models;

namespace Smart_Center.UserControls.Purchases
{
    /// <summary>
    /// Interaction logic for ShowPurchesDetailsUserControl.xaml
    /// </summary>
    public partial class ShowPurchesDetailsUserControl : UserControl
    {
        public MainWindow MainWindow = null;
        public Purchas Purchas = null;
        public ShowPurchesDetailsUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Purchas != null)
            {
                txtIdCompany.Text = Purchas.companyId.ToString() ;
                txtNameCompany.Text = Purchas.Company.Name;
                txtDisc.Text = Purchas.Disc;
                txtPurchesId.Text = Purchas.Id.ToString();
                txtPurchesNumber.Text = Purchas.Number;
                dtPurches.Text = Purchas.Date.ToString();
                SmartDbContext db = new SmartDbContext();
                List<PurchasDetail> x = db.PurchasDetail.Where(j => j.purchasId == Purchas.Id).Include(j => j.Product).Include(j=>j.IMEIs).ToList();
                PurchesGV.ItemsSource =x;
               
                txtTotalAmoount.Text = x.Sum(j => j.TotalAmount).ToString();
                if (Purchas.debt.Count>0)
                {
                    txtPayAmount.Text = Purchas.debt.Sum(j => j.Pay).ToString();
                    txtRemAmount.Text = Purchas.debt.Sum(j => j.Rem).ToString();
                    payPanel.Visibility = Visibility.Visible;
                    remPanel.Visibility = Visibility.Visible;
                    isDebats.IsChecked = true;
                }
            }
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlPurchas x = new  UserControlPurchas();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void PurchesGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void onShowImeiClick(object sender, RoutedEventArgs e)
        {
            if (PurchesGV.CurrentItem != null)
            {
                Models.PurchasDetail purchas = PurchesGV.CurrentItem as Models.PurchasDetail;
                if (purchas.IMEIs.Count == 0)
                {
                    MaterialMessageBox.ShowError("ئه‌ڤێ پێناسێ IMEI نینه‌!");
                }
                else
                {
                    ContentSelectCompanyDilog.Children.Clear();
                    UserControls.Purchases.ShowImeiUserControl ch = new UserControls.Purchases.ShowImeiUserControl();
                    ch.parent = this;
                    ch.iMEI = purchas.IMEIs.ToList();
                    ContentSelectCompanyDilog.Children.Add(ch);
                    SelectCompanyDilog.IsOpen = true;
                }
            }
        }
    }
}
