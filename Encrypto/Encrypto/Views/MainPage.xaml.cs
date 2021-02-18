using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Encrypto
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            Cipher_Type type = Cipher_Type.Caesar;
            string cipher = "";
            if (sender is Button)
            {
                cipher = ((Button)sender).Text;
            }
            else if (sender is Picker)
            {
                var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;
                cipher = picker.Items[selectedIndex];
            }

            // Determines which cipher to initialize on the tabbed pages.
            switch (cipher)
            {
                case "Caesar Cipher":
                    type = Cipher_Type.Caesar;
                    break;
                case "Double Caesar Cipher":
                    type = Cipher_Type.Vigenere;
                    break;
                case "Monoalphabetic Cipher":
                    type = Cipher_Type.Monoalphabetic;
                    break;
                case "Homophonic Cipher":
                    type = Cipher_Type.Homophonic;
                    break;
                case "Hill Cipher":
                    type = Cipher_Type.Hill;
                    break;
                case "Vernam Cipher":
                    type = Cipher_Type.Vernam;
                    break;
                default:
                    break;
            }
            await Navigation.PushAsync(new TabbedPage1(type));
        }
    }
}
