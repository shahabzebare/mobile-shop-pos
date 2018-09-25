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

namespace Smart_Center.UserControls.Product
{
    /// <summary>
    /// Interaction logic for ShowProductSelectOrderUserControl.xaml
    /// </summary>
    public partial class ShowProductSelectOrderUserControl : UserControl
    {
        List<Models.Product> Products = null;
        public Orders.AddOrderUserControl parent = null;
        SmartDbContext smartDb;

        public ShowProductSelectOrderUserControl()
        {
            InitializeComponent();
            smartDb = new SmartDbContext();
        }

        void load()
        {
            Products = smartDb.Products.Include(x=>x.PurchasDetails).Where(x=>x.PurchasDetails.Sum(k=>k.QteCh)>0).ToList();
            ProductVG.ItemsSource = Products;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void onSearchProduct(object sender, TextChangedEventArgs e)
        {
            Products = smartDb.Products.Where(x=>x.Name.Contains(txtSearch.Text)).Include(x => x.PurchasDetails).Where(x => x.PurchasDetails.Sum(k => k.QteCh) > 0).ToList();
            ProductVG.ItemsSource = Products;
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {

            if (ProductVG.SelectedItem != null)
            {
                Models.Product pr = (Models.Product)ProductVG.SelectedItem;
                parent.Product = pr;
                parent.txtProductBarcode.Text = pr.Barcode;
                parent.txtProductName.Text = pr.Name;

                if((bool)parent.isMulti.IsChecked)
                {
                    parent.txtProductSalesPrice.Text = pr.sales_price_multi.ToString();
                }
                else
                    parent.txtProductSalesPrice.Text = pr.sales_price.ToString();

                PurchasDetail purchasDetail = smartDb.PurchasDetail.Where(j=>j.Barcode==pr.Barcode).Where(x => x.QteCh > 0).OrderByDescending(x => x.purchasId).LastOrDefault();

                parent.txtProductByePrice.Text = purchasDetail.ByePrice.ToString();
                
                parent.txtQte.Text = "0";
                if (pr.isHaveIMEI)
                {
                    parent.ImeiPanel.IsEnabled =true;
                    parent.txtQte.IsReadOnly = true;
                    parent.txtQte.Text = "1";
                    parent.txtImei.Focus();
                    parent.txtQte.Select(0, parent.txtQte.Text.Length);
                }
                else
                {
                    parent.ImeiPanel.IsEnabled = false;
                    parent.txtQte.IsReadOnly = false;
                    parent.txtDicount.Focus();
                }

                parent.SelectCompanyDilog.IsOpen = false;
            }
            else
            {
                MaterialMessageBox.ShowError("پێناسه‌كێ هه‌لبژێره‌");
            }
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.SelectCompanyDilog.IsOpen = false;
        }
    }


    public class CalcCountProduct : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                Models.Product x = (Models.Product)value;
                return x.PurchasDetails.Sum(j => j.QteCh);
            }
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
