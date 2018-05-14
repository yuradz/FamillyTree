using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FamilyTreeApp
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateTimeToYearStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {

            if (value == null)
                return " ? ";
            if ((DateTime)value == DateTime.MinValue)
                return " ...";
            return ((DateTime)value).Year.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
