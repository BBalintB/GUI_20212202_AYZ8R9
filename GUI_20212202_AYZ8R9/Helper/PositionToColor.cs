using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GUI_20212202_AYZ8R9.Helper
{
    public class PositionToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoundPosition position = (RoundPosition)value;
            switch (position)
            {
                case RoundPosition.Neutral:
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#00FFFFFF");
                    return brush;
                case RoundPosition.Attack:
                    return Brushes.LightPink;
                case RoundPosition.Heal:
                    return Brushes.LightBlue;
                case RoundPosition.Special:
                    return Brushes.LightGreen;
                default:
                    return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
