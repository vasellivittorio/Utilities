using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Utilities
{
    class WindowsHelloManager
    {
        public static async Task<bool> CreatePassportKeyAsync(string accountId)
        {
            KeyCredentialRetrievalResult keyCreationResult = await KeyCredentialManager.RequestCreateAsync(accountId, KeyCredentialCreationOption.ReplaceExisting);
            switch (keyCreationResult.Status)
            {
                case KeyCredentialStatus.Success:
                    Debug.WriteLine("Successfully made key");
                    return true;
                case KeyCredentialStatus.UserCanceled:
                    Debug.WriteLine("User cancelled sign-in process.");
                    break;
                case KeyCredentialStatus.NotFound:
                    Debug.WriteLine("Microsoft Passport is not setup!\nPlease go to Windows Settings and set up a PIN to use it.");
                    break;
                default:
                    break;
            }

            return false;
        }

        public static async Task<bool> IsSupported()
        {
            var result = await KeyCredentialManager.IsSupportedAsync();
            return result;
        }
    }
}
