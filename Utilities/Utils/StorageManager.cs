using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPassword.Utilities;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Utilities
{
    class StorageManager
    {

        public static async void Write<T>(T objectToSave, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(objectToSave);
            var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var datawriter = new DataWriter(stream))
                {
                    datawriter.WriteString(jsonString);
                    await datawriter.StoreAsync();
                }
            }
            var properties = await file.GetBasicPropertiesAsync();
            System.Diagnostics.Debug.WriteLine(properties.Size);
        }

        public static async void EncryptAndWrite<T>(T objectToSave, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(objectToSave);
            jsonString = AESEncryption.Encrypt(jsonString, "key");
            var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var datawriter = new DataWriter(stream))
                {
                    datawriter.WriteString(jsonString);
                    await datawriter.StoreAsync();
                }
            }
            var properties = await file.GetBasicPropertiesAsync();
            System.Diagnostics.Debug.WriteLine(properties.Size);
        }

        public static async Task<T> Read<T>(string fileName)
        {
            string jsonString;
            var file = await ApplicationData.Current.RoamingFolder.TryGetItemAsync(fileName) as StorageFile;
            if (file == null)
                return default(T);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            using (var dataReader = new DataReader(stream.GetInputStreamAt(0)))
            {
                uint bytesLoaded = await dataReader.LoadAsync((uint)stream.Size);
                jsonString = dataReader.ReadString(bytesLoaded);
            }
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static async Task<T> ReadAndDecrypt<T>(string fileName)
        {
            string jsonString;
            var file = await ApplicationData.Current.RoamingFolder.TryGetItemAsync(fileName) as StorageFile;
            if (file == null)
                return default(T);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            using (var dataReader = new DataReader(stream.GetInputStreamAt(0)))
            {
                uint bytesLoaded = await dataReader.LoadAsync((uint)stream.Size);
                jsonString = dataReader.ReadString(bytesLoaded);
            }
            jsonString = AESEncryption.Decrypt(jsonString, "key");
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
