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

namespace Smart_Center.UserControls.Companies
{
    /// <summary>
    /// Interaction logic for UserControlCreateCompany.xaml
    /// </summary>
    public partial class UserControlCreateCompany : UserControl
    {
        public UserControlCompany parent = null;

        public UserControlCreateCompany()
        {
            InitializeComponent();
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtName.Text == "")
                error += "ناڤێ كومپانیێ پێتڤیه‌\n";
            if (txtPhone.Text == "")
                error += "ژمارا ته‌له‌فونێ پێتڤیه‌\n";

            if (error == "")
            {
                Company company = new Company()
                {
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };

                SmartDbContext db = new SmartDbContext();
                db.Companies.Add(company);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.CreateDialog.IsOpen = false;
                    parent.loadData();
                }
                catch (Exception)
                {
                    MaterialMessageBox.ShowError("ژمارا ته‌له‌فونێ یا  هه‌ی دوباره‌");
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
            txtEmail.Text = "";
            txtPhone.Text = "";
        }
    }
}
