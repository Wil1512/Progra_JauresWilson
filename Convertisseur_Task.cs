using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Progra_JauresWilson
{
    public class Convertisseur_Task : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isDone = value is bool b && b;

            if (targetType == typeof(Brush))
            {
                return isDone ? Brushes.Gray : Brushes.Black;
            }

            if (targetType == typeof(TextDecorationCollection))
            {
                return isDone ? TextDecorations.Strikethrough : new TextDecorationCollection();
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
