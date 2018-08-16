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

namespace Smart_Center.UserControls.Admins
{
    /// <summary>
    /// Interaction logic for UserControlCreateAdmin.xaml
    /// </summary>
    public partial class UserControlCreateAdmin : UserControl
    {
        public  UserControlAdmin parent = null;
        public UserControlCreateAdmin()
        {
            InitializeComponent();
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            
            parent.CreateDialog.IsOpen = false;
            clearData();
        }

        private void clearData()
        {
            txtName.Text = "";
            txtPassword.Text = "";
            txtUsername.Text = "";
            cmbType.SelectedIndex = -1;
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtName.Text == "")
                error += "ناڤێ یوزه‌ری پێتڤیه‌\n";
            if(txtUsername.Text == "")
                error += "نا‌ڤێ به‌كارهێنه‌ری پێتڤیه‌\n";
            if (txtPassword.Text == "")
                error += " ژمارا نهێنی پێتڤیه‌\n";
            if (cmbType.SelectedIndex == -1)
                error += "جورێ ئه‌دمێنی پێتڤی \n";
            if (error == "")
            {
                Admin admin = new Admin()
                {
                    Name = txtName.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Type = cmbType.SelectedIndex==0 ? Models.Type.FullAdmin : Models.Type.NormalAdmin,
                };

                SmartDbContext db = new SmartDbContext();
                db.Admins.Add(admin);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.CreateDialog.IsOpen = false;
                    parent.loadData();
                }
                catch(Exception)
                {
                    MaterialMessageBox.ShowError("ناڤ به‌كارهێنه‌ری یێ  هه‌ی دوباره‌");
                }
                
                
            }else
            {
                MaterialMessageBox.ShowError(error);
            }

        }
    }
}
