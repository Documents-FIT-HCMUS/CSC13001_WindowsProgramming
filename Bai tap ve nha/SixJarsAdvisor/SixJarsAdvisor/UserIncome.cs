using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SixJarsAdvisor
{
    public class UserIncome
    {
        public int Income { get; set; }
        public float Part1 { get; set; }
        public float Part2 { get; set; }
        public float Part3 { get; set; }
        public UserIncome(int income)
        {
            Income = income;
            Part1 = Income * 0.55f;
            Part2 = Income * 0.10f;
            Part3 = Income * 0.05f;
        }
    }
    public class CurrencyConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float income = (float)value;
            CultureInfo cultureInfo = new CultureInfo("vi");
            string result = String.Format(cultureInfo, "{0:N0} đ", income);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
