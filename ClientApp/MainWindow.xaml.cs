// ============================================================
// Developer Name: Kate Odabas
// Student ID: [Add Your Student ID]
// Date: [Add Submission Date]
// Assessment: AT2 - Part B
// File: MainWindow.xaml.cs
// Description:
// WPF client application for the Astronomical Processing System.
// This file contains:
// 1. Language switching
// 2. Theme and UI customisation
// 3. About dialog
// 4. Astronomical calculation button logic
// ============================================================

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClientApp
{
    public partial class MainWindow : Window
    {
        // ============================================================
        // Constructor
        // ============================================================
        public MainWindow()
        {
            InitializeComponent();
            txtStatusBar.Text = "Ready";
        }

        // ============================================================
        // Language Switching (Assessment Requirement Q7)
        // Changes UI text at runtime for English, French and German.
        // ============================================================

        private void English_Click(object sender, RoutedEventArgs e)
        {
            Title = "Astronomical Processing System";
            txtMainTitle.Text = "Astronomical Processing System";

            menuLanguage.Header = "Language";
            menuEnglish.Header = "English";
            menuFrench.Header = "French";
            menuGerman.Header = "German";

            menuTheme.Header = "Theme";
            menuLightTheme.Header = "Light Theme";
            menuDarkTheme.Header = "Dark Theme";
            menuBlueTheme.Header = "Blue Theme";

            menuCustomise.Header = "Customise";
            menuBackgroundColour.Header = "Change Background Colour";
            menuLabelColour.Header = "Change Label Colour";
            menuTextBoxColour.Header = "Change TextBox Colour";
            menuFont.Header = "Change Font";

            menuAbout.Header = "About";
            menuAboutApp.Header = "Application Info";

            grpVelocity.Header = "Star Velocity (Doppler Effect)";
            lblObserved.Text = "Observed Wavelength (nm):";
            lblRest.Text = "Rest Wavelength (nm):";
            lblVelocity.Text = "Radial Velocity (m/s):";
            btnVelocity.Content = "Calculate Velocity";

            grpDistance.Header = "Star Distance (Parallax)";
            lblParallax.Text = "Parallax Angle (arcsec):";
            lblParsecs.Text = "Distance (parsecs):";
            lblLightYears.Text = "Distance (light-years):";
            lblKm.Text = "Distance (km):";
            btnDistance.Content = "Calculate Distance";

            grpTemperature.Header = "Temperature Conversion (°C to K)";
            lblCelsius.Text = "Temperature (°C):";
            lblKelvin.Text = "Temperature (K):";
            btnTemperature.Content = "Convert to Kelvin";

            grpRadius.Header = "Black Hole Event Horizon (Schwarzschild Radius)";
            lblMass.Text = "Black Hole Mass (kg):";
            lblRadius.Text = "Schwarzschild Radius (m):";
            btnRadius.Content = "Calculate Radius";

            txtStatusBar.Text = "Language changed to English.";
        }

        private void French_Click(object sender, RoutedEventArgs e)
        {
            Title = "Système de traitement astronomique";
            txtMainTitle.Text = "Système de traitement astronomique";

            menuLanguage.Header = "Langue";
            menuEnglish.Header = "Anglais";
            menuFrench.Header = "Français";
            menuGerman.Header = "Allemand";

            menuTheme.Header = "Thème";
            menuLightTheme.Header = "Thème Clair";
            menuDarkTheme.Header = "Thème Sombre";
            menuBlueTheme.Header = "Thème Bleu";

            menuCustomise.Header = "Personnaliser";
            menuBackgroundColour.Header = "Changer la couleur du fond";
            menuLabelColour.Header = "Changer la couleur du texte";
            menuTextBoxColour.Header = "Changer la couleur des zones de texte";
            menuFont.Header = "Changer la police";

            menuAbout.Header = "À propos";
            menuAboutApp.Header = "Information sur l'application";

            grpVelocity.Header = "Vitesse des étoiles (effet Doppler)";
            lblObserved.Text = "Longueur d'onde observée (nm) :";
            lblRest.Text = "Longueur d'onde au repos (nm) :";
            lblVelocity.Text = "Vitesse radiale (m/s) :";
            btnVelocity.Content = "Calculer la vitesse";

            grpDistance.Header = "Distance des étoiles (parallaxe)";
            lblParallax.Text = "Angle de parallaxe (arcsec) :";
            lblParsecs.Text = "Distance (parsecs) :";
            lblLightYears.Text = "Distance (années-lumière) :";
            lblKm.Text = "Distance (km) :";
            btnDistance.Content = "Calculer la distance";

            grpTemperature.Header = "Conversion de température (°C en K)";
            lblCelsius.Text = "Température (°C) :";
            lblKelvin.Text = "Température (K) :";
            btnTemperature.Content = "Convertir en Kelvin";

            grpRadius.Header = "Horizon des événements d'un trou noir";
            lblMass.Text = "Masse du trou noir (kg) :";
            lblRadius.Text = "Rayon de Schwarzschild (m) :";
            btnRadius.Content = "Calculer le rayon";

            txtStatusBar.Text = "Langue changée en français.";
        }

        private void German_Click(object sender, RoutedEventArgs e)
        {
            Title = "Astronomisches Verarbeitungssystem";
            txtMainTitle.Text = "Astronomisches Verarbeitungssystem";

            menuLanguage.Header = "Sprache";
            menuEnglish.Header = "Englisch";
            menuFrench.Header = "Französisch";
            menuGerman.Header = "Deutsch";

            menuTheme.Header = "Design";
            menuLightTheme.Header = "Helles Design";
            menuDarkTheme.Header = "Dunkles Design";
            menuBlueTheme.Header = "Blaues Design";

            menuCustomise.Header = "Anpassen";
            menuBackgroundColour.Header = "Hintergrundfarbe ändern";
            menuLabelColour.Header = "Textfarbe ändern";
            menuTextBoxColour.Header = "Textfeldfarbe ändern";
            menuFont.Header = "Schriftart ändern";

            menuAbout.Header = "Info";
            menuAboutApp.Header = "Anwendungsinfo";

            grpVelocity.Header = "Sternengeschwindigkeit (Doppler-Effekt)";
            lblObserved.Text = "Beobachtete Wellenlänge (nm):";
            lblRest.Text = "Ruhewellenlänge (nm):";
            lblVelocity.Text = "Radialgeschwindigkeit (m/s):";
            btnVelocity.Content = "Geschwindigkeit berechnen";

            grpDistance.Header = "Sternentfernung (Parallaxe)";
            lblParallax.Text = "Parallaxenwinkel (Bogensekunden):";
            lblParsecs.Text = "Entfernung (Parsec):";
            lblLightYears.Text = "Entfernung (Lichtjahre):";
            lblKm.Text = "Entfernung (km):";
            btnDistance.Content = "Entfernung berechnen";

            grpTemperature.Header = "Temperaturumrechnung (°C zu K)";
            lblCelsius.Text = "Temperatur (°C):";
            lblKelvin.Text = "Temperatur (K):";
            btnTemperature.Content = "In Kelvin umrechnen";

            grpRadius.Header = "Ereignishorizont eines Schwarzen Lochs";
            lblMass.Text = "Masse des Schwarzen Lochs (kg):";
            lblRadius.Text = "Schwarzschild-Radius (m):";
            btnRadius.Content = "Radius berechnen";

            txtStatusBar.Text = "Sprache auf Deutsch geändert.";
        }

        // ============================================================
        // Theme Menu Event Handlers
        // ============================================================

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            Background = Brushes.White;
            txtStatusBar.Text = "Light theme applied.";
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
            txtStatusBar.Text = "Dark theme applied.";
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromRgb(225, 235, 250));
            txtStatusBar.Text = "Blue theme applied.";
        }

        // ============================================================
        // UI Customisation Features
        // ============================================================

        private void BackgroundColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Background = new SolidColorBrush(
                    Color.FromRgb(dialog.Color.R, dialog.Color.G, dialog.Color.B)
                );

                txtStatusBar.Text = "Background colour changed.";
            }
        }

        private void LabelColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var brush = new SolidColorBrush(
                    Color.FromRgb(dialog.Color.R, dialog.Color.G, dialog.Color.B)
                );

                ApplyToVisualTree(this, control =>
                {
                    if (control is TextBlock textBlock)
                    {
                        textBlock.Foreground = brush;
                    }
                    else if (control is Label label)
                    {
                        label.Foreground = brush;
                    }
                    else if (control is GroupBox groupBox)
                    {
                        groupBox.Foreground = brush;
                    }
                });

                txtStatusBar.Text = "Label colour changed.";
            }
        }

        private void TextBoxColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var brush = new SolidColorBrush(
                    Color.FromRgb(dialog.Color.R, dialog.Color.G, dialog.Color.B)
                );

                ApplyToVisualTree(this, control =>
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Background = brush;
                    }
                });

                txtStatusBar.Text = "TextBox colour changed.";
            }
        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FontDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fontFamily = new FontFamily(dialog.Font.Name);
                double fontSize = dialog.Font.Size;

                ApplyToVisualTree(this, control =>
                {
                    if (control is TextBlock textBlock)
                    {
                        textBlock.FontFamily = fontFamily;
                        textBlock.FontSize = fontSize;
                    }
                    else if (control is Label label)
                    {
                        label.FontFamily = fontFamily;
                        label.FontSize = fontSize;
                    }
                    else if (control is GroupBox groupBox)
                    {
                        groupBox.FontFamily = fontFamily;
                        groupBox.FontSize = fontSize;
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.FontFamily = fontFamily;
                        textBox.FontSize = fontSize;
                    }
                    else if (control is Button button)
                    {
                        button.FontFamily = fontFamily;
                        button.FontSize = fontSize;
                    }
                    else if (control is MenuItem menuItem)
                    {
                        menuItem.FontFamily = fontFamily;
                        menuItem.FontSize = fontSize;
                    }
                });

                txtStatusBar.Text = "Font changed.";
            }
        }

        // ============================================================
        // Helper Method
        // ============================================================

        private void ApplyToVisualTree(DependencyObject parent, Action<object> action)
        {
            if (parent == null || action == null)
                return;

            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                try
                {
                    action(child);
                }
                catch
                {
                }

                ApplyToVisualTree(child, action);
            }
        }

        // ============================================================
        // About Menu
        // ============================================================

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Astronomical Processing System\nClient Application for astronomical calculations.",
                "About",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        // ============================================================
        // Star Velocity Calculation
        // ============================================================

        private void BtnVelocity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtObserved.Text, out double observed))
                {
                    MessageBox.Show("Invalid Observed Wavelength");
                    return;
                }

                if (!double.TryParse(txtRest.Text, out double rest))
                {
                    MessageBox.Show("Invalid Rest Wavelength");
                    return;
                }

                if (rest == 0)
                {
                    MessageBox.Show("Rest Wavelength cannot be zero");
                    return;
                }

                double c = 299792458;
                double velocity = ((observed - rest) / rest) * c;

                txtVelocity.Text = velocity.ToString("0.000E+00");
                txtStatusBar.Text = "Velocity calculated successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ============================================================
        // Star Distance Calculation
        // ============================================================

        private void BtnDistance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtParallax.Text, out double parallax))
                {
                    MessageBox.Show("Invalid Parallax Angle");
                    return;
                }

                if (parallax <= 0)
                {
                    MessageBox.Show("Parallax must be greater than zero");
                    return;
                }

                double parsecs = 1 / parallax;
                double lightYears = parsecs * 3.26156;
                double km = parsecs * 3.0857e13;

                txtParsecs.Text = parsecs.ToString("0.000E+00");
                txtLightYears.Text = lightYears.ToString("0.000E+00");
                txtKm.Text = km.ToString("0.000E+00");
                txtStatusBar.Text = "Distance calculated successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ============================================================
        // Temperature Conversion
        // ============================================================

        private void BtnTemp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtCelsius.Text, out double celsius))
                {
                    MessageBox.Show("Invalid Temperature");
                    return;
                }

                if (celsius < -273.15)
                {
                    MessageBox.Show("Temperature cannot be below -273.15°C");
                    return;
                }

                double kelvin = celsius + 273.15;

                txtKelvin.Text = kelvin.ToString("0.000E+00");
                txtStatusBar.Text = "Temperature converted successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ============================================================
        // Black Hole Event Horizon Calculation
        // ============================================================

        private void BtnRadius_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(txtMass.Text, out double mass))
                {
                    MessageBox.Show("Invalid Mass");
                    return;
                }

                if (mass <= 0)
                {
                    MessageBox.Show("Mass must be greater than zero");
                    return;
                }

                double G = 6.674e-11;
                double c = 299792458;
                double radius = (2 * G * mass) / (c * c);

                txtRadius.Text = radius.ToString("0.000E+00");
                txtStatusBar.Text = "Radius calculated successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}