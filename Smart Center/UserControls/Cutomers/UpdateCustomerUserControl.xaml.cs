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

namespace Smart_Center.UserControls.Cutomers
{
    /// <summary>
    /// Interaction logic for UpdateCustomerUserControl.xaml
    /// </summary>
    public partial class UpdateCustomerUserControl : UserControl
    {
        public UserControlCustomer parent = null;
        public Customer company = null;
        public UpdateCustomerUserControl()
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

                company.Name = txtName.Text;
                company.Phone = txtPhone.Text;


                SmartDbContext db = new SmartDbContext();
                db.Customers.Update(company);
                try
                {
                    db.SaveChanges();
                    clearData();
                    parent.updateDailog.IsOpen = false;
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

        private void clearData()
        {
            txtName.Text = "";
            txtPhone.Text = "";
        }


        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.updateDailog.IsOpen = false;
            clearData();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            if (company != null)
            {
                txtName.Text = company.Name;
                txtPhone.Text = company.Phone;
            }
        }
    }
}
