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

namespace StudentBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student student;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            student = new Student() { 
                ID = "19120383", 
                Fullname = "Huỳnh Tấn Thọ", 
                Address = "Hóc Môn, TP.HCM", 
                Email = "tantho340@gmail.com",
                PhoneNumber = "0945439849"
            };
            this.DataContext = student;
        }
        private void onChangeButtonClick(object sender, RoutedEventArgs e)
        {
            this.DataContext = student;
            student.ID += "1";
            MessageBox.Show(student.ID);
        }
    }
}
