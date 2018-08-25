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

namespace Smart_Center.UserControls.Companies
{
    /// <summary>
    /// Interaction logic for ShowCompanyForSelectingUserControl.xaml
    /// </summary>
    public partial class ShowCompanyForSelectingUserControl : UserControl
    {
        List<Company> companies = null;
        public Purchases.AddPurchesUserControl parent = null;
        SmartDbContext smartDb;
        public ShowCompanyForSelectingUserControl()
        {
            InitializeComponent();
            smartDb = new SmartDbContext();
        }

        public void loadData()
        {
            companies = smartDb.Companies.ToList();
            CompanyVG.ItemsSource = companies;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if(CompanyVG.SelectedItem!=null)
            {
                parent.company = (Company)CompanyVG.SelectedItem;
                parent.txtNameCompany.Text = parent.company.Name;
                parent.txtIdCompany.Text = parent.company.Id.ToString();
                parent.SelectCompanyDilog.IsOpen = false;
            }
            else
            {
                MaterialMessageBox.ShowError("كومپانیه‌كێ هه‌لبژێره‌");
            }
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.SelectCompanyDilog.IsOpen = false;
        }

        private void onSearchCompany(object sender, TextChangedEventArgs e)
        {
            companies = smartDb.Companies.Where(x=>x.Name.Contains(txtSearch.Text)).ToList();
            CompanyVG.ItemsSource = companies;
        }
    }
}
