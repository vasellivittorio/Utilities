using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Utilities
{
    class WindowManager
    {
        public static void SetTitleBar(string primaryColorKey, string secondaryColorKey)
        {
            var view = ApplicationView.GetForCurrentView();
            var primaryColor = (Application.Current.Resources[primaryColorKey] as SolidColorBrush).Color;
            var secondaryColor = (Application.Current.Resources[secondaryColorKey] as SolidColorBrush).Color;
            view.TitleBar.BackgroundColor = primaryColor;
            view.TitleBar.ButtonBackgroundColor = primaryColor;
            view.TitleBar.ButtonHoverBackgroundColor = secondaryColor;
        }
    }
}
