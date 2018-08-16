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
    /// Interaction logic for UserControlCompany.xaml
    /// </summary>
    public partial class UserControlCompany : UserControl
    {
        UserControls.Companies.UserControlUpdateCompany up = null;
        List<Company> companies = null;
        SmartDbContext smartDb;
        public UserControlCompany()
        {
            InitializeComponent();
            smartDb = new SmartDbContext();
        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

            if (result.ToString() == "OK")
            {
                Company company = (CompanyVG.SelectedItem as Company);
                smartDb.Remove(company);
                smartDb.SaveChanges();
                loadData();
            }
        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {
           Company company = (CompanyVG.SelectedItem as Company);
            up.company = company;
            updateDailog.IsOpen = true;
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            CreateDialog.IsOpen = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();

            UserControls.Companies.UserControlCreateCompany ch = new UserControls.Companies.UserControlCreateCompany();
            ch.parent = this;
            contentOfCreateDilog.Children.Add(ch);

            up = new Companies.UserControlUpdateCompany();
            up.parent = this;
            contentOfUpdateDilog.Children.Add(up); 
        }

        public void loadData()
        {
            companies = smartDb.Companies.ToList();
            CompanyVG.ItemsSource = companies;
        }
    }
}
