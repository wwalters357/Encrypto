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

            // Set keyboard input type based on Cipher
            if (cipher.Type == Cipher_Type.Caesar)
            {
                Input_Key.Keyboard = Keyboard.Numeric;
            }
            else
            {
                Input_Key.Keyboard = Keyboard.Default;
            }

            // Hill Cipher needs two images
            if (cipher.Type == Cipher_Type.Hill)
            {
                Cipher_Image_2.IsVisible = true;
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
        }

        // Parse plaintext and output converted ciphertext.
        private async void Encryption_OnClicked(object sender, EventArgs e)
        {
            if (cipher.Is_Initialized)
            {
                try
                {
                    // This line will yield control to the UI while Encrypt()
                    // performs its work. The UI thread is free to perform other work.
                    Result_Message.Text = await Task.Run(() => cipher.Encrypt);
                }
                catch(Exception)
                {
                    Result_Message.Text = "Invalid Key Entered";
                }
            }
            else
            {
                Result_Message.Text = "Invalid Cipher cannot encrypt!";
            }
        }

        // Parse ciphertext and output converted plaintext.
        private async void Decryption_OnClicked(object sender, EventArgs e)
        {
            if (cipher.Is_Initialized)
            {
                try
                {
                    // This line will yield control to the UI while Decrypt()
                    // performs its work. The UI thread is free to perform other work.
                    Result_Message.Text = await Task.Run(() => cipher.Decrypt);
                }
                catch(Exception)
                {
                    Result_Message.Text = "Invalid Key Entered";
                }
            }
            else
            {
                Result_Message.Text = "Invalid Cipher cannot decrypt!";
            }
        }

        // Swap text between Editors Input_text and Result_Message.
        private void SwitchText_OnClicked(object sender, EventArgs e)
        {
            string temp = Input_Text.Text;
            Input_Text.Text = Result_Message.Text;
            Result_Message.Text = temp;
        }

        // --------------------------------------------------------------------
        // -----------------------Tabbed Page 2--------------------------------
        // --------------------------------------------------------------------
    }
}