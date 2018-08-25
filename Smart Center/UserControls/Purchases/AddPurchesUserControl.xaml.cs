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
        List<PurchasDetail> listOfPurchesDetailDT = new List<PurchasDetail>();
        SmartDbContext SmartDbContext = new SmartDbContext();
        public Company company = null;
        public Models.Product Product = null;
        public List<IMEI> selectedImeI;
        public MainWindow MainWindow = null;
        public AddPurchesUserControl()
        {
            InitializeComponent();
            dtPurches.SelectedDate = DateTime.Today;
            Purchas p = SmartDbContext.Purchas.LastOrDefault();
            if (p != null)
                txtPurchesId.Text = (p.Id + 1).ToString();
            else
                txtPurchesId.Text = "0";
            selectedImeI = new List<IMEI>();
            calaTotalAmount();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtProductBarcode);
            PurchesGV.ItemsSource = listOfPurchesDetailDT;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentSelectCompanyDilog.Children.Clear();
            UserControls.Companies.ShowCompanyForSelectingUserControl ch = new UserControls.Companies.ShowCompanyForSelectingUserControl();
            ch.parent = this;
            ContentSelectCompanyDilog.Children.Add(ch);
            SelectCompanyDilog.IsOpen = true;
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if(company==null)
            {
                MaterialMessageBox.ShowError("كومپانیه‌كێ هه‌لبژێره‌");
                return;
            }

            if(listOfPurchesDetail.Count<=0)
            {
                MaterialMessageBox.ShowError("پێتڤیه‌ پێناسه‌كێ هه‌لبژێری!");
                return;
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

            
            purchas.purchasDetails = listOfPurchesDetail;
            if((bool)isDebats.IsChecked)
            {
                List<Debt> x = new List<Debt>();
                x.Add(new Debt()
                {
                    Company_Id = company.Id,
                    Pay = float.Parse(txtPayAmount.Text.ToString()),
                    Rem = float.Parse(txtRemAmount.Text.ToString()),
                });
                purchas.debt = x;
            }

            SmartDbContext.Add<Purchas>(purchas);

            try
            {
                SmartDbContext.SaveChanges();
                clearBox();
                ClearBoxRow();
                listOfPurchesDetail.Clear();
                listOfPurchesDetailDT.Clear();
                PurchesGV.Items.Refresh();
                MaterialMessageBox.Show("ب سه‌ركه‌فتیانه‌ هاتنه‌ هه‌لگرتن");
            }
            catch (Exception ex)
            {
                MaterialMessageBox.ShowError(ex.Message);
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
            txtTotalAmoount.Text = "0";
            txtRemAmount.Text = "0";
            txtPayAmount.Text = "0";
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlPurchas x = new UserControlPurchas();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void selectProduct(object sender, RoutedEventArgs e)
        {
            ContentSelectCompanyDilog.Children.Clear();
            UserControls.Product.ShowProductUserControl ch = new UserControls.Product.ShowProductUserControl();
            ch.parent = this;
            ContentSelectCompanyDilog.Children.Add(ch);
            SelectCompanyDilog.IsOpen = true;
        }

       

        private void onQteAndPriceChnage(object sender, TextChangedEventArgs e)
        {
            TextBox x = (TextBox)sender;
            string removed = "";
            foreach (var item in x.Text)
            {
                if (item >= '0' && item <= '9')
                    removed += item;
                if (item == '.' && !removed.Contains('.'))
                    removed += item;
            }
            x.Text = removed;
            x.Select(removed.Length, 0);

        }

        public void txtQte_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtQte.Text == String.Empty || txtProductSalesPrice.Text == String.Empty)
            {
                txtQte.Text = "0";
                txtProductSalesPrice.Text = "0";
                txtQte_KeyUp(sender, e);
            }
            else
            {
                float sales = float.Parse(txtProductByePrice.Text.ToString());
                float qte = float.Parse(txtQte.Text.ToString());
                float last = sales * qte;
                txtAmount.Text = last.ToString();
            }


        }

        private void onInsertImei(object sender, RoutedEventArgs e)
        {
            ContentSelectCompanyDilog.Children.Clear();
            UserControls.Purchases.AddIMEIUserControl ch = new UserControls.Purchases.AddIMEIUserControl();
            ch.parent = this;
            ContentSelectCompanyDilog.Children.Add(ch);
            SelectCompanyDilog.IsOpen = true;
        }

        private void OnClickNewRow(object sender, RoutedEventArgs e)
        {
            if (txtProductBarcode.Text == String.Empty)
            {
                MaterialMessageBox.ShowError("باركودێ پێناسێ پێتڤیه‌!");
                return;

            }

            if (txtProductName.Text == String.Empty)
            {
                MaterialMessageBox.ShowError("ناڤێ پێناسێ پێتڤیه‌!");
                return;
            }

            if (txtProductByePrice.Text == String.Empty || txtProductByePrice.Text=="0")
            {
                MaterialMessageBox.ShowError("بهایێ كرینێ پێناسێ پێتڤیه‌!");
                return;
            }

            if (txtProductSalesPrice.Text == String.Empty || txtProductSalesPrice.Text =="0")
            {
                MaterialMessageBox.ShowError("بهایێ فروتنێ تاك پێناسێ پێتڤیه‌!");
                return;
            }

            if (txtProductSalesPriceMulti.Text == String.Empty || txtProductSalesPriceMulti.Text == "0")
            {
                MaterialMessageBox.ShowError("بهایێ فروتنێ كو پێناسێ پێتڤیه‌!");
                return;
            }

            if (txtQte.Text == String.Empty || txtQte.Text == "0")
            {
                MaterialMessageBox.ShowError("ژمارا پێناسێ پێتڤیه‌!");
                return;
            }

            if (listOfPurchesDetail.Find(x => x.Barcode == txtProductBarcode.Text) != null)
            {
                MaterialMessageBox.ShowError(" پێناسا دوباره‌ نابیت پێتڤیه‌!");
                return;
            }

            PurchasDetail purchasDetail = new PurchasDetail()
            {
                Barcode = txtProductBarcode.Text,
                ByePrice = float.Parse(txtProductByePrice.Text),
                SalesPrice = float.Parse(txtProductSalesPrice.Text),
                SalesPriceMuli = float.Parse(txtProductSalesPriceMulti.Text),
                IMEIs = selectedImeI,
                Qte = int.Parse(txtQte.Text),
                QteCh = int.Parse(txtQte.Text),
                TotalAmount = float.Parse(txtAmount.Text),
            };

            listOfPurchesDetail.Add(purchasDetail);

            listOfPurchesDetailDT.Add( new PurchasDetail(){
                    Product = new Models.Product() { Name=Product.Name},
                    Barcode = txtProductBarcode.Text,
                    ByePrice = float.Parse(txtProductByePrice.Text),
                    SalesPrice = float.Parse(txtProductSalesPrice.Text),
                    SalesPriceMuli = float.Parse(txtProductSalesPriceMulti.Text),
                    IMEIs = selectedImeI,
                    Qte = int.Parse(txtQte.Text),
                    QteCh = int.Parse(txtQte.Text),
                    TotalAmount = float.Parse(txtAmount.Text),
                });

            PurchesGV.Items.Refresh();
            txtProductBarcode.Focus();
            ClearBoxRow();
            calaTotalAmount();
            txtTotalAmoount_KeyUp(null, null);
        }

        void ClearBoxRow()
        {
            txtProductBarcode.Clear();
            txtProductByePrice.Text = "0";
            txtProductSalesPrice.Text = "0";
            txtQte.Text = "0";
            txtProductName.Text = "";
            txtAmount.Text = "0";
            txtProductSalesPriceMulti.Text = "0";
            selectedImeI = new List<IMEI>();
        }

        void calaTotalAmount()
        {
            txtTotalAmoount.Text =  listOfPurchesDetail.Sum(x => x.TotalAmount).ToString();
        }

        private void txtProductBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtProductBarcode.Text == String.Empty)
                {
                    MaterialMessageBox.ShowError("باركودێ بنڤیسه‌!!");
                    return;
                }

                Models.Product product = SmartDbContext.Products.FirstOrDefault(x => x.Barcode == txtProductBarcode.Text);

                if (product == null)
                {
                    MaterialMessageBox.ShowError("ئه‌ڤ باركوده‌ نیه‌ ‌!!");
                }
                else
                {
                    this.Product = product;
                    txtProductName.Text = product.Name;
                    txtProductSalesPrice.Text = product.sales_price.ToString();
                    txtProductByePrice.Text = product.bye_price.ToString();
                    txtProductSalesPriceMulti.Text = product.sales_price_multi.ToString();
                    txtQte.Text = "0";
                    selectedImeI = new List<IMEI>();
                    if (product.isHaveIMEI)
                    {
                        this.ImeiPanel.Visibility = Visibility.Visible;
                        this.txtQte.IsReadOnly = true;
                        this.txtQte.Focus();
                        this.txtQte.Select(0, txtQte.Text.Length);
                    }
                    else
                    {
                        ImeiPanel.Visibility = Visibility.Hidden;
                        txtQte.IsReadOnly = false;
                        txtQte.Focus();
                    }
                }
            }
        }

        private void txtQte_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                OnClickNewRow(null, null);
            }
        }

        private void PurchesGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(PurchesGV.CurrentItem!=null)
            {
                PurchasDetail purchasDetail = (PurchasDetail)PurchesGV.CurrentItem;
                txtProductBarcode.Text = purchasDetail.Barcode;
                txtProductName.Text = purchasDetail.Product.Name;
                txtProductByePrice.Text = purchasDetail.ByePrice.ToString();
                txtProductSalesPrice.Text = purchasDetail.SalesPrice.ToString();
                txtProductSalesPriceMulti.Text = purchasDetail.SalesPriceMuli.ToString();
                txtQte.Text = purchasDetail.Qte.ToString();
                txtAmount.Text = purchasDetail.TotalAmount.ToString();
                if (purchasDetail.IMEIs == null)
                {
                    ImeiPanel.Visibility = Visibility.Hidden;
                    txtQte.IsReadOnly = false;
                }
                else
                {
                    ImeiPanel.Visibility = Visibility.Visible;
                    txtQte.IsReadOnly = true;
                }
                this.Product = purchasDetail.Product;
                selectedImeI = purchasDetail.IMEIs.ToList();
                listOfPurchesDetail.RemoveAt(PurchesGV.SelectedIndex);
                listOfPurchesDetailDT.RemoveAt(PurchesGV.SelectedIndex);
                PurchesGV.Items.Refresh();
                calaTotalAmount();
                txtTotalAmoount_KeyUp(null, null);
            }
        }

        private void changeDebatAndPay(object sender, RoutedEventArgs e)
        {
            if ((bool)isPayNow.IsChecked)
            {
                payPanel.Visibility = Visibility.Hidden;
                remPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                payPanel.Visibility = Visibility.Visible;
                remPanel.Visibility = Visibility.Visible;
            }

        }

       

        private void txtTotalAmoount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                float total = float.Parse(txtTotalAmoount.Text);
                float pay = float.Parse(txtPayAmount.Text);
                if (pay > total)
                {
                    MaterialMessageBox.Show("پارێ دایی پتره‌ ل هه‌می پاره‌ی");
                    txtPayAmount.Text = "0";
                }
                else
                {
                    txtRemAmount.Text = (total - pay).ToString();
                }
            }catch(Exception)
            {
                txtPayAmount.Text = "0";
            }
        }
    }
}
