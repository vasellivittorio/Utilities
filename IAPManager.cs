using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPassword;
using Windows.ApplicationModel.Store;

namespace Utilities
{
    class IAPManager
    {
        public static bool IsItemBought(string productID)
        {
            try
            {
                LicenseInformation licenseInformation = CurrentApp.LicenseInformation;
                var a = licenseInformation.ProductLicenses[productID].IsActive;
                return a;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
                
            
        }

        public static async Task<bool> Buy(string productID)
        {
            try
            {
                await CurrentApp.RequestProductPurchaseAsync(productID, false);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static async Task AskForUnlimitedItems()
        {
            var response = await Dialog.AskQuestion(StringLoader.LoadString("BuyUnlimitedItems"),
                                                                StringLoader.LoadString("Yes"),
                                                                StringLoader.LoadString("No"));
            if (response == 0)
                await Buy("Unlimited Items");
        }
    }
}
