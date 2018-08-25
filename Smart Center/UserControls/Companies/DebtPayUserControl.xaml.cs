﻿using BespokeFusion;
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
    /// Interaction logic for DebtPayUserControl.xaml
    /// </summary>
    public partial class DebtPayUserControl : UserControl
    {

        public CompanyDebtsUserControl parent = null;
        Models.SmartDbContext SmartDbContext = new Models.SmartDbContext();
        public DebtPayUserControl()
        {
            InitializeComponent();
        }

        private void onSaveClick(object sender, RoutedEventArgs e)
        {
            if (txtPay.Text == "")
            {
                MaterialMessageBox.ShowError("دڤێت پارێ دایی داخل بكه‌ی!");
                return;
            }

            float pay = float.Parse(txtPay.Text);
            if(pay>0)
            {
                SmartDbContext.DebtPays.Add(new Models.DebtPay()
                {
                    Company_id = parent.company.Id,
                    Date = DateTime.Now,
                    Pay = pay
                });


                try
                {
                    SmartDbContext.SaveChanges();
                    
                    txtDebts.Text = "0";
                    txtPay.Text = "0";
                    txtRem.Text = "0";
                    parent.PayDilog.IsOpen = false;
                    MaterialMessageBox.Show("ب سه‌ركه‌فتیانه‌ هاتنه‌ هه‌لگرتن");
                    parent.LoadDebtsPay();
                    parent.calc();
                }
                catch (Exception ex)
                {
                    MaterialMessageBox.ShowError(ex.Message);
                }
            }
            else
            {
                MaterialMessageBox.Show("نابیت پارێ دایی سفر بیت ");
            }

        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            parent.PayDilog.IsOpen = false;
        }

        private void txtPay_TextChanged(object sender, TextChangedEventArgs e)
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

        private void txtPay_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDebts.Text == String.Empty || txtPay.Text == String.Empty)
            {
                txtPay.Text = "0";
                txtPay_KeyUp(sender, e);
            }
            else
            {
                float sales = float.Parse(txtDebts.Text.ToString());
                float qte = float.Parse(txtPay.Text.ToString());
                if(qte>sales)
                {
                    MaterialMessageBox.ShowError("نابیت پارێ دایی زێده‌تر بیت پارێ ده‌ینی");
                    txtPay.Text = "0";
                    txtPay_KeyUp(sender, e);
                    return;
                }
                float last = sales - qte;
                txtRem.Text = last.ToString();
            }
        }
    }
}
