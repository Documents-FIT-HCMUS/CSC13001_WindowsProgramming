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

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onLoginButtonCLick(object sender, RoutedEventArgs e)
        {
            string username = "admin", password = "qwe3@1";
            if (usernameBox.Text == username && passwordBox.Password == password)
            {
                var dashboard = new Dashboard();
                dashboard.Show();
                loginScreen.Close();
            }
            else if (usernameBox.Text.Length == 0 || passwordBox.Password.Length == 0)
            {
                MessageBox.Show("ERROR: EMPTY FIELDS!\nPlease input the username and/or password.");
            }
            else
            {
                MessageBox.Show("ERROR: WRONG USERNAME OR PASSWORD!\nPlease check again and retry.");
            }
        }
    }
}
