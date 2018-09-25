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
using Microsoft.EntityFrameworkCore;
using Smart_Center.Models;

namespace Smart_Center.UserControls.Orders
{
    /// <summary>
    /// Interaction logic for AddOrderUserControl.xaml
    /// </summary>
    public partial class AddOrderUserControl : UserControl
    {
        List<OrderDetail> listOfOrderDetails = new List<OrderDetail>();
        List<OrderDetail> listOfOrderDetailsDT = new List<OrderDetail>();
        public Models.Product Product = null;
        public MainWindow MainWindow = null;
        SmartDbContext SmartDb = new SmartDbContext();
        public Customer custmer = null;
        public IMEI iMEI = null;

        public AddOrderUserControl()
        {
            InitializeComponent();
            dtPurches.SelectedDate = DateTime.Today;
            Order p = SmartDb.Orders.LastOrDefault();
            if (p != null)
                txtPurchesId.Text = (p.Id + 1).ToString();
            else
                txtPurchesId.Text = "0";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PurchesGV.ItemsSource = listOfOrderDetails;
            Keyboard.Focus(txtProductBarcode);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentSelectCompanyDilog.Children.Clear();
            UserControls.Cutomers.CustomerForSelectingUserControl ch = new UserControls.Cutomers.CustomerForSelectingUserControl();
            ch.parent = this;
            ContentSelectCompanyDilog.Children.Add(ch);
            SelectCompanyDilog.IsOpen = true;
        }

        private void selectProduct(object sender, RoutedEventArgs e)
        {
            ContentSelectCompanyDilog.Children.Clear();
            UserControls.Product.ShowProductSelectOrderUserControl ch = new UserControls.Product.ShowProductSelectOrderUserControl();
            ch.parent = this;
            ContentSelectCompanyDilog.Children.Add(ch);
            SelectCompanyDilog.IsOpen = true;
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

                Models.Product product = SmartDb.Products.FirstOrDefault(x => x.Barcode == txtProductBarcode.Text);

                if (product == null)
                {
                    MaterialMessageBox.ShowError("ئه‌ڤ باركوده‌ نیه‌ ‌!!");
                }
                else
                {
                    this.Product = product;
                    txtProductName.Text = product.Name;

                    if ((bool)isMulti.IsChecked)
                    {
                        txtProductSalesPrice.Text = product.sales_price_multi.ToString();
                    }
                    else
                        txtProductSalesPrice.Text = product.sales_price.ToString();

                    PurchasDetail purchasDetail = SmartDb.PurchasDetail.Where(j => j.Barcode == product.Barcode).Where(x => x.QteCh > 0).OrderByDescending(x => x.purchasId).LastOrDefault();


                    txtProductByePrice.Text = purchasDetail.ByePrice.ToString();

                    txtQte.Text = "0";
                    if (product.isHaveIMEI)
                    {
                        this.ImeiPanel.IsEnabled = true;
                        this.txtQte.IsReadOnly = true;
                        txtQte.Text = "1";
                        txtImei.Focus();
                        this.txtQte.Select(0, txtQte.Text.Length);
                    }
                    else
                    {
                        ImeiPanel.IsEnabled = false;
                        txtQte.IsReadOnly = false;
                        txtQte.Focus();
                    }
                    txtQte_KeyUp(null, null);
                }
            }
        }

