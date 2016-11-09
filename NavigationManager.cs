using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Utilities
{
    class NavigationManager
    {
        public static void NavigateTo(Type destinationPage)
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(destinationPage);
            Window.Current.Activate();
        }
    }
}
