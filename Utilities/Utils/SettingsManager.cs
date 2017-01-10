using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace Utilities
{
    public class SettingsManager
    {
        

        public static void SaveLocal<T>(string key, T value)
        {
            var values = ApplicationData.Current.LocalSettings.Values;
            Save<T>(key, value, values);
        }

        public static void SaveInRoaming<T>(string key, T value)
        {
            var values = ApplicationData.Current.RoamingSettings.Values;
            Save<T>(key, value, values);
        }

        private static void Save<T>(string key, T value, IPropertySet values)
        {
            if (values.ContainsKey(key))
                values.Remove(key);
            values.Add(key, value);
        }

        public static T GetLocal<T>(string key)
        {
            var values = ApplicationData.Current.LocalSettings.Values;
            return (T)values[key];
        }

        public static T GetInRoaming<T>(string key)
        {
            var values = ApplicationData.Current.LocalSettings.Values;
            return (T)values[key];
        }

        public static bool ContainsKeyLocal(string key)
        {
            var values = ApplicationData.Current.LocalSettings.Values;
            return values.ContainsKey(key);
        }

        public static bool ContainsKeyInRoaming(string key)
        {
            var values = ApplicationData.Current.RoamingSettings.Values;
            return values.ContainsKey(key);
        }

        public static bool IsFirstStart()
        {
            if (ContainsKeyLocal(SettingsKeys.FIRSTSTART))
            {
                return false;
            }
            SaveLocal<bool>(SettingsKeys.FIRSTSTART, true);
            return true;
        }

    }
}
