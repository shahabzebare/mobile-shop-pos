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

namespace Smart_Center.UserControls.Categories
{
    /// <summary>
    /// Interaction logic for UserControlCreateCat.xaml
    /// </summary>
    public partial class UserControlCreateCat : UserControl
    {
        public UserControlCategory parent = null;

        public UserControlCreateCat()
        {
            InitializeComponent();
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtName.Text == "")
                error += "ناڤێ یوزه‌ری پێتڤیه‌\n";

            if (error == "")
            {
                Category cat = new Category()
                {
                    Name = txtName.Text,
                };

                SmartDbContext db = new SmartDbContext();
                db.Categories.Add(cat);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.CreateDialog.IsOpen = false;
                    parent.loadData();
                }
                catch (Exception)
                {
                    MaterialMessageBox.ShowError("EROOR!!!!!!!!‌");
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
            clearData();
        }

        private void clearData()
        {
            txtName.Text = "";
        }
    }
}
