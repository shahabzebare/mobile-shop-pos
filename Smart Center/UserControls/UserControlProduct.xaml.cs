using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
using System.Globalization;

namespace Smart_Center.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlProduct.xaml
    /// </summary>
    public partial class UserControlProduct : UserControl
    {
        Product.UserControlUpdateProduct up = null;
        List<Models.Product> Products = null;
        Models.SmartDbContext smartDb; 
        public UserControlProduct()
        {
            InitializeComponent();
            smartDb = new Models.SmartDbContext();
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            CreateDialog.IsOpen = true;
        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

            if (result.ToString() == "OK")
            {
                Models.Product product = (ProductVG.SelectedItem as Models.Product);
                smartDb.Remove(product);
                smartDb.SaveChanges();
                loadData();
            }
        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {
            Models.Product pr = (ProductVG.SelectedItem as Models.Product);
            up.product = pr;
            updateDailog.IsOpen = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();

            UserControls.Product.UserControlCreateProduct ch = new UserControls.Product.UserControlCreateProduct();
            ch.parent = this;
            contentOfCreateDilog.Children.Add(ch);

            up = new UserControls.Product.UserControlUpdateProduct();
            up.parent = this;
            contentOfUpdateDilog.Children.Add(up);
        }

        public void loadData()
        {
            Products = smartDb.Products.Include(x=>x.Category).Include(x=>x.PurchasDetails).ToList();
            ProductVG.ItemsSource = Products;
        }
    }

    public class CalcCountProduct : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return 0;
            }
            else
            {
                Models.Product x = (Models.Product)value;
                return x.PurchasDetails.Sum(j=>j.QteCh);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
