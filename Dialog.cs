using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace Utilities
{
    class Dialog
    {
        private static bool showing = false; 

        public static async Task ShowMessage(string message)
        {
            var dialog = new MessageDialog(message);

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            {
                if (!showing)
                {
                    showing = true;
                    await dialog.ShowAsync();
                    showing = false;
                }                                                
            });
        }

        public static async Task<int> AskQuestion(string question, string firstChoice, string secondChoice)
        {
            if (!showing)
            {
                showing = true;
                var dialog = new MessageDialog(question);
                dialog.Commands.Add(new UICommand { Label = firstChoice, Id = 0 });
                dialog.Commands.Add(new UICommand { Label = secondChoice, Id = 1 });
                var response = await dialog.ShowAsync();
                showing = false;
                return (int)response.Id;            
            }
            return -1; 
        }
    }
}
