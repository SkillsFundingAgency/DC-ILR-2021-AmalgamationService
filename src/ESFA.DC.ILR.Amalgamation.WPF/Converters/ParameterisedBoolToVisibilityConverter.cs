using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ESFA.DC.ILR.Amalgamation.WPF.Converters
{
    public class ParameterisedBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = int.Parse(parameter.ToString());
            var val = (bool)value;
            if (param == 0 && val == false)
            {
                return Visibility.Visible;
            }
            else if (param == 1 && val == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
