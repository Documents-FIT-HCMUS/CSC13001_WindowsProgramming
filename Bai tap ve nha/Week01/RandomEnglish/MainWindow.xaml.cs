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

namespace RandomEnglish
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

        private void GenerateWord_Click(object sender, RoutedEventArgs e)
        {
            string[] words = { "ball", "bed", "car", "computer", "door", "eraser", "horse", "orange", "sofa", "window" };

            var random = new Random();
            int randomIndex = random.Next(10);

            var bitmap = new BitmapImage(new Uri($"/assets/{words[randomIndex]}.jpg", UriKind.Relative));

            generatedWord.Content = words[randomIndex];
            wordImage.Source = bitmap;
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
