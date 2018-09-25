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
    /// Interaction logic for CustomerForSelectingUserControl.xaml
    /// </summary>
    public partial class CustomerForSelectingUserControl : UserControl
    {
        List<Customer> Customers = null;
        public Orders.AddOrderUserControl parent = null;
        SmartDbContext smartDb;
        public CustomerForSelectingUserControl()
        {
            InitializeComponent();
            smartDb = new SmartDbContext();

        }

        public void loadData()
        {
            Customers = smartDb.Customers.ToList();
            CustomerVG.ItemsSource = Customers;
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void onSearchCompany(object sender, TextChangedEventArgs e)
        {
            Customers = smartDb.Customers.Where(x => x.Name.Contains(txtSearch.Text)).ToList();
            CustomerVG.ItemsSource = Customers;
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if (CustomerVG.SelectedItem != null)
            {
                parent.custmer = (Customer)CustomerVG.SelectedItem;
                parent.txtNameCompany.Text = parent.custmer.Name;
                parent.txtIdCompany.Text = parent.custmer.Id.ToString();
                parent.SelectCompanyDilog.IsOpen = false;
            }
            else
            {
                MaterialMessageBox.ShowError("كریاره‌كێ هه‌لبژێره‌");
            }
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.SelectCompanyDilog.IsOpen = false;
        }
    }
}
