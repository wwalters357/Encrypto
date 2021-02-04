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
        private const double TextSpeed = 0.5;
        private const double ShadowSpeed = 0.2;
        private const double SkyscrapersSpeed = 0.4;
        private const double CloudBiggestSpeed = 0.35;
        private const double CloudMediumSpeed = 0.6;
        private const double CloudSmallSpeed = 0.7;

        // double Y value for each page element.
        private double _parallaxLabelStartY;
        private double _parallaxShadowCityStartY;
        private double _parallaxSkyscrapersStartY;
        private double _parallaxCloudBiggestStartY;
        private double _parallaxCloudMediumStartY;
        private double _parallaxCloudSmallStartY;

        public TabbedPage1(Cipher_Type cipherType)
        {
            InitializeComponent();

            ParallaxScrollView.Scrolled += ParallaxScrollViewOnScrolled;

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
                // This line will yield control to the UI while Encrypt()
                // performs its work. The UI thread is free to perform other work.
                Result_Message.Text = await Task.Run(() => cipher.Encrypt());
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
                // This line will yield control to the UI while Decrypt()
                // performs its work. The UI thread is free to perform other work.
                Result_Message.Text = await Task.Run(() => cipher.Decrypt());
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
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Init start position for parallax elements.
            ParallaxLabel.TranslationX = ParalaxContainer.Width / 2
                                         - ParallaxLabel.Width / 2;
            ParallaxLabel.TranslationY =
                _parallaxLabelStartY = ParalaxContainer.Height / 2
                                       - ParallaxLabel.Height / 2;
        }

        private void ParallaxScrollViewOnScrolled(object sender, ScrolledEventArgs e)
        {
            ParalaxTextAnimation(e.ScrollY);
        }

        private void ParalaxTextAnimation(double scrollY)
        {
            ParalaxAnimation(ParallaxLabel, scrollY, _parallaxLabelStartY, 0,
                ParalaxContainer.HeightRequest - ParallaxLabel.Height, TextSpeed);
        }

        private void ParalaxAnimation(View control,
                                      double scrollY,
                                      double startPosition,
                                      double minPosition,
                                      double maxPosition,
                                      double speed)
        {
            var newPosition = startPosition + scrollY * speed;
            if (newPosition > minPosition && newPosition < maxPosition)
                control.TranslationY = newPosition;
        }
    }
}