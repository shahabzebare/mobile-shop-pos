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
    /// Interaction logic for UserControlAdmin.xaml
    /// </summary>
    public partial class UserControlAdmin : UserControl
    {
        List<Admin> Admins = null;
        SmartDbContext smarrtDb = new SmartDbContext();
        UserControls.Admins.UserControlEditAdmin up;
        public UserControlAdmin()
        {
            InitializeComponent();
          

        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟","سرینه‌وه‌");
            
            if (result.ToString()=="OK")
            {
                Admin admin = (AdminGV.SelectedItem as Admin);
                smarrtDb.Remove(admin);
                smarrtDb.SaveChanges();
                loadData();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            loadData();

            UserControls.Admins.UserControlCreateAdmin ch = new UserControls.Admins.UserControlCreateAdmin();
            ch.parent = this;
            contentOfCreateDilog.Children.Add(ch);

              up  = new UserControls.Admins.UserControlEditAdmin();
            up.parent = this;
            contentOfUpdateDilog.Children.Add(up);
        }

       public void loadData()
        {
            Admins = smarrtDb.Admins.ToList();

            AdminGV.ItemsSource = Admins;
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            CreateDialog.IsOpen = true;
        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {
            Admin admin = (AdminGV.SelectedItem as Admin);
            up.admin = admin;
            updateDailog.IsOpen = true;
        }
    }
}
