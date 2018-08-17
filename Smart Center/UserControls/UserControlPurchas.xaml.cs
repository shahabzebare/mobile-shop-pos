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
    /// Interaction logic for UserControlPurchas.xaml
    /// </summary>
    public partial class UserControlPurchas : UserControl
    {
        public MainWindow MainWindow = null;
        public UserControlPurchas()
        {
            InitializeComponent();
        }

        private void onCreateClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            MainWindow.GridMain.Children.Add(new Purchases.AddPurchesUserControl());
        }

        private void onDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void onUpdateClick(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
