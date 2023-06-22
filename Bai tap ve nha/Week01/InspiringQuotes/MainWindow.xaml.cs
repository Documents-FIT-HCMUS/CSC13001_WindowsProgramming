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

namespace InspiringQuotes
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

        private void GenerateQuote_Click(object sender, RoutedEventArgs e)
        {
            string[] quotes = {
                "Don’t try to be perfect. Just try to be better than you were yesterday.",
                "You don’t always get what you wish for. But you always get what you work for.",
                "Strength doesn’t come from what you can do.\nIt comes from overcoming the things you once thought you couldn’t.",
                "Don’t wait for opportunity. Create it",
                "Be so good they can’t ignore you.",
                "It’s not about perfect. It’s about effort.",
                "Success is what comes after you stop making excuses.",
                "I’ve failed over and over and over again in my life and that is why I succeed.",
                "You have to be at your strongest when you’re feeling at your weakest.",
                "Push yourself because no one else is going to do it for you."
                };
            string[] authors = { "Unknown", "Unknown", "Rikki Rogers",  "Unknown", "Steve Martin", "Jillian Michaels",
                "Luis Galarza", "Michael Jordan", "Unknown", "Unknown"
                };
            string[] images = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" };

            var random = new Random();
            int randomIndex = random.Next(10);

            var bitmap = new BitmapImage(new Uri($"/assets/quote_{images[randomIndex]}.jpg", UriKind.Relative));
            
            quote.Content = quotes[randomIndex];
            author.Content = "- " + authors[randomIndex] + " -";
            quoteImage.Source = bitmap;

            // MessageBox.Show("Quote Generated");
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
