using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SixJarsAdvisor
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

        private void adviseBtnClick(object sender, RoutedEventArgs e)
        {
            string inputIncome = incomeTxtBox.Text;
            int income = -1;
            if (inputIncome.Length == 0)
                MessageBox.Show("ERROR: EMPTY FIELD!\nPlease input data before clicking on the button.");
            else if (!int.TryParse(inputIncome, out income))
                MessageBox.Show("ERROR: INVALID DATA!\nPlease input a number.");
            else if (income <= 0)
                MessageBox.Show("ERROR: INVALID DATA!\nPlease input a POSITIVE number.");
            else display(income);
            
        }

        private void closeBtnCLick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void display(int income)
        {
            UserIncome userIncome = new UserIncome(income);
            this.DataContext = userIncome;
            chiTieu.Content = "Chi tiêu cần thiết";
            tietKiem.Content = "Tiết kiệm dài hạn";
            giaoDuc.Content = "Quỹ giáo dục";
            huongThu.Content = "Hưởng thụ";
            tuDo.Content = "Tự do tài chính";
            tuThien.Content = "Quỹ từ thiện";
        }
    }
}
