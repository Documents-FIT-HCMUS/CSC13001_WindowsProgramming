using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Books
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private int _addCount = 0; 
        BindingList<Book> books = new BindingList<Book>();
        List<Book> newBooks = new List<Book>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initial book list
            books.Add(new Book()
            {
                Name = "The Lost Apothecary",
                ImagePath = "images/book01.jpg",
                Author = "Sarah Penner",
                PublishYear = 2021
            });
            books.Add(new Book()
            {
                Name = "The Beauty of Living Twice",
                ImagePath = "images/book02.jpg",
                Author = "Sharon Stone",
                PublishYear = 2021
            });
            books.Add(new Book()
            {
                Name = "Of Women and Salt",
                ImagePath = "images/book03.jpg",
                Author = "Grabriela Garcia",
                PublishYear = 2021
            });
            books.Add(new Book()
            {
                Name = "Unsettled Ground",
                ImagePath = "images/book04.jpg",
                Author = "Claire Fuller",
                PublishYear = 2021
            });
            books.Add(new Book()
            {
                Name = "Summerwater",
                ImagePath = "images/book05.jpg",
                Author = "Sarah Moss",
                PublishYear = 2021
            });
            books.Add(new Book()
            {
                Name = "Frankenstein",
                ImagePath = "images/book06.jpg",
                Author = "Mary Shelley",
                PublishYear = 1818
            });
            books.Add(new Book()
            {
                Name = "Foundation",
                ImagePath = "images/book07.jpg",
                Author = "Isaac Asimov",
                PublishYear = 1951
            });
            books.Add(new Book()
            {
                Name = "Ice",
                ImagePath = "images/book08.jpg",
                Author = "Anna Kavan",
                PublishYear = 1967
            });
            books.Add(new Book()
            {
                Name = "The Martian",
                ImagePath = "images/book09.jpg",
                Author = "Andy Weir",
                PublishYear = 2015
            });
            books.Add(new Book()
            {
                Name = "The Power",
                ImagePath = "images/book10.jpg",
                Author = "Naomi Alderman",
                PublishYear = 2016
            });

            // Additional book list
            newBooks.Add(new Book()
            {
                Name = "Kindred",
                ImagePath = "images/book11.jpg",
                Author = "Octavia E. Butler",
                PublishYear = 1979
            });
            newBooks.Add(new Book()
            {
                Name = "Hyperion",
                ImagePath = "images/book12.jpg",
                Author = "Dan Simmons",
                PublishYear = 1989
            });
            newBooks.Add(new Book()
            {
                Name = "Jurassic Park",
                ImagePath = "images/book13.jpg",
                Author = "Michael Crichton",
                PublishYear = 1990
            });
            newBooks.Add(new Book()
            {
                Name = "Metro 2033",
                ImagePath = "images/book14.jpg",
                Author = "Dmitry Glukhovsky",
                PublishYear = 2002
            });
            newBooks.Add(new Book()
            {
                Name = "Neuromancer",
                ImagePath = "images/book15.jpg",
                Author = "William Gibson",
                PublishYear = 1984
            });


            bookList.ItemsSource = books;
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            // Add 1 book at a time, max is 5
            if (_addCount < 5)
            {
                books.Add(newBooks[_addCount]);
                _addCount += 1;
            }
            else MessageBox.Show("Out of book to add! Sorry!");
        }

        private void onUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            int index = bookList.SelectedIndex;
            if (index != -1)
            {
                books[index].Name = "Oryx and Crake";
                books[index].Author = "Margaret Atwood";
                books[index].ImagePath = "images/book16.jpg";
                books[index].PublishYear = 2003;
            }
            else MessageBox.Show("Please choose an item to update!");
        }

        private void onDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            int index = bookList.SelectedIndex;
            if (index != -1) 
                books.RemoveAt(index);
            else 
                MessageBox.Show("Please choose an item to delete!");
        }
        

    }
}
