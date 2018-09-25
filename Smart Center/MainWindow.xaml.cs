using Smart_Center.Models;
using Smart_Center.UserControls;
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

namespace Smart_Center
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SmartDbContext x = new SmartDbContext();
            x.Database.EnsureCreated();
        }


        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemAdmin":
                    usc = new UserControlAdmin();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCategory":
                    usc = new UserControlCategory();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemProduct":
                    usc = new UserControlProduct();
                    GridMain.Children.Add(usc);
                    break;
                    
                case "ItemCompany":
                    usc = new UserControlCompany();
                    UserControlCompany uc = (UserControlCompany)usc;
                    uc.MainWindow = this;
                    GridMain.Children.Add(uc);
                    break;

                case "ItemCutomer":
                    usc = new UserControlCustomer();
                    UserControlCustomer ucc = (UserControlCustomer)usc;
                    ucc.MainWindow = this;
                    GridMain.Children.Add(ucc);
                    break;
                case "ItemPurchas":
                    usc = new UserControlPurchas();
                    UserControlPurchas u = (UserControlPurchas)usc;
                    u.MainWindow = this;
                    GridMain.Children.Add(u);
                    break;

                case "ItemOrder":
                    usc = new UserControlOrder();
                    UserControlOrder uss = (UserControlOrder)usc;
                    uss.MainWindow = this;
                    GridMain.Children.Add(uss);
                    break;
                case "ItemPr":
                    usc = new UserControlPremium();
                    UserControlPremium us_s = (UserControlPremium)usc;
                    us_s.MainWindow = this;
                    GridMain.Children.Add(us_s);
                    break;
                    
                default:
                    break;
            }
        }



        private void onLogoutClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
