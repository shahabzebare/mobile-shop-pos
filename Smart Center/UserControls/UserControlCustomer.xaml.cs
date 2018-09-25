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
    /// Interaction logic for UserControlCustomer.xaml
    /// </summary>
    public partial class UserControlCustomer : UserControl
    {
        public MainWindow MainWindow = null;
        UserControls.Cutomers.UpdateCustomerUserControl up = null;
        List<Customer> companies = null;
        SmartDbContext smartDb;
        public UserControlCustomer()
        {
            InitializeComponent();
            smartDb = new SmartDbContext();
        }

        public void loadData()
        {
            companies = smartDb.Customers.ToList();
            CustomerVG.ItemsSource = companies;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();

            UserControls.Cutomers.CreateCutomerUserControl ch = new UserControls.Cutomers.CreateCutomerUserControl();
            ch.parent = this;
            contentOfCreateDilog.Children.Add(ch);

            up = new Cutomers.UpdateCustomerUserControl();
            up.parent = this;
            contentOfUpdateDilog.Children.Add(up);
        }

        private void onShowDebtClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            Cutomers.CustomerDebtsUserControl x = new Cutomers.CustomerDebtsUserControl();
            x.MainWindow = this.MainWindow;
            x.customer = (CustomerVG.SelectedItem as Customer);
            MainWindow.GridMain.Children.Add(x);

        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

            if (result.ToString() == "OK")
            {
                Customer company = (CustomerVG.SelectedItem as Customer);
                smartDb.Remove(company);
                smartDb.SaveChanges();
                loadData();
            }
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            CreateDialog.IsOpen = true;
        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {
            Customer company = (CustomerVG.SelectedItem as Customer);
            up.company = company;
            updateDailog.IsOpen = true;
        }
    }
}
