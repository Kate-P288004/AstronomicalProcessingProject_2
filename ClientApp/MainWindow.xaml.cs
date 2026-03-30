// ============================================================
// Developer Name: Kate Odabas
// Student ID: P288004
// ============================================================

using AstroProto;
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
        // Initializes the WPF client window and all UI components.
        // ============================================================
        public MainWindow()
        {
            InitializeComponent();
        }

        // ============================================================
        // Language Menu Event Handlers
        // Assessment Requirement Q7:
        // Provide menu/button options to change language and layout.
        // These methods are currently placeholders and can be extended
        // later to switch labels, buttons and menu text between
        // English, French and German.
        // ============================================================

        private void English_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("English language selected.");
        }

        private void French_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("French language selected.");
        }

        private void German_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("German language selected.");
        }

        // ============================================================
        // Theme Menu Event Handlers
        // Assessment Requirement Q7:
        // Provide menu/button options to change form style, colours
        // and visual appearance.
        // These methods are placeholders and can be extended later.
        // ============================================================

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Light theme selected.");
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dark theme selected.");
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Blue theme selected.");
        }

        // ============================================================
        // UI Customisation Features
        // Assessment Requirement Q7:
        // Allows the user to customise the client application by
        // changing the background colour, label colour, textbox colour
        // and font settings at runtime.
        // ============================================================

        // ============================================================
        // Background Colour
        // Opens a colour dialog and applies the selected colour to the
        // main WPF window background.
        // ============================================================
        private void BackgroundColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Background = new SolidColorBrush(
                    Color.FromRgb(
                        dialog.Color.R,
                        dialog.Color.G,
                        dialog.Color.B
                    )
                );
            }
        }

        // ============================================================
        // Label Colour
        // Opens a colour dialog and applies the selected colour to all
        // TextBlock, Label and GroupBox controls in the form.
        // ============================================================
        private void LabelColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var brush = new SolidColorBrush(
                    Color.FromRgb(
                        dialog.Color.R,
                        dialog.Color.G,
                        dialog.Color.B
                    )
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
            }
        }

        // ============================================================
        // TextBox Colour
        // Opens a colour dialog and applies the selected background
        // colour to all TextBox controls in the form.
        // ============================================================
        private void TextBoxColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var brush = new SolidColorBrush(
                    Color.FromRgb(
                        dialog.Color.R,
                        dialog.Color.G,
                        dialog.Color.B
                    )
                );

                ApplyToVisualTree(this, control =>
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Background = brush;
                    }
                });
            }
        }

        // ============================================================
        // Font Selection
        // Opens a font dialog and applies the selected font family and
        // font size to text-based UI controls.
        // ============================================================
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
            }
        }

        // ============================================================
        // Helper Method
        // Traverses all visual elements in the WPF window and applies
        // the specified action to each child control.
        // Used for customisation features such as colours and font.
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
                    // Prevent UI traversal from stopping due to one control error
                }

                ApplyToVisualTree(child, action);
            }
        }

        // ============================================================
        // About Menu
        // Displays application information for the user.
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
        // Star Velocity Calculation (Doppler Effect)
        // Assessment Requirement Q7:
        // Reads observed wavelength and rest wavelength from the UI,
        // validates input, calculates star velocity and displays the
        // result in scientific notation.
        //
        // Formula:
        // V = ((Observed Wavelength - Rest Wavelength) / Rest Wavelength) * C
        // where C = 299792458 m/s
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ============================================================
        // Star Distance Calculation (Parallax)
        // Assessment Requirement Q7:
        // Reads parallax angle from the UI, validates input,
        // calculates the distance in parsecs and additionally shows
        // light years and kilometres in scientific notation.
        //
        // Formula:
        // Distance (parsecs) = 1 / parallax
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ============================================================
        // Temperature Conversion (Celsius to Kelvin)
        // Assessment Requirement Q7:
        // Reads temperature in Celsius, validates against absolute zero,
        // converts it to Kelvin and displays the result in scientific
        // notation.
        //
        // Formula:
        // K = C + 273.15
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ============================================================
        // Black Hole Event Horizon Calculation
        // Assessment Requirement Q7:
        // Reads black hole mass, validates input, calculates the
        // Schwarzschild radius and displays the result in scientific
        // notation.
        //
        // Formula:
        // R = (2GM) / c^2
        // where:
        // G = 6.674 × 10^-11
        // c = 299792458 m/s
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}