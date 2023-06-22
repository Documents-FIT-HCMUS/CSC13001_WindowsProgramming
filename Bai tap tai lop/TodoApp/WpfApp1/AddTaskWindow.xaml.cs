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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public TodoTask newTask;
        public AddTaskWindow()
        {
            InitializeComponent();
        }
        private void OnSubmitButtonClick(object sender, RoutedEventArgs e)
        {
            string newTaskName = addTextBox.Text;
            if (newTaskName.Length == 0)
                MessageBox.Show("Task cannot be empty!");
            else
            {
                newTask = new TodoTask(newTaskName);
                DialogResult = true;
            } 
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
