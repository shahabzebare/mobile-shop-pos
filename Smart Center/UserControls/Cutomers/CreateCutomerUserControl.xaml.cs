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
    /// Interaction logic for CreateCutomerUserControl.xaml
    /// </summary>
    public partial class CreateCutomerUserControl : UserControl
    {
        public UserControlCustomer parent = null;

        public CreateCutomerUserControl()
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
                Customer company = new Customer()
                {
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                };

                SmartDbContext db = new SmartDbContext();
                db.Customers.Add(company);
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

        private void clearData()
        {
            txtName.Text = "";
            txtPhone.Text = "";
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.CreateDialog.IsOpen = false;
        }
    }
}
