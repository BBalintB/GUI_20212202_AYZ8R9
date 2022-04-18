﻿using GUI_20212202_AYZ8R9.Models;
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
    public class EnumToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HeroTypes type = (HeroTypes)value;
            switch (type)
            {
                case HeroTypes.Archer:
                    return Brushes.LightPink;
                case HeroTypes.Assault:
                    return Brushes.Gray;
                case HeroTypes.Support:
                    return Brushes.LightBlue;
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
