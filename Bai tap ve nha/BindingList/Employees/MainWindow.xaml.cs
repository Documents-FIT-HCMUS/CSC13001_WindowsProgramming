using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        BindingList<Employee> employees = new BindingList<Employee>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            employees.Add(new Employee()
            {
                Fullname = "Kiley Alphonso",
                Email = "kiley343@gmail.co.uk",
                Address = "Street 1A, District 7",
                PhoneNumber = "45848494",
                ImagePath = "images/avatar01.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Morty Jarod",
                Email = "morty121@gmail.com",
                Address = "Street 1B, District 9",
                PhoneNumber = "27729191",
                ImagePath = "images/avatar02.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Mavis Bonita",
                Email = "mavis002@outlook.com",
                Address = "Street 2C, District 10",
                PhoneNumber = "09992817",
                ImagePath = "images/avatar03.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Dominick Elise",
                Email = "domelise@gmail.com",
                Address = "Street 5D, District 2",
                PhoneNumber = "65285151",
                ImagePath = "images/avatar04.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Earnest Rick",
                Email = "ricky45@yahoo.com",
                Address = "Street 2A, District 3",
                PhoneNumber = "45848494",
                ImagePath = "images/avatar05.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Betsy Madilyn",
                Email = "betsymad777@gmail.com",
                Address = "Street 3A, District 5",
                PhoneNumber = "12376129",
                ImagePath = "images/avatar06.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Read Ione",
                Email = "ioneread92@yahoo.co.uk",
                Address = "Street 2E, District 11",
                PhoneNumber = "87561001",
                ImagePath = "images/avatar07.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Lettie Jocelin",
                Email = "letty555@outlook.com",
                Address = "Street 1A, District 6",
                PhoneNumber = "75461093",
                ImagePath = "images/avatar08.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Jonny Gervase",
                Email = "jonnyger69@facebook.com",
                Address = "Street 3G, District 3",
                PhoneNumber = "65009828",
                ImagePath = "images/avatar09.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Quintella Keeleigh",
                Email = "quintessa@live.com",
                Address = "Street 2B, District 8",
                PhoneNumber = "45934349",
                ImagePath = "images/avatar10.jpg"
            });
            employeeList.ItemsSource = employees;
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            employees.Add(new Employee()
            {
                Fullname = "Jeffrey Christi",
                Email = "jeffchrist@gmail.com",
                Address = "Street 1A, District 1",
                PhoneNumber = "2358165",
                ImagePath = "images/avatar11.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Jeffrey Christi",
                Email = "jeffchrist@gmail.com",
                Address = "Street 1A, District 1",
                PhoneNumber = "2358165",
                ImagePath = "images/avatar11.jpg"
            });
            employees.Add(new Employee()
            {
                Fullname = "Jeffrey Christi",
                Email = "jeffchrist@gmail.com",
                Address = "Street 1A, District 1",
                PhoneNumber = "2358165",
                ImagePath = "images/avatar11.jpg"
            });
        }

        private void onUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            int index = employeeList.SelectedIndex;
            if (index != -1)
            {
                employees[index].Fullname = "Susanna Ebony";
                employees[index].Email = "susanne613@facebook.com";
                employees[index].Address = "Street 2E, District 3";
                employees[index].PhoneNumber = "85874784";
                employees[index].ImagePath = "images/avatar12.jpg";
            }
            else MessageBox.Show("Please choose an item to update!");
        }

        private void onDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            int index = employeeList.SelectedIndex;
            if (index != -1)
                employees.RemoveAt(index);
            else
                MessageBox.Show("Please choose an item to delete!");
        }
    }
}
