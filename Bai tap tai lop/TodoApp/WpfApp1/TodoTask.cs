using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TodoTask : INotifyPropertyChanged, ICloneable
    {
        public string TaskName { get; set; }
        public TodoTask(string taskName)
        {
            this.TaskName = taskName;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
