using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Forms = System.Windows.Forms;
using ClientApp.Resources;
using AstroProto;
using Grpc.Net.Client;

namespace ClientApp
{
    public partial class MainWindow : Window
    {
        private const string GrpcServerAddress = "http://localhost:5000";

        private Brush _defaultBackground = Brushes.White;
        private Brush _defaultTextForeground = Brushes.Black;
        private Brush _defaultButtonBackground = Brushes.LightGray;
        private FontFamily _defaultFontFamily = new FontFamily("Segoe UI");
        private double _defaultFontSize = 13;

        public MainWindow()
        {
            InitializeComponent();
            ApplyResources();
            ApplyDefaultTheme();
        }

        private AstroService.AstroServiceClient CreateGrpcClient()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(GrpcServerAddress);
            return new AstroService.AstroServiceClient(channel);
        }

        private void ApplyResources()
        {
            Title = Strings.AppTitle;
            txtMainTitle.Text = Strings.AppTitle;

            menuLanguage.Header = Strings.MenuLanguage;
            menuEnglish.Header = Strings.MenuEnglish;
            menuFrench.Header = Strings.MenuFrench;
            menuGerman.Header = Strings.MenuGerman;

            menuTheme.Header = Strings.MenuTheme;
            menuFont.Header = Strings.MenuFont;

            menuCustomise.Header = Strings.MenuCustomise;
            menuBackgroundColour.Header = Strings.MenuBackgroundColour;
            menuLabelColour.Header = Strings.MenuLabelColour;
            menuTextBoxColour.Header = Strings.MenuTextBoxColour;
            menuAbout.Header = Strings.MenuAbout;
            menuAboutApp.Header = Strings.MenuAboutApp;

            menuLightTheme.Header = Strings.MenuLightTheme;
            menuDarkTheme.Header = Strings.MenuDarkTheme;
            menuBlueTheme.Header = Strings.MenuBlueTheme;

            grpVelocity.Header = Strings.GrpVelocity;
            grpDistance.Header = Strings.GrpDistance;
            grpTemperature.Header = Strings.GrpTemperature;
            grpRadius.Header = Strings.GrpRadius;

            btnVelocity.Content = Strings.BtnCalculateVelocity;
            btnDistance.Content = Strings.BtnCalculateDistance;
            btnTemperature.Content = Strings.BtnConvertTemperature;
            btnRadius.Content = Strings.BtnCalculateHorizon;

            lblObserved.Text = Strings.LblObserved;
            lblRest.Text = Strings.LblRest;
            lblVelocity.Text = Strings.LblVelocity;
            lblParallax.Text = Strings.LblParallax;
            lblParsecs.Text = Strings.LblParsecs;
            lblLightYears.Text = Strings.LblLightYears;
            lblKm.Text = Strings.LblKm;
            lblCelsius.Text = Strings.LblCelsiusFull;
            lblKelvin.Text = Strings.LblKelvin;
            lblMass.Text = Strings.LblMass;
            lblRadius.Text = Strings.LblRadius;

            txtStatusBar.Text = Strings.StatusReady;
        }

        private void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Properties.Settings.Default.Language = cultureCode;
            Properties.Settings.Default.Save();
        }

        private void RefreshWindow()
        {
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            Close();
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("en");
            RefreshWindow();
        }

        private void French_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("fr-FR");
            RefreshWindow();
        }

        private void German_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("de-DE");
            RefreshWindow();
        }

        private void ApplyDefaultTheme()
        {
            MainDock.Background = _defaultBackground;
            MainMenu.Background = Brushes.WhiteSmoke;
            MainStatusBar.Background = Brushes.WhiteSmoke;
            txtMainTitle.Foreground = _defaultTextForeground;

            ApplyLabelColour(Brushes.Black);
            ApplyButtonBackground(_defaultButtonBackground);
            ApplyTextBoxBackground(Brushes.White);

            FontFamily = _defaultFontFamily;
            FontSize = _defaultFontSize;
        }

        private void ApplyDarkTheme()
        {
            MainDock.Background = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            MainMenu.Background = new SolidColorBrush(Color.FromRgb(45, 45, 45));
            MainStatusBar.Background = new SolidColorBrush(Color.FromRgb(45, 45, 45));
            txtMainTitle.Foreground = Brushes.White;

            ApplyLabelColour(Brushes.White);
            ApplyButtonBackground(new SolidColorBrush(Color.FromRgb(70, 70, 70)));
            ApplyTextBoxBackground(new SolidColorBrush(Color.FromRgb(55, 55, 55)));
        }

        private void ApplyBlueTheme()
        {
            MainDock.Background = new SolidColorBrush(Color.FromRgb(230, 240, 255));
            MainMenu.Background = new SolidColorBrush(Color.FromRgb(200, 220, 245));
            MainStatusBar.Background = new SolidColorBrush(Color.FromRgb(200, 220, 245));
            txtMainTitle.Foreground = Brushes.DarkBlue;

            ApplyLabelColour(Brushes.DarkBlue);
            ApplyButtonBackground(new SolidColorBrush(Color.FromRgb(173, 216, 230)));
            ApplyTextBoxBackground(Brushes.White);
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyDefaultTheme();
            txtStatusBar.Text = "Light theme applied";
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyDarkTheme();
            txtStatusBar.Text = "Dark theme applied";
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyBlueTheme();
            txtStatusBar.Text = "Blue theme applied";
        }

        private void BackgroundColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.ColorDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color c = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                MainDock.Background = new SolidColorBrush(c);
                txtStatusBar.Text = "Background colour updated";
            }
        }

        private void LabelColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.ColorDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color c = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                ApplyLabelColour(new SolidColorBrush(c));
                txtStatusBar.Text = "Label colour updated";
            }
        }

        private void TextBoxColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.ColorDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color c = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                ApplyTextBoxBackground(new SolidColorBrush(c));
                txtStatusBar.Text = "TextBox colour updated";
            }
        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.FontDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                FontFamily = new FontFamily(dialog.Font.Name);
                FontSize = dialog.Font.Size;
                txtStatusBar.Text = "Font updated";
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Astronomical Processing System\nClient Application\nSupports language switching and UI customisation.",
                "Application Info",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ApplyLabelColour(Brush colour)
        {
            lblObserved.Foreground = colour;
            lblRest.Foreground = colour;
            lblVelocity.Foreground = colour;
            lblParallax.Foreground = colour;
            lblParsecs.Foreground = colour;
            lblLightYears.Foreground = colour;
            lblKm.Foreground = colour;
            lblCelsius.Foreground = colour;
            lblKelvin.Foreground = colour;
            lblMass.Foreground = colour;
            lblRadius.Foreground = colour;

            grpVelocity.Foreground = colour;
            grpDistance.Foreground = colour;
            grpTemperature.Foreground = colour;
            grpRadius.Foreground = colour;

            txtStatusBar.Foreground = colour;
        }

        private void ApplyButtonBackground(Brush colour)
        {
            btnVelocity.Background = colour;
            btnDistance.Background = colour;
            btnTemperature.Background = colour;
            btnRadius.Background = colour;
        }

        private void ApplyTextBoxBackground(Brush colour)
        {
            txtObserved.Background = colour;
            txtRest.Background = colour;
            txtVelocity.Background = colour;
            txtParallax.Background = colour;
            txtParsecs.Background = colour;
            txtLightYears.Background = colour;
            txtKm.Background = colour;
            txtCelsius.Background = colour;
            txtKelvin.Background = colour;
            txtMass.Background = colour;
            txtRadius.Background = colour;
        }

        private async void BtnVelocity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtObserved.Text, out double observed) ||
                    !double.TryParse(txtRest.Text, out double rest))
                {
                    MessageBox.Show(Strings.MsgInvalidInput);
                    return;
                }

                if (rest == 0)
                {
                    MessageBox.Show(Strings.MsgOutOfRange);
                    return;
                }

                var client = CreateGrpcClient();

                var request = new VelocityRequest
                {
                    ObservedWavelength = observed,
                    RestWavelength = rest
                };

                var response = await client.CalculateVelocityAsync(request);

                txtVelocity.Text = response.Velocity.ToString("E6");
                txtStatusBar.Text = "Velocity calculated via server";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error: " + ex.Message);
            }
        }

        private async void BtnDistance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtParallax.Text, out double parallax))
                {
                    MessageBox.Show(Strings.MsgInvalidInput);
                    return;
                }

                if (parallax <= 0)
                {
                    MessageBox.Show(Strings.MsgOutOfRange);
                    return;
                }

                var client = CreateGrpcClient();

                var request = new DistanceRequest
                {
                    Parallax = parallax
                };

                var response = await client.CalculateDistanceAsync(request);

                txtParsecs.Text = response.Parsecs.ToString("E6");
                txtLightYears.Text = response.LightYears.ToString("E6");
                txtKm.Text = response.Km.ToString("E6");
                txtStatusBar.Text = "Distance calculated via server";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error: " + ex.Message);
            }
        }

        private async void BtnTemp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtCelsius.Text, out double celsius))
                {
                    MessageBox.Show(Strings.MsgInvalidInput);
                    return;
                }

                if (celsius < -273.15)
                {
                    MessageBox.Show(Strings.MsgOutOfRange);
                    return;
                }

                var client = CreateGrpcClient();

                var request = new TemperatureRequest
                {
                    Celsius = celsius
                };

                var response = await client.ConvertTemperatureAsync(request);

                txtKelvin.Text = response.Kelvin.ToString("E6");
                txtStatusBar.Text = "Temperature converted via server";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error: " + ex.Message);
            }
        }

        private async void BtnRadius_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtMass.Text, out double mass))
                {
                    MessageBox.Show(Strings.MsgInvalidInput);
                    return;
                }

                if (mass <= 0)
                {
                    MessageBox.Show(Strings.MsgOutOfRange);
                    return;
                }

                var client = CreateGrpcClient();

                var request = new RadiusRequest
                {
                    Mass = mass
                };

                var response = await client.CalculateRadiusAsync(request);

                txtRadius.Text = response.Radius.ToString("E6");
                txtStatusBar.Text = "Radius calculated via server";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error: " + ex.Message);
            }
        }
    }
}