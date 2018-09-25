using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Center
{
    public partial class ConfigWindow : Form
    {
        public ConfigWindow()
        {
            InitializeComponent();
            txtServer.Text = Properties.Settings.Default.Server;
            txtDatabase.Text = Properties.Settings.Default.Database;
            if (Properties.Settings.Default.Mode == "SQL")
            {
                rbSQL.Checked = true;
                txtID.Text = Properties.Settings.Default.ID;
                txtPWD.Text = Properties.Settings.Default.Password;
            }
            else
            {
                rbWindoes.Checked = true;
                txtID.Clear();
                txtPWD.Clear();
                txtID.ReadOnly = true;
                txtPWD.ReadOnly = true;
            }
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server = txtServer.Text;
            Properties.Settings.Default.Database = txtDatabase.Text;
            Properties.Settings.Default.Mode = rbSQL.Checked == true ? "SQL" : "Windows";
            Properties.Settings.Default.ID = txtID.Text;
            Properties.Settings.Default.Password = txtPWD.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("بسه‌ركه‌فتیانه‌ هاتنه‌ هه‌لگرتن");
        }

        private void ConfigWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
