using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    class Employee : INotifyPropertyChanged
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        override public string ToString()
        {
            return $"{ImagePath} - {Fullname} - {Email} - {PhoneNumber}";
        }
        public event PropertyChangedEventHandler? PropertyChanged = null;
    }
}
