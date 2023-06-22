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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<TodoTask> todoList = new BindingList<TodoTask>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            todoList.Add(new TodoTask("Clean the house"));
            todoList.Add(new TodoTask("Wash dishes"));
            todoList.Add(new TodoTask("Do math homework"));
            todoList.Add(new TodoTask("Paint the walls"));
            todoList.Add(new TodoTask("Replace car's tire"));
            
            todoListView.ItemsSource = todoList;
        }
        private void OnAddTaskButtonClick(object sender, RoutedEventArgs e)
        {
            var addTaskWindow = new AddTaskWindow();
            if (addTaskWindow.ShowDialog() == true)
            {
                todoList.Add(addTaskWindow.newTask);
            }
        }

        private void OnEditTaskButtonClick(object sender, RoutedEventArgs e)
        {
            var index = todoListView.SelectedIndex;

            if (index == -1)
                MessageBox.Show("Please choose an item to edit!");
            else
            {
                var editTaskWindow = new EditTaskWindow(todoList[index]);
                if (editTaskWindow.ShowDialog() == true)
                    todoList[index] = editTaskWindow.NewTodoTask;
            }
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var index = todoListView.SelectedIndex;

            if (index == -1)
                MessageBox.Show("Please choose an item to delete!");
            else
            {
                todoList.RemoveAt(index);
            }
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
