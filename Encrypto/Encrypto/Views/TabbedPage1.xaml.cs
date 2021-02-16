using Encrypto.Models;
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
        private Cipher cipher = null;

        public TabbedPage1(Cipher_Type cipherType)
        {
            InitializeComponent();

            switch (cipherType)
            {
                case Cipher_Type.Caesar:
                    cipher = new Caesar_Cipher("", "", cipherType);
                    break;
                case Cipher_Type.Double_Caesar:
                    cipher = new Caesar_Cipher("", "", cipherType);
                    break;
                case Cipher_Type.Monoalphabetic:
                    cipher = new Monoalphabetic_Cipher("", "");
                    break;
                case Cipher_Type.Homophonic:
                    cipher = new Homophonic_Cipher("", "");
                    break;
                case Cipher_Type.Hill:
                    cipher = new Hill_Cipher("", "");
                    break;
                case Cipher_Type.Vernam:
                    cipher = new Vernam_Cipher("");
                    Input_Key.IsVisible = false;
                    break;
                default:
                    break;
            }

            // Set name of Cipher on top of tabbed page 1
            Cipher_Name.Text = cipher.Name;

            // Set image source of Cipher on tabbed page 2
            Cipher_Image.Source = cipher.Image;

            // Set history of Cipher on tabbed page 3
            Cipher_History.Text = cipher.History;
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
            if (cipher.Is_Initialized())
            {
                try
                {
                    // This line will yield control to the UI while Encrypt()
                    // performs its work. The UI thread is free to perform other work.
                    Result_Message.Text = await Task.Run(() => cipher.Encrypt());
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
            if (cipher.Is_Initialized())
            {
                try
                {
                    // This line will yield control to the UI while Decrypt()
                    // performs its work. The UI thread is free to perform other work.
                    Result_Message.Text = await Task.Run(() => cipher.Decrypt());
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