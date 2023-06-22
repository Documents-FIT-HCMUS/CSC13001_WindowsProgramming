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

namespace MobilePhones
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        BindingList<MobilePhone> phones = new BindingList<MobilePhone>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            phones.Add(new MobilePhone()
            {
                Name = "iPhone 13 Pro Max",
                Manufacturer = "Apple",
                Price = 1299,
                ImagePath = "images/phone01.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "iPhone 13 Pro",
                Manufacturer = "Apple",
                Price = 999,
                ImagePath = "images/phone02.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "iPhone 13",
                Manufacturer = "Apple",
                Price = 799,
                ImagePath = "images/phone03.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "iPhone 13 Mini",
                Manufacturer = "Apple",
                Price = 699,
                ImagePath = "images/phone04.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Samsung Galaxy Note 20 Ultra",
                Manufacturer = "Samsung",
                Price = 949,
                ImagePath = "images/phone05.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Samsung Galaxy S21 Ultra",
                Manufacturer = "Samsung",
                Price = 999,
                ImagePath = "images/phone06.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Samsung Galaxy S21+",
                Manufacturer = "Samsung",
                Price = 899,
                ImagePath = "images/phone07.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Samsung Galaxy S20 Ultra",
                Manufacturer = "Samsung",
                Price = 799,
                ImagePath = "images/phone08.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Xiaomi Mi 11 5G",
                Manufacturer = "Xiaomi",
                Price = 699,
                ImagePath = "images/phone09.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Xiaomi Mi 10T Pro",
                Manufacturer = "Xiaomi",
                Price = 549,
                ImagePath = "images/phone10.jpg"
            });
            phoneList.ItemsSource = phones;
        }
        private void onAddButtonClick(object sender, RoutedEventArgs e)
        { 
            phones.Add(new MobilePhone()
            {
                Name = "Realme 8",
                Manufacturer = "Realme",
                Price = 349,
                ImagePath = "images/phone11.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Realme 8 Pro",
                Manufacturer = "Realme",
                Price = 399,
                ImagePath = "images/phone11.jpg"
            });
            phones.Add(new MobilePhone()
            {
                Name = "Realme 8 Pro",
                Manufacturer = "Realme",
                Price = 399,
                ImagePath = "images/phone11.jpg"
            });
        }

        private void onUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            int index = phoneList.SelectedIndex;
            if (index != -1)
            {
                phones[index].Name = "Vsmart Aris Pro";
                phones[index].Manufacturer = "Vsmart";
                phones[index].Price = 299;
                phones[index].ImagePath = "images/phone12.jpg";
            }
            else MessageBox.Show("Please choose an item to update!");
        }

        private void onDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            int index = phoneList.SelectedIndex;
            if (index != -1)
                phones.RemoveAt(index);
            else 
                MessageBox.Show("Please choose an item to delete!");
        }

        
    }
}