        private void onQteAndPriceChnage(object sender, TextChangedEventArgs e)
        {
            TextBox x = (TextBox)sender;
            if (x.Text == "")
                x.Text = "0";
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

        private void txtQte_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtQte.Text == String.Empty || txtProductSalesPrice.Text == String.Empty || txtDicount.Text == String.Empty)
            {
              
            }
            else
            {
                float sales = float.Parse(txtProductSalesPrice.Text.ToString());
                float qte = float.Parse(txtQte.Text.ToString());
                float discount = float.Parse(txtDicount.Text.ToString());
                float last = sales * qte;
                float reja = discount / 100;
                float dashkandn = last * reja;

                txtAmount.Text = (last - dashkandn).ToString();
            }
        }

        private void txtQte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnClickNewRow(null, null);
            }
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

            if (!ImeiPanel.IsEnabled && listOfOrderDetails.Find(x=>x.Barcode==txtProductBarcode.Text)!=null)
            {
                MaterialMessageBox.Show("باركودێ پێناسێ دوباره‌یه‌‌!");
                return;
            }

            if (txtProductByePrice.Text == String.Empty || txtProductByePrice.Text == "0")
            {
                MaterialMessageBox.ShowError("بهایێ كرینێ پێناسێ پێتڤیه‌!");
                return;
            }

            if (txtProductSalesPrice.Text == String.Empty || txtProductSalesPrice.Text == "0")
            {
                MaterialMessageBox.ShowError("بهایێ فروتنێ تاك پێناسێ پێتڤیه‌!");
                return;
            }

            if (txtQte.Text == String.Empty || txtQte.Text == "0")
            {
                MaterialMessageBox.ShowError("ژمارا پێناسێ پێتڤیه‌!");
                return;
            }

            if(ImeiPanel.IsEnabled)
            {
                if (listOfOrderDetails.Find(x => x.IMEIOrders == txtImei.Text) != null)
                {
                    MaterialMessageBox.ShowError(" IMEI دوباره‌ نابیت ‌!");
                    return;
                }
                OrderDetail orderDetail = new OrderDetail()
                {
                    PurchesDetailId = this.iMEI.purchesDetailId,
                    PurchasDetail = this.iMEI.PurchasDetail,
                    Product = new Models.Product() { Name = Product.Name },
                    Barcode = txtProductBarcode.Text,
                    ByePrice = float.Parse(txtProductByePrice.Text),
                    SalesPrice = float.Parse(txtProductSalesPrice.Text),
                    Qte = int.Parse(txtQte.Text),
                    Discount = int.Parse(txtDicount.Text),
                    IMEIOrders = txtImei.Text,
                    Mode = (bool)isMulti.IsChecked ? Mode.Multi : Mode.Single,
                    Bin = (float.Parse(txtAmount.Text) - (float.Parse(txtProductByePrice.Text) * int.Parse(txtQte.Text))),
                    TotalAmount = float.Parse(txtAmount.Text),
                };

                listOfOrderDetails.Add(orderDetail);

                listOfOrderDetailsDT.Add(new OrderDetail()
                {
                    PurchesDetailId = this.iMEI.purchesDetailId,
                    PurchasDetail = this.iMEI.PurchasDetail,
                    Product = new Models.Product() { Name = Product.Name },
                    Barcode = txtProductBarcode.Text,
                    ByePrice = float.Parse(txtProductByePrice.Text),
                    SalesPrice = float.Parse(txtProductSalesPrice.Text),
                    Qte = int.Parse(txtQte.Text),
                    Discount = int.Parse(txtDicount.Text),
                    IMEIOrders = txtImei.Text,
                    Mode = (bool)isMulti.IsChecked ? Mode.Multi : Mode.Single,
                    Bin = (float.Parse(txtAmount.Text) - (float.Parse(txtProductByePrice.Text) * int.Parse(txtQte.Text))),
                    TotalAmount = float.Parse(txtAmount.Text),
                });

            }
            else
            {
                Models.Product p = versfyQte();
                if(p==null)
                {
                    return;
                }
                else
                {
                    int qte = int.Parse(txtQte.Text);
                    List<PurchasDetail> purchasDetail = p.PurchasDetails.OrderBy(x => x.Id).Where(j=>j.QteCh>0).ToList();
                    int index = 0;
                    while (qte>0)
                    {
                        int newqte = purchasDetail[index].QteCh;
                        int qtedisc = 0;
                        if (newqte < qte)
                        {
                            qte = qte - newqte;
                            qtedisc = newqte;
                        }
                        else
                        {
                            qtedisc = qte;
                            qte = 0;
                        }

                        float sales = float.Parse(txtProductSalesPrice.Text.ToString());
                        float discount = float.Parse(txtDicount.Text.ToString());
                        float last = sales * qtedisc;
                        float reja = discount / 100;
                        float dashkandn = last * reja;
                        float totalAmount = last - dashkandn;


                        OrderDetail orderDetail = new OrderDetail()
                        {
                            PurchesDetailId = purchasDetail[index].Id,
                            PurchasDetail = purchasDetail[index],
                            Product = new Models.Product() { Name = Product.Name },
                            Barcode = txtProductBarcode.Text,
                            ByePrice = float.Parse(txtProductByePrice.Text),
                            SalesPrice = sales,
                            Qte = qtedisc,
                            Discount = discount,
                            IMEIOrders = txtImei.Text,
                            Mode = (bool)isMulti.IsChecked ? Mode.Multi : Mode.Single,
                            Bin = (float.Parse(txtAmount.Text) - (float.Parse(txtProductByePrice.Text) * int.Parse(txtQte.Text))),
                            TotalAmount = totalAmount,
                        };

                        listOfOrderDetails.Add(orderDetail);

                        listOfOrderDetailsDT.Add(new OrderDetail()
                        {
                            PurchesDetailId = purchasDetail[index].Id,
                            PurchasDetail = purchasDetail[index],
                            Product = new Models.Product() { Name = Product.Name },
                            Barcode = txtProductBarcode.Text,
                            ByePrice = float.Parse(txtProductByePrice.Text),
                            SalesPrice = sales,
                            Qte = qtedisc,
                            Discount = discount,
                            IMEIOrders = txtImei.Text,
                            Mode = (bool)isMulti.IsChecked ? Mode.Multi : Mode.Single,
                            Bin = (float.Parse(txtAmount.Text) - (float.Parse(txtProductByePrice.Text) * int.Parse(txtQte.Text))),
                            TotalAmount = totalAmount,
                        });
                        index++;
                    }
                }
            }

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
            txtDicount.Text = "0";
            txtImei.Text = "";
        }

        void calaTotalAmount()
        {
            txtTotalAmoount.Text = listOfOrderDetails.Sum(x => x.TotalAmount).ToString();
        }

        private void PurchesGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PurchesGV.CurrentItem != null)
            {
                listOfOrderDetails.RemoveAt(PurchesGV.SelectedIndex);
                listOfOrderDetailsDT.RemoveAt(PurchesGV.SelectedIndex);
                PurchesGV.Items.Refresh();
                calaTotalAmount();
                txtTotalAmoount_KeyUp(null, null);
                txtProductBarcode.Focus();
            }
        }

        void clearBox()
        {
            custmer = null;
            txtDisc.Clear();
            txtIdCompany.Clear();
            txtNameCompany.Clear();
            txtPurchesId.Text = txtPurchesId.Text = (SmartDb.Orders.LastOrDefault().Id + 1).ToString();
            dtPurches.SelectedDate = DateTime.Today;
            txtTotalAmoount.Text = "0";
            txtRemAmount.Text = "0";
            txtPayAmount.Text = "0";
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if (custmer == null)
            {
                MaterialMessageBox.ShowError("‌كریاره‌كێ هه‌لبژێره‌");
                return;
            }

            if (listOfOrderDetails.Count <= 0)
            {
                MaterialMessageBox.ShowError("پێتڤیه‌ پێناسه‌كێ هه‌لبژێری!");
                return;
            }

            using (var transaction = SmartDb.Database.BeginTransaction())
            {


                Order order = new Order()
                {
                    CustomerId = custmer.Id,
                    AdminId = 1,
                    Disc = txtDisc.Text,
                    Date = (DateTime)dtPurches.SelectedDate,
                    CreatedAt = DateTime.Now
                };

                order.OrderDetails = listOfOrderDetails;


                if ((bool)isDebats.IsChecked)
                {
                    List<MyDebt> x = new List<MyDebt>();
                    x.Add(new MyDebt()
                    {
                        Customer_Id = custmer.Id,
                        Pay = float.Parse(txtPayAmount.Text.ToString()),
                        Rem = float.Parse(txtRemAmount.Text.ToString()),
                    });
                    order.myDebts = x;
                }

                SmartDb.Add<Order>(order);

                try
                {
                    SmartDb.SaveChanges();
                    foreach (OrderDetail item in listOfOrderDetails)
                    {
                        item.PurchasDetail.QteCh = item.PurchasDetail.QteCh - item.Qte;
                        SmartDb.Update<PurchasDetail>(item.PurchasDetail);
                        if(item.IMEIOrders!="")
                        {
                          IMEI iMEI =   SmartDb.iMEIs.SingleOrDefault(x => x.ImEI == item.IMEIOrders);
                            iMEI.inStock = false;
                            SmartDb.iMEIs.Update(iMEI);
                        }
                        SmartDb.SaveChanges();
                    }
                    
                    ClearBoxRow();
                    clearBox();
                    isDebats.IsChecked = false;
                    listOfOrderDetails.Clear();
                    listOfOrderDetailsDT.Clear();
                    PurchesGV.Items.Refresh();
                    MaterialMessageBox.Show("ب سه‌ركه‌فتیانه‌ هاتنه‌ هه‌لگرتن");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MaterialMessageBox.ShowError(ex.Message);
                }
                finally
                {
                    transaction.Dispose();
                }


            }

        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlOrder x = new UserControlOrder();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
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
            }
            catch (Exception)
            {
                txtPayAmount.Text = "0";
            }

        }

        private void changeMultiAndSingle(object sender, RoutedEventArgs e)
        {

        }

        private Models.Product versfyQte()
        {
            Models.Product product = SmartDb.Products.Include(p => p.PurchasDetails).SingleOrDefault(x => x.Barcode == txtProductBarcode.Text);
            int qte = product.PurchasDetails.Sum(x => x.QteCh);
            if(qte<int.Parse(txtQte.Text))
            {
                txtQte.Text = "0";
                txtQte_KeyUp(null, null);
                txtQte.Focus();
                return null;
            }

            return product;
        }


        private void txtImei_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtImei.Text == String.Empty)
                {
                    MaterialMessageBox.ShowError("IMEI بنڤیسه‌!!");
                    return;
                }


                this.iMEI = SmartDb.iMEIs.Where(x => x.ImEI == txtImei.Text && x.inStock && x.PurchasDetail.Barcode == txtProductBarcode.Text).Include(k=>k.PurchasDetail).LastOrDefault();

               // PurchasDetail purchas = SmartDb.PurchasDetail.Where(x => x.Barcode == txtProductBarcode.Text).Where(k => k.IMEIs.Where(j=> j.ImEI == txtImei.Text && j.inStock).ToList().Count > 0).LastOrDefault();

                if (this.iMEI == null)
                {
                    MaterialMessageBox.ShowError("ئه‌ڤ IMEI‌ نیه‌ ‌!!");
                }
                else
                {
                    txtProductByePrice.Text = this.iMEI.PurchasDetail.ByePrice.ToString();
                    txtDicount.Focus();
                    txtQte_KeyUp(null, null);
                }
            }
        }
    }
}
