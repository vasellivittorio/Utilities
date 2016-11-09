using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Utilities
{
    class StringLoader
    {
        public static string LoadString(string key)
        {
            var loader = ResourceLoader.GetForCurrentView();
            return loader.GetString(key);
        }
    }
}
