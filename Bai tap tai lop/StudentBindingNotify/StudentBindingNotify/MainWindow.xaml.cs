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

namespace StudentBindingNotify
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

        Student sv = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sv = new Student()
            {
                ID = "19120383",
                Fullname = "Huỳnh Tấn Thọ",
                Address = "Hóc Môn, TP.HCM",
                Email = "sinhvien_946@gmail.com",
                PhoneNumber = "0945439849",
                Avatar = "images/img1.png"
            };
            this.DataContext = sv;
        }

        private void onUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            sv.ID = "19120338";
            sv.Fullname = "Trần Hoàng Quân";
            sv.Address = "Bến Tre";
            sv.PhoneNumber = "0857626514";
            sv.Avatar = "images/img2.png";
        }
    }
}
