using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Forms = System.Windows.Forms;
using ClientApp.Resources;

namespace ClientApp
{
    /*
     * Astronomical Processing System - Client Application
     * 
     * This application provides a graphical interface for performing
     * astronomical calculations and interacting with the system.
     * 
     * Main features:
     * - Multilingual UI (English, French, German) using .resx files
     * - Language switching at runtime
     * - UI customisation (themes, colours, fonts)
     * - Scientific calculations:
     *     • Star velocity (Doppler effect)
     *     • Star distance (parallax)
     *     • Temperature conversion (Celsius → Kelvin)
     *     • Black hole event horizon (Schwarzschild radius)
     * - Input validation and error handling
     */

    // Main window class controls UI, language, themes, and calculations
    public partial class MainWindow : Window
    {
        // Default UI settings used for reset
        private Brush _defaultBackground = Brushes.White;
        private Brush _defaultTextForeground = Brushes.Black;
        private Brush _defaultButtonBackground = Brushes.LightGray;
        private FontFamily _defaultFontFamily = new FontFamily("Segoe UI");
        private double _defaultFontSize = 13;

        // Constructor runs when app starts
        // Loads UI, applies language, and sets default theme
        public MainWindow()
        {
            InitializeComponent();
            ApplyResources();      // Apply language
            ApplyDefaultTheme();   // Apply theme
        }

        // Updates all UI text using resource files
        // Ensures correct language is displayed everywhere
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

        // Changes application language and saves user choice
        private void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Save language for next time app opens
            Properties.Settings.Default.Language = cultureCode;
            Properties.Settings.Default.Save();
        }

        // Reloads window to apply language change
        private void RefreshWindow()
        {
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            Close();
        }

        // Switch language to English
        private void English_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("en");
            RefreshWindow();
        }

        // Switch language to French
        private void French_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("fr-FR");
            RefreshWindow();
        }

        // Switch language to German
        private void German_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("de-DE");
            RefreshWindow();
        }

        // Applies default light theme
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

        // Applies dark theme
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

        // Applies blue theme
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

        // Applies light theme
        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyDefaultTheme();
            txtStatusBar.Text = "Light theme applied";
        }

        // Applies dark theme
        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            MainDock.Background = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            MainMenu.Background = new SolidColorBrush(Color.FromRgb(45, 45, 45));
            MainStatusBar.Background = new SolidColorBrush(Color.FromRgb(45, 45, 45));
            txtMainTitle.Foreground = Brushes.White;

            ApplyLabelColour(Brushes.White);
            ApplyButtonBackground(new SolidColorBrush(Color.FromRgb(70, 70, 70)));
            ApplyTextBoxBackground(new SolidColorBrush(Color.FromRgb(55, 55, 55)));

            txtStatusBar.Text = "Dark theme applied";
        }

        // Applies blue theme
        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            MainDock.Background = new SolidColorBrush(Color.FromRgb(230, 240, 255));
            MainMenu.Background = new SolidColorBrush(Color.FromRgb(200, 220, 245));
            MainStatusBar.Background = new SolidColorBrush(Color.FromRgb(200, 220, 245));
            txtMainTitle.Foreground = Brushes.DarkBlue;

            ApplyLabelColour(Brushes.DarkBlue);
            ApplyButtonBackground(new SolidColorBrush(Color.FromRgb(173, 216, 230)));
            ApplyTextBoxBackground(Brushes.White);

            txtStatusBar.Text = "Blue theme applied";
        }

        // Opens colour picker and changes background colour
        private void BackgroundColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.ColorDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color c = Color.FromArgb(dialog.Color.A, 
                    dialog.Color.R, 
                    dialog.Color.G, 
                    dialog.Color.B);
                MainDock.Background = new SolidColorBrush(c);
                txtStatusBar.Text = "Background colour updated";
            }
        }

        // Opens colour picker and changes label colour
        private void LabelColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.ColorDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color c = Color.FromArgb(dialog.Color.A,
                    dialog.Color.R, 
                    dialog.Color.G, 
                    dialog.Color.B);
                ApplyLabelColour(new SolidColorBrush(c));
                txtStatusBar.Text = "Label colour updated";
            }
        }

        // Opens colour picker and changes textbox colour
        private void TextBoxColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.ColorDialog();

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                Color c = Color.FromArgb(dialog.Color.A,
                    dialog.Color.R, 
                    dialog.Color.G, 
                    dialog.Color.B);
                ApplyTextBoxBackground(new SolidColorBrush(c));
                txtStatusBar.Text = "TextBox colour updated";
            }
        }

        // Opens font dialog and changes font
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

        // Shows application info
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Astronomical Processing System\nClient Application\nSupports language switching and UI customisation.",
                "Application Info",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // Changes label and group text colours
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

        // Changes button colours
        private void ApplyButtonBackground(Brush colour)
        {
            btnVelocity.Background = colour;
            btnDistance.Background = colour;
            btnTemperature.Background = colour;
            btnRadius.Background = colour;
        }

        // Changes textbox colours
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

        // Calculate star velocity
        private void BtnVelocity_Click(object sender, RoutedEventArgs e)
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

                const double speedOfLight = 299792458.0;
                double velocity = ((observed - rest) / rest) * speedOfLight;

                txtVelocity.Text = velocity.ToString("E6");
                txtStatusBar.Text = "Velocity calculated";
            }
            catch
            {
                MessageBox.Show(Strings.MsgServerError);
            }
        }

        // Calculate star distance
        private void BtnDistance_Click(object sender, RoutedEventArgs e)
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

                double parsecs = 1.0 / parallax;
                double lightYears = parsecs * 3.26156;
                double kilometres = parsecs * 3.0857E13;

                txtParsecs.Text = parsecs.ToString("E6");
                txtLightYears.Text = lightYears.ToString("E6");
                txtKm.Text = kilometres.ToString("E6");
                txtStatusBar.Text = "Distance calculated";
            }
            catch
            {
                MessageBox.Show(Strings.MsgServerError);
            }
        }

        // Convert temperature
        private void BtnTemp_Click(object sender, RoutedEventArgs e)
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

                double kelvin = celsius + 273.15;

                txtKelvin.Text = kelvin.ToString("E6");
                txtStatusBar.Text = "Temperature converted";
            }
            catch
            {
                MessageBox.Show(Strings.MsgServerError);
            }
        }

        // Calculate black hole radius
        private void BtnRadius_Click(object sender, RoutedEventArgs e)
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

                const double G = 6.674E-11;
                const double C = 299792458.0;

                double radius = (2 * G * mass) / (C * C);

                txtRadius.Text = radius.ToString("E6");
                txtStatusBar.Text = "Radius calculated";
            }
            catch
            {
                MessageBox.Show(Strings.MsgServerError);
            }
        }
    }
}