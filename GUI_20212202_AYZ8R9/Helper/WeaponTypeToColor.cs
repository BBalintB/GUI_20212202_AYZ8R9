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
    public class WeaponTypeToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponType tp = (WeaponType)value;
            switch (tp)
            {
                case WeaponType.uncommon:
                    return Brushes.LightGray;
                case WeaponType.common:
                    return Brushes.LightGreen;
                case WeaponType.rare:
                    return Brushes.LightBlue;
                case WeaponType.epic:
                    return Brushes.Purple;
                case WeaponType.legendary:
                    return Brushes.Gold;
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
