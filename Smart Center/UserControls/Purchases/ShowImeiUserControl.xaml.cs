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

namespace Smart_Center.UserControls.Purchases
{
    /// <summary>
    /// Interaction logic for ShowImeiUserControl.xaml
    /// </summary>
    public partial class ShowImeiUserControl : UserControl
    {
        public Purchases.ShowPurchesDetailsUserControl parent = null;
        public List<Models.IMEI> iMEI = null;
        public ShowImeiUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CompanyVG.ItemsSource = iMEI;
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.SelectCompanyDilog.IsOpen = false;
        }
    }
}
