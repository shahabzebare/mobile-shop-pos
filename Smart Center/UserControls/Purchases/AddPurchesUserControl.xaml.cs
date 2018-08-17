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
using BespokeFusion;
using Smart_Center.Models;

namespace Smart_Center.UserControls.Purchases
{
    /// <summary>
    /// Interaction logic for AddPurchesUserControl.xaml
    /// </summary>
    public partial class AddPurchesUserControl : UserControl
    {
        List<PurchasDetail> listOfPurchesDetail = new List<PurchasDetail>();
        SmartDbContext SmartDbContext = new SmartDbContext();
        public Company company = null;
        public AddPurchesUserControl()
        {
            InitializeComponent();
            dtPurches.SelectedDate = DateTime.Today;
            txtPurchesId.Text = (SmartDbContext.Purchas.LastOrDefault().Id+1).ToString();
            UserControls.Companies.ShowCompanyForSelectingUserControl ch = new UserControls.Companies.ShowCompanyForSelectingUserControl();
            ch.parent = this;
            ContentSelectCompanyDilog.Children.Add(ch);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listOfPurchesDetail.Add(new PurchasDetail() { purchasId = 1, Barcode = "dj455", Qte = 29, ByePrice = 531, SalesPrice = 1515, TotalAmount = 5153,Product=new Models.Product() {Name="name" } });
            PurchesGV.ItemsSource = listOfPurchesDetail;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectCompanyDilog.IsOpen = true;
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if(company==null)
            {
                MaterialMessageBox.ShowError("كومپانیه‌كێ هه‌لبژێره‌");
            }

            

            Purchas purchas = new Purchas()
            {
                companyId = company.Id,
                adminId = 1,
                Disc = txtDisc.Text,
                Date = (DateTime)dtPurches.SelectedDate,
                Number = txtPurchesId.Text,
                CreatedAt = DateTime.Now
            };

            SmartDbContext.Purchas.Add(purchas);

            try
            {
                SmartDbContext.SaveChanges();
                clearBox();
                MaterialMessageBox.Show("ب سه‌ركه‌فتیانه‌ هاتنه‌ هه‌لگرتن");
            }
            catch (Exception)
            {
                MaterialMessageBox.ShowError("EROOR!");
            }


        }

        void clearBox()
        {
            company = null;
            txtDisc.Clear();
            txtIdCompany.Clear();
            txtNameCompany.Clear();
            txtPurchesId.Text = txtPurchesId.Text = (SmartDbContext.Purchas.LastOrDefault().Id + 1).ToString();
            txtPurchesNumber.Clear();
            dtPurches.SelectedDate = DateTime.Today;
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
