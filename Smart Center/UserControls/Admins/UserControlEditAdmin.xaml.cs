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
using BespokeFusion;


namespace Smart_Center.UserControls.Admins
{
    /// <summary>
    /// Interaction logic for UserControlEditAdmin.xaml
    /// </summary>
    public partial class UserControlEditAdmin : UserControl
    {
        public UserControlAdmin parent = null;
        public Admin admin = null;
        public UserControlEditAdmin()
        {
            InitializeComponent();
           
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtName.Text == "")
                error += "ناڤێ یوزه‌ری پێتڤیه‌\n";
            if (txtUsername.Text == "")
                error += "نا‌ڤێ به‌كارهێنه‌ری پێتڤیه‌\n";
            if (txtPassword.Text == "")
                error += " ژمارا نهێنی پێتڤیه‌\n";
            if (cmbType.SelectedIndex == -1)
                error += "جورێ ئه‌دمێنی پێتڤی \n";
            if (error == "")
            {
                admin.Name = txtName.Text;
                admin.Username = txtUsername.Text;
                admin.Password = txtPassword.Text;
                admin.Type = cmbType.SelectedIndex==0 ? Models.Type.FullAdmin : Models.Type.NormalAdmin;
               

                SmartDbContext db = new SmartDbContext();
                db.Admins.Update(admin);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.updateDailog.IsOpen = false;
                    parent.loadData();
                }
                catch (Exception)
                {
                    MaterialMessageBox.ShowError("ناڤ به‌كارهێنه‌ری یێ  هه‌ی دوباره‌");
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
            clearData();

        }

        private void clearData()
        {
            txtName.Text = "";
            txtPassword.Text = "";
            txtUsername.Text = "";
            cmbType.SelectedIndex = -1;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (admin!=null)
            {
                txtName.Text = admin.Name;
                txtPassword.Text = admin.Password;
                txtUsername.Text = admin.Username;
                cmbType.SelectedIndex = admin.Type == Models.Type.FullAdmin ? 0 : 1;
            }
           
        }
    }
}
