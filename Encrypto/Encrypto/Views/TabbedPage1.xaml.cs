using Encrypto.Models;
using Encrypto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Encrypto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        private static CipherViewModel cipher = null;

        public TabbedPage1(Cipher_Type cipherType)
        {
            InitializeComponent();

            cipher = new CipherViewModel(cipherType);

            // Bind Cipher ViewModel object to tabbed page
            this.BindingContext = cipher;

            // Set IsBusy to false on inital page load.
            cipher.IsBusy = false;

            // Set page elements based on Cipher type
            switch (cipher.Type)
            {
                case Cipher_Type.Caesar:
                    Input_Key.Keyboard = Keyboard.Numeric;
                    Input_Key.MaxLength = 2;
                    break;
                case Cipher_Type.Hill:
                    Input_Key.MaxLength = 4;
                    break;
                case Cipher_Type.Homophonic:
                    Input_Key.Text = "21,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
                    break;
                case Cipher_Type.Monoalphabetic:
                    break;
                case Cipher_Type.Vernam:
                    Input_Key.IsVisible = false;
                    break;
                case Cipher_Type.Vigenere:
                    break;
            }
        }

        // --------------------------------------------------------------------
        // -----------------------Tabbed Page 1--------------------------------
        // --------------------------------------------------------------------

        // Navigation back button to return to the home page.
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        // Stores message/key while the user types each letter.
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            // Stores whatever is in Enter text box incase enter is not pressed.
            if (((Editor)sender).ClassId.Equals("Input_Text"))
            {
                cipher.Message = newText;
            }
            else if (((Editor)sender).ClassId.Equals("Input_Key"))
            {
                cipher.Key = newText;
            }
        }

        // User enters a string for either text or key and presses enter.
        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;
            if (((Editor)sender).ClassId.Equals("Input_Text"))
            {
                cipher.Message = text;
            }
            else if (((Editor)sender).ClassId.Equals("Input_Key"))
            {
                cipher.Key = text;
            }
            IsBusy = true;
        }

        // Parse plaintext and output converted ciphertext.
        private async void Encryption_OnClicked(object sender, EventArgs e)
        {
            
            if (cipher.Is_Initialized)
            {
                try
                {
                    // Activate loading indicator
                    Loading();

                    // This line will yield control to the UI while Encrypt()
                    // performs its work. The UI thread is free to perform other work.
                    Result_Message.Text = await Task.Run(() => cipher.Encrypt);             
                }
                catch (Exception)
                {
                    Result_Message.Text = "Invalid Key Entered";
                }

                // Deactivate loading indicator
                Loading();
            }
            else
            {
                Result_Message.Text = "Invalid Cipher cannot encrypted!";
            }
        }

        // Parse ciphertext and output converted plaintext.
        private async void Decryption_OnClicked(object sender, EventArgs e)
        {
            if (cipher.Is_Initialized)
            {
                try
                {
                    // Activate loading indicator
                    Loading();

                    // This line will yield control to the UI while Decrypt()
                    // performs its work. The UI thread is free to perform other work.
                    Result_Message.Text = await Task.Run(() => cipher.Decrypt);
                }
                catch (Exception E)
                {
                    Result_Message.Text = E.Message;
                }

                // Deactivate loading indicator
                Loading();
            }
            else
            {
                Result_Message.Text = "Invalid Cipher cannot decrypted!";
            }
        }

        // Swap text between Editors Input_text and Result_Message.
        private void SwitchText_OnClicked(object sender, EventArgs e)
        {
            string temp = Input_Text.Text;
            Input_Text.Text = Result_Message.Text;
            Result_Message.Text = temp;
        }

        private void Loading()
        {
            bool load = !cipher.IsBusy;
            Loader.IsEnabled = load;
            Loader.IsRunning = load;
            Loader.IsVisible = load;
            cipher.IsBusy    = load;
        }

        // --------------------------------------------------------------------
        // -----------------------Tabbed Page 2--------------------------------
        // --------------------------------------------------------------------
    }
}