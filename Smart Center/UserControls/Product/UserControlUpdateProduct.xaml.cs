using BespokeFusion;
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
    /// Interaction logic for UserControlUpdateProduct.xaml
    /// </summary>
    public partial class UserControlUpdateProduct : UserControl
    {
        public UserControlProduct parent;
        SmartDbContext db;
        List<Category> cats;
        public Models.Product product = null;

        public UserControlUpdateProduct()
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
            if (txtSalesPrice.Text == "")
                error += "بهایێ كرینێ پێتڤیه‌\n";
            if (txtByePrice.Text == "")
                error += "بهایێ فروتنێ پێتڤیه‌\n";
            if (cmbColor.SelectedIndex == -1)
                error += "ره‌نگ پێتڤی \n";



            if (error == "")
            {

                product.Name = txtName.Text;
                product.categoryId = cats[cmbCategory.SelectedIndex].Id;
                product.Company = cmbCompany.Text;
                product.bye_price = float.Parse(txtByePrice.Text.ToString());
                product.sales_price = float.Parse(txtSalesPrice.Text.ToString());
                product.Color = cmbColor.Text;
                product.Disc = txtDisc.Text;
                product.isFavorate = (bool)isFavorate.IsChecked;
                product.isHaveIMEI = (bool)isHaveIMEI.IsChecked;
                

                db.Products.Update(product);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.updateDailog.IsOpen = false;
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
            parent.updateDailog.IsOpen = false;
            this.clearData();
        }

        private void onProductLoad(object sender, RoutedEventArgs e)
        {
            cats = db.Categories.ToList();
            cmbCategory.ItemsSource = cats;

            int index =  cats.FindIndex(c=>c.Id==product.categoryId);
           

            txtName.Text = product.Name;
            cmbCategory.SelectedIndex = index;
            txtBarcode.Text = product.Barcode;
            txtByePrice.Text = product.bye_price.ToString();
            txtSalesPrice.Text = product.sales_price.ToString();
            txtDisc.Text = product.Disc;
            cmbColor.Text = product.Color;
            cmbCompany.Text = product.Company;
            isFavorate.IsChecked = product.isFavorate;
            isHaveIMEI.IsChecked = product.isHaveIMEI;

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
