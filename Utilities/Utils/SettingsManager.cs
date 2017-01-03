using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Utilities
{
    public class SettingsManager
    {
        
        public static void Save<T>(string key, T value)
        {
            var values = ApplicationData.Current.RoamingSettings.Values;
            if (values.ContainsKey(key))
                values.Remove(key);
            values.Add(key, value);
        }

        public static T Get<T>(string key)
        {
            var values = ApplicationData.Current.RoamingSettings.Values;
            return (T)values[key];
        }

        public static bool ContainsKey(string key)
        {
            var values = ApplicationData.Current.RoamingSettings.Values;
            return values.ContainsKey(key);
        }

        public static bool IsFirstStart()
        {
            if (ContainsKey(SettingsKeys.FIRSTSTART))
            {
                return false;
            }
            Save<bool>(SettingsKeys.FIRSTSTART, true);
            return true;
        }

    }
}
