using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace CuteWpfControls.Converters
{
    /// <summary>
    /// true隐藏，false显示
    /// </summary>
    public class InverseBooleanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = System.Convert.ToBoolean(value);

            if (bValue)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
