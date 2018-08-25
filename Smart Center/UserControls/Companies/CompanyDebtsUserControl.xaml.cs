using BespokeFusion;
using Microsoft.EntityFrameworkCore;
using Smart_Center.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for CompanyDebtsUserControl.xaml
    /// </summary>
    public partial class CompanyDebtsUserControl : UserControl
    {
        public MainWindow MainWindow = null;
        public Company company = null;
        SmartDbContext dbContext = new SmartDbContext();
        List<Debt> Debts = new List<Debt>();
        List<DebtPay> debtPays = new List<DebtPay>();
        
        public CompanyDebtsUserControl()
        {
            InitializeComponent();
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = company.Name;
            txtEmail.Text = company.Email;
            txtPhone.Text = company.Phone;
            LoadDebt();
            LoadDebtsPay();
            CompanyVG.ItemsSource = this.Debts;
            calc();
        }

        private void LoadDebt()
        {
            Debts = dbContext.Debts.Where(x => x.Company_Id == company.Id).
                Include(x=>x.Purchas).ThenInclude(x=>x.purchasDetails).ToList();
        }

        public void LoadDebtsPay()
        {
            debtPays = dbContext.DebtPays.Where(x => x.Company_id == company.Id).ToList();
            CompanyPayGV.ItemsSource = this.debtPays;

        }

        public void calc()
        {
            float totalDebts = Debts.Sum(x => x.Rem);
            float totalpay = debtPays.Sum(x => x.Pay);
            txtTotalDebts.Text = (totalDebts - totalpay).ToString();
            txtCountDebts.Text = Debts.Count.ToString();
            txtCountPay.Text = debtPays.Count.ToString();
        }

        private void payLoanClick(object sender, RoutedEventArgs e)
        {
            ContentPayDilog.Children.Clear();
            DebtPayUserControl ch = new DebtPayUserControl();
            ch.parent = this;
            ch.txtRem.Text = ch.txtDebts.Text = txtTotalDebts.Text;

            ContentPayDilog.Children.Add(ch);
            PayDilog.IsOpen = true;
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            MainWindow.GridMain.Children.Clear();
            UserControlCompany x = new UserControlCompany();
            x.MainWindow = this.MainWindow;
            MainWindow.GridMain.Children.Add(x);
        }

        private void deletDebts(object sender, RoutedEventArgs e)
        {
            if (CompanyPayGV.SelectedItem == null)
            {
                MaterialMessageBox.ShowError("دانا ده‌ینی هه‌لبژێره‌");
            }
            else
            {
                MessageBoxResult result = MaterialMessageBox.ShowWithCancel("ئایا تو ئاماده‌ی ئه‌مه‌ بسریته‌وه‌؟", "سرینه‌وه‌");

                if (result.ToString() == "OK")
                {
                    DebtPay d = CompanyPayGV.SelectedItem as DebtPay;
                    dbContext.DebtPays.Remove(d);
                    try
                    {
                        dbContext.SaveChanges();
                        LoadDebtsPay();
                        calc();
                    }
                    catch (Exception)
                    {
                        MaterialMessageBox.Show("Error");
                    }
                }
            }
        }
    }



    public class CalcPurchesTotal : IValueConverter
    {
      

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                ICollection<Models.PurchasDetail> d = (ICollection<Models.PurchasDetail>)value;

                return d.Sum(x => x.TotalAmount);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




}
