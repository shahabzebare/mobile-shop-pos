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
    /// Interaction logic for UserControlUpdateCat.xaml
    /// </summary>
    public partial class UserControlUpdateCat : UserControl
    {
        public UserControlCategory parent = null;
        public Category catt = null;
        public UserControlUpdateCat()
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
                catt.Name = txtName.Text;
                SmartDbContext db = new SmartDbContext();
                db.Categories.Update(catt);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.updateDailog.IsOpen = false;
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
            clearData();
            parent.updateDailog.IsOpen = false;
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            if (catt!=null)
            {
                txtName.Text = catt.Name;
            }
        }

        private void clearData()
        {
            txtName.Text = "";
        }
    }
}
