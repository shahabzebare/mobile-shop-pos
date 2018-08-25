using BespokeFusion;
using Smart_Center.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserControlCreateProduct.xaml
    /// </summary>
    public partial class UserControlCreateProduct : UserControl
    {
        public UserControlProduct parent;
        SmartDbContext db;
        List<Category> cats;
        public UserControlCreateProduct()
        {
            InitializeComponent();
            db = new SmartDbContext();
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtName.Text == "")
                error += "ناڤ پێتڤیه‌\n";
            if (cmbCategory.SelectedIndex == -1)
                error += "به‌ش پێتڤی \n";
            if (txtBarcode.Text == "")
                error += "باركود پێتڤیه‌\n";
            if (cmbCompany.SelectedIndex == -1)
                error += "كومپانی پێتڤی \n";
            if (txtByePrice.Text == "")
                error += "بهایێ كرینێ پێتڤیه‌\n";
            if (txtSalesPrice.Text == "")
                error += "بهایێ فروتنێ تاك پێتڤیه‌\n";

            if (txtSalesPriceMulti.Text == "")
                error += "بهایێ فروتنێ كو پێتڤیه‌\n";
            if (cmbColor.SelectedIndex == -1)
                error += "ره‌نگ پێتڤی \n";



            if (error == "")
            {
                Models.Product pr = new Models.Product()
                {
                    Name = txtName.Text,
                    Barcode = txtBarcode.Text.Trim(),
                    categoryId = cats[cmbCategory.SelectedIndex].Id,
                    Category = cats[cmbCategory.SelectedIndex],
                    Company = cmbCompany.Text,
                    bye_price = float.Parse(txtByePrice.Text.ToString()),
                    sales_price = float.Parse(txtSalesPrice.Text.ToString()),
                    sales_price_multi = float.Parse(txtSalesPriceMulti.Text.ToString()),
                    Color = cmbColor.Text,
                    Disc = txtDisc.Text,
                    isFavorate = (bool)isFavorate.IsChecked,
                    isHaveIMEI = (bool)isHaveIMEI.IsChecked
                };
               
                db.Products.Add(pr);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.CreateDialog.IsOpen = false;
                    parent.loadData();
                }
                catch (Exception ex)
                {
                    MaterialMessageBox.ShowError("Error!");
                }


            }
            else
            {
                MaterialMessageBox.ShowError(error);
            }
        }



        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.CreateDialog.IsOpen = false;
            this.clearData();   
        }

        private void onProductLoad(object sender, RoutedEventArgs e)
        {
            cats = db.Categories.ToList();
            cmbCategory.ItemsSource = cats;   
        }

        private void clearData()
        {
            txtName.Text = "";
            cmbCategory.SelectedIndex = -1;
            txtBarcode.Text = "";
            txtByePrice.Text = "";
            txtSalesPrice.Text = "";
            txtDisc.Text = "";
            cmbColor.SelectedIndex = -1;
            cmbCompany.SelectedIndex = -1;
            isFavorate.IsChecked = false;
            isHaveIMEI.IsChecked = false;
        }

      

       

        private void onLostFocusBarcode(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtBarcode.Text.Trim() != "")
            {
                Models.Product p = db.Products.Where(x => x.Barcode == txtBarcode.Text.Trim()).FirstOrDefault();
                if (p != null)
                {
                    MaterialMessageBox.ShowError("باركود دوباره‌یه‌ باركوده‌كێ دی داخل بكه‌!");

                    txtBarcode.Focus();
                }
            }
            else
            {
                MaterialMessageBox.ShowError("باركود پێتڤیه");
                txtBarcode.Focus();
            }
        }

      

        private void onlyNumber(object sender, TextChangedEventArgs e)
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
    }
}
