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
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="#@84949jdsjkdfjk$#@")
            {
                ConfigWindow config = new ConfigWindow();
                config.Show();
                this.Close();
            }
        }
    }
}
