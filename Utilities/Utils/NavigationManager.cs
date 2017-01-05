using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Utilities
{
    public class NavigationManager
    {

        public static void Navigate<T>()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(T));
            Window.Current.Activate();
        }

        public static void GoBack(int times = 1)
        {
            var frame = Window.Current.Content as Frame;
            for(int i = 0; i < times;i++)
            {
                frame.GoBack();
            }
            Window.Current.Activate();
        }
    }
}
