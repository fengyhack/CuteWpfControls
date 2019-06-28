using System;
using System.Globalization;
using System.Windows.Data;

namespace CuteWpfControls.Converters
{
    /// <summary>
    /// 判断值为否为0
    /// </summary>
    public class ValueIsZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string paraStr = System.Convert.ToString(parameter);
            switch (paraStr)
            {
                case "System.Windows.CornerRadius":
                    return CornerRadiusIsZero(value);
                default:
                    return false;
            }
        }

        private bool CornerRadiusIsZero(object value)
        {
            System.Windows.CornerRadius cornerRadus = (System.Windows.CornerRadius)value;
            if (cornerRadus.BottomLeft == 0 && cornerRadus.BottomRight == 0
                && cornerRadus.TopLeft == 0 && cornerRadus.TopRight == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
