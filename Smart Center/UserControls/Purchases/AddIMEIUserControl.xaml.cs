using BespokeFusion;
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
    /// Interaction logic for AddIMEIUserControl.xaml
    /// </summary>
    public partial class AddIMEIUserControl : UserControl
    {

        public Purchases.AddPurchesUserControl parent = null;
        public AddIMEIUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CompanyVG.ItemsSource = parent.selectedImeI;
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            parent.txtQte.Text = parent.selectedImeI.Count().ToString();
            parent.SelectCompanyDilog.IsOpen = false;
            parent.txtQte_KeyUp(null,null);
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.SelectCompanyDilog.IsOpen = false;
        }

        private void onAddIMEI(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (parent.selectedImeI.Find(x => x.ImEI == txtIMEI.Text) == null)
                {
                    Models.SmartDbContext dbContext = new Models.SmartDbContext();
                    if (dbContext.iMEIs.Where(x => x.ImEI == txtIMEI.Text).ToList().Count==0)

                    {
                        if (txtIMEI.Text != String.Empty)
                        {
                            parent.selectedImeI.Add(new Models.IMEI() { ImEI = txtIMEI.Text, inStock = true });
                            txtIMEI.Text = "";
                            CompanyVG.Items.Refresh();
                        }
                        else
                        {
                            MaterialMessageBox.Show("IMEI بنڤیسه‌");
                        }
                    }
                    else
                    {
                        MaterialMessageBox.ShowError("ئه‌ڤ IMEI یێ هه‌ی");
                    }
                }
                else
                {
                    MaterialMessageBox.ShowError("ئه‌ڤ IMEI یێ هه‌ی");
                }
            }
        }

        private void CompanyVG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(CompanyVG.CurrentItem!=null)
            {
                parent.selectedImeI.RemoveAt(CompanyVG.SelectedIndex);
                CompanyVG.Items.Refresh();
            }
        }
    }
}
