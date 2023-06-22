using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBindingNotify
{
    class Student : INotifyPropertyChanged
    {
        private string _id;
        private string _fullname;
        private string _address;
        private string _email;
        private string _phoneNumber;
        private string _avatar;

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }
        public string Fullname
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fullname"));
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Address"));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }
        public string Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Avatar"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
