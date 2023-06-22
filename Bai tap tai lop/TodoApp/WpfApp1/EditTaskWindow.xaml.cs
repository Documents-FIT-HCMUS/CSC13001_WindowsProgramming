﻿using System;
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
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public TodoTask NewTodoTask { get; set; }

        public EditTaskWindow(TodoTask oldTodoTask)
        {
            InitializeComponent();
            NewTodoTask = (TodoTask) oldTodoTask.Clone();
        }
        private void OnSubmitButtonClick(object sender, RoutedEventArgs e)
        {
            if (editTextBox.Text.Length == 0)
                MessageBox.Show("Task cannot be empty!");
            else
            {
                NewTodoTask.TaskName = editTextBox.Text;
                DialogResult = true;
            }
        }
        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void EditTaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = NewTodoTask;
        }
    }
}
