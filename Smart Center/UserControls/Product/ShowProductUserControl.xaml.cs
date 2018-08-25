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

namespace Smart_Center.UserControls.Product
{
    /// <summary>
    /// Interaction logic for ShowProductUserControl.xaml
    /// </summary>
    /// 


   

    public partial class ShowProductUserControl : UserControl
    {
        List<Models.Product> Products = null;
        public Purchases.AddPurchesUserControl parent = null;
        SmartDbContext  smartDb;

        public ShowProductUserControl()
        {
            InitializeComponent();
            smartDb = new SmartDbContext();
        }


        void load()
        {
            Products = smartDb.Products.Include(x => x.Category).ToList();
            ProductVG.ItemsSource = Products;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            load();
            Keyboard.Focus(parent.txtQte);
        }
         
        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if (ProductVG.SelectedItem != null)
            {
                Models.Product pr = (Models.Product)ProductVG.SelectedItem;
                parent.Product =pr ;
                parent.txtProductBarcode.Text = pr.Barcode;
                parent.txtProductName.Text = pr.Name;
                parent.txtProductSalesPrice.Text = pr.sales_price.ToString();
                parent.txtProductByePrice.Text = pr.bye_price.ToString();
                parent.txtProductSalesPriceMulti.Text = pr.sales_price_multi.ToString();
                parent.txtQte.Text = "0";
                parent.selectedImeI = new List<IMEI>();
                if (pr.isHaveIMEI)
                {
                    parent.ImeiPanel.Visibility = Visibility.Visible;
                    parent.txtQte.IsReadOnly = true;
                    parent.txtQte.Focus();
                    parent.txtQte.Select(0,parent.txtQte.Text.Length);
                }
                else
                {
                    parent.ImeiPanel.Visibility = Visibility.Hidden;
                    parent.txtQte.IsReadOnly = false;
                    parent.txtQte.Focus();
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

        private void onSearchProduct(object sender, TextChangedEventArgs e)
        {
            Products = smartDb.Products.Where(x=>x.Name.Contains(txtSearch.Text)).Include(x => x.Category).ToList();
            ProductVG.ItemsSource = Products;
        }
    }
}
