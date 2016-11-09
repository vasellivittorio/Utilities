using Microsoft.OneDrive.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;
using Windows.Storage.Streams;

namespace VPassword.Utilities
{
    class OneDriveManager
    {
        public static IOneDriveClient oneDriveClient;
        public static async  Task Autenthicate()
        {

            oneDriveClient = OneDriveClientExtensions.GetClientUsingOnlineIdAuthenticator(new string[1] { "onedrive.readwrite" });

            await oneDriveClient.AuthenticateAsync();
        }

        public static async Task ReadPasswordFile(ObservableCollection<Models.Item> items)
        {
            UriBuilder builder = new UriBuilder("https://api.onedrive.com");
            builder.Path = "/v1.0/drive/root:/PassFile.txt";
            Uri url = builder.Uri;
            

            var contentStream = await oneDriveClient
                     .Drive
                     .Root
                     .ItemWithPath("SystemPlus.vpp")
                     .Content
                     .Request()
                     .GetAsync();
            StreamReader reader = new StreamReader(contentStream);
            string text = reader.ReadToEnd();
            var oldversionItems = JsonConvert.DeserializeObject<List<Models.Item>>(text);
            int j = 0;
            if ( (oldversionItems.Count + items.Count) >10 && !IAPManager.IsItemBought("Unlimited Items"))
            {
                await IAPManager.AskForUnlimitedItems();
                return;
            }
            else
            {
                
                foreach(var item in oldversionItems)
                {
                    j++;
                    items.Add(item);
                }
            }
            //System.Diagnostics.Debug.WriteLine(text);
            //text = decrypt(text);
            //System.Diagnostics.Debug.WriteLine(text);
            //var data = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //if (((data.Length/4) >= (10-items.Count)) && (!IAPManager.IsItemBought("Unlimited Items")))
            //{
            //    await IAPManager.AskForUnlimitedItems();
            //    return;
            //}
            //var j = 0;
            //for (int i = 0; i < data.Length; i += 4)
            //{
            //    var item = new Models.Item
            //    {
            //        Title = data[i],
            //        UserName = data[i + 1],
            //        Password = data[i + 2],
            //        Url = data[i + 3]
            //    };
            //    items.Add(item);
            //    j++;
            //}
            StorageManager.EncryptAndWrite(items, "system.dll");
            await Dialog.ShowMessage(String.Format(StringLoader.LoadString("PasswordsRecovered"), j));
        } 

        private static string decrypt(string passwords)
        {
            char[] result;
            result = new char[passwords.Length];

            for (int i = 0; i < passwords.Length; i++)
            {
                result[i] = passwords[i];
                for (int j = 0; j < 141091; j++)
                {
                    result[i]--;
                }  
                if(result[i] == 55530 || result[i] == 55527)
                {
                    result[i] = ' ';
                }
            }
            return new string(result);
        }

    }
}
