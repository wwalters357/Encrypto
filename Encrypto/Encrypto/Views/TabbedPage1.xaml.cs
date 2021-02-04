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

            Cipher_Name.Text = cipher.Name;
        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            // Stores whatever is in Enter text box incase enter is not pressed
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

        private async void Encryption_OnClicked(object sender, EventArgs e)
        {
            if (cipher.Is_Initialized())
            { 
                Result_Message.Text = await Task.Run(() => cipher.Encrypt());
            }
            else
            {
                Result_Message.Text = "Invalid Cipher cannot encrypt!";
            }
        }

        private async void Decryption_OnClicked(object sender, EventArgs e)
        {
            if (cipher.Is_Initialized())
            {
                Result_Message.Text = await Task.Run(() => cipher.Decrypt());
            }
            else
            {
                Result_Message.Text = "Invalid Cipher cannot decrypt!";
            }
        }

        private void SwitchText_OnClicked(object sender, EventArgs e)
        {
            string temp = Input_Text.Text;
            Input_Text.Text = Result_Message.Text;
            Result_Message.Text = temp;
        }
    }
}