using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo
{
    public class TodoTask : INotifyPropertyChanged
    {
        public string TaskName { get; set; }
        public TodoTask(string taskName)
        {
            this.TaskName = taskName;
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
