using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AccountingForTouristTrips.View;

namespace AccountingForTouristTrips.Helper
{
    [ValueConversion(typeof(string), typeof(string))]
    public class PasswordToHashConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AuthorizationWindow.HashPassword((string) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
