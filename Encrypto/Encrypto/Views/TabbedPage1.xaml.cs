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

        // --------------------------------------------------------------------
        // -----------------------Tabbed Page 2--------------------------------
        // --------------------------------------------------------------------
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Init start position for parallax elements
            /*CityImage.WidthRequest = width;
            CityImage.TranslationY = ParalaxContainer.Height -
                                     CityImage.Height;
            CityImage.TranslationX = 0;

            ShadowCityImage.WidthRequest = width;
            ShadowCityImage.TranslationY =
                _parallaxShadowCityStartY = ParalaxContainer.Height -
                                            ShadowCityImage.Height - 30;
            ShadowCityImage.TranslationX = 0;

            SkyscrapersImage.WidthRequest = width;
            SkyscrapersImage.TranslationY =
                _parallaxSkyscrapersStartY =
                    _parallaxShadowCityStartY = ParalaxContainer.Height -
                                                SkyscrapersImage.Height - 30;
            SkyscrapersImage.TranslationX = 0;

            CloudMediumImage.TranslationX = ParalaxContainer.Width -
                                            CloudMediumImage.Width - 20;
            CloudMediumImage.TranslationY = _parallaxCloudMediumStartY = 60;

            _parallaxCloudBiggestStartY = CloudBiggestImage.TranslationY;
            _parallaxCloudSmallStartY = CloudSmallImage.TranslationY;*/

            ParallaxLabel.TranslationX = ParalaxContainer.Width / 2
                                         - ParallaxLabel.Width / 2;
            ParallaxLabel.TranslationY =
                _parallaxLabelStartY = ParalaxContainer.Height / 2
                                       - ParallaxLabel.Height / 2;
        }

        private void ParallaxScrollViewOnScrolled(object sender, ScrolledEventArgs e)
        {
            ParalaxTextAnimation(e.ScrollY);
            //ParalaxShadowCityAnimation(e.ScrollY);
            //ParalaxSkyscrapersAnimation(e.ScrollY);
            //ParalaxCloudBiggestAnimation(e.ScrollY);
            //ParalaxCloudMediumAnimation(e.ScrollY);
            //ParalaxCloudSmallAnimation(e.ScrollY);
        }

        private void ParalaxTextAnimation(double scrollY)
        {
            ParalaxAnimation(ParallaxLabel, scrollY, _parallaxLabelStartY, 0,
                ParalaxContainer.HeightRequest - ParallaxLabel.Height, TextSpeed);
        }

        /*private void ParalaxShadowCityAnimation(double scrollY)
        {
            ParalaxAnimation(ShadowCityImage, scrollY, _parallaxShadowCityStartY, 0,
                ParalaxContainer.HeightRequest - ShadowCityImage.Height, ShadowSpeed);
        }

        private void ParalaxSkyscrapersAnimation(double scrollY)
        {
            ParalaxAnimation(SkyscrapersImage, scrollY, _parallaxSkyscrapersStartY, 0,
                ParalaxContainer.HeightRequest - SkyscrapersImage.Height, SkyscrapersSpeed);
        }

        private void ParalaxCloudBiggestAnimation(double scrollY)
        {
            ParalaxAnimation(CloudBiggestImage, scrollY, _parallaxCloudBiggestStartY, 20,
                ParalaxContainer.HeightRequest - CloudBiggestImage.Height, CloudBiggestSpeed);
        }

        private void ParalaxCloudMediumAnimation(double scrollY)
        {
            ParalaxAnimation(CloudMediumImage, scrollY, _parallaxCloudMediumStartY, 0,
                ParalaxContainer.HeightRequest - CloudMediumImage.Height, CloudMediumSpeed);
        }

        private void ParalaxCloudSmallAnimation(double scrollY)
        {
            ParalaxAnimation(CloudSmallImage, scrollY, _parallaxCloudSmallStartY, 30,
                ParalaxContainer.HeightRequest - CloudSmallImage.Height - CloudMediumImage.Height, CloudSmallSpeed);
        }*/

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