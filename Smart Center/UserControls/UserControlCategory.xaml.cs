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

namespace Smart_Center.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlCategory.xaml
    /// </summary>
    public partial class UserControlCategory : UserControl
    {
        List<Category> Categories = null;
        SmartDbContext smarrtDb = new SmartDbContext();
        UserControls.Categories.UserControlUpdateCat up;

        public UserControlCategory()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {

            loadData();
            UserControls.Categories.UserControlCreateCat ch = new UserControls.Categories.UserControlCreateCat();
            ch.parent = this;
            contentOfCreateDilog.Children.Add(ch);

            up = new UserControls.Categories.UserControlUpdateCat();
            up.parent = this;
            contentOfUpdateDilog.Children.Add(up);

        }

        public void loadData()
        {
            Categories = smarrtDb.Categories.ToList();

            CategoryGV.ItemsSource = Categories;
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
                Category Cat = (CategoryGV.SelectedItem as Category);
                smarrtDb.Remove(Cat);
                smarrtDb.SaveChanges();
                loadData();
            }
        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {
            Category cat = (CategoryGV.SelectedItem as Category);
            up.catt = cat;
            updateDailog.IsOpen = true;
        }
    }
}
