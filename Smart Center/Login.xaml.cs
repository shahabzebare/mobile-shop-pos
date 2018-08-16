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
using System.Windows.Shapes;

namespace Smart_Center
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void onCloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void onLoginClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtUsername.Text.Trim() == "")
                error += "تكایه‌ ناڤێ به‌كارهێنه‌ری بنڤیسه‌" + "\n";

            if (txtPassword.Password.Trim() == "")
                error += "تكایه‌ ژمارا نهێنی بنڤیسه" + "\n";

            if(error=="")
            {
                Models.SmartDbContext db = new Models.SmartDbContext();
                var admin = db.Admins.Where(p => p.Username == txtUsername.Text && p.Password == txtPassword.Password).SingleOrDefault();
                if(admin!=null)
                {
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                }else
                {
                    MaterialMessageBox.ShowError("ناڤێ به‌كارهێنه‌ری یان ژمارا نهێنی با دروست نیه‌!!");
                }
            }else
            {
                MaterialMessageBox.ShowError(error);
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
