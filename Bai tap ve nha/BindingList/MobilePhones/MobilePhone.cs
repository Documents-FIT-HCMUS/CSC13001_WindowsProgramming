using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePhones
{
    class MobilePhone : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        override public string ToString()
        {
            return $"{ImagePath} - {Name} {Manufacturer} {Price}";
        }
        public event PropertyChangedEventHandler? PropertyChanged = null;
    }
}
