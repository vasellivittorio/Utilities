using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Utilities.Converters
{
    public class CollapseIfEqualToFloat : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var par = float.Parse((string)parameter);
            if((float)value == par)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
