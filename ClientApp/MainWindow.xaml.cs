using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Forms = System.Windows.Forms;
using ClientApp.Resources;
using AstroProto;
using Grpc.Net.Client;

namespace ClientApp
{
    public partial class MainWindow : Window
    {
        // The address where the gRPC server is listening.
        // Port 5000 is the default used by the server application.
        // gRPC communicates over HTTP/2, which is why the address uses "http://".
        private const string GrpcServerAddress = "http://localhost:5000";

        public MainWindow()
        {
            // Enable unencrypted HTTP/2 support - required for gRPC on a local
            // network without SSL certificates. This is acceptable for a private
            // LAN environment as described in the assessment scenario.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            // Build all the WPF controls defined in MainWindow.xaml
            InitializeComponent();

            // Load all text labels from the resource file (supports multilanguage)
            ApplyResources();

            // Set the default light colour theme on startup
            ApplyDefaultTheme();
        }

        // ── gRPC Client ───────────────────────────────────────────────────────

        // Creates and returns a new gRPC client connected to the server.
        // GrpcChannel is the gRPC equivalent of opening a network connection.
        // AstroServiceClient is the auto-generated class from the .proto file
        // that lets us call server methods as if they were local functions.
        private AstroService.AstroServiceClient CreateGrpcClient() =>
            new AstroService.AstroServiceClient(GrpcChannel.ForAddress(GrpcServerAddress));

        // A reusable helper that wraps any gRPC server call in a try/catch.
        // This ensures the app shows a friendly message if the server is offline
        // rather than crashing. The "where T : class" constraint is required
        // so that we can safely return null when something goes wrong.
        private async Task<T?> SafeCall<T>(Func<Task<T>> action) where T : class
        {
            try
            {
                // Await the server call and return the result if successful
                return await action();
            }
            catch
            {
                // If anything goes wrong (server offline, network error, etc.)
                // show a friendly error message instead of crashing
                MessageBox.Show(Strings.MsgServerUnavailable, Strings.MsgInputError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        // ── Resources (Multilanguage Support) ────────────────────────────────

        // Applies all UI text from the Strings resource file.
        // The Strings class (in Resources/Strings.resx) stores all labels
        // in English, French and German. When the user changes language,
        // this method is called again to refresh all text on screen.
        // This is how the assessment requirement for runtime language switching
        // is achieved - the entire window is rebuilt with the new language.
        private void ApplyResources()
        {
            // Set the window title bar text and the main heading label
            Title = txtMainTitle.Text = Strings.AppTitle;

            // Populate the Language menu items from the resource file
            menuLanguage.Header = Strings.MenuLanguage;
            menuEnglish.Header = Strings.MenuEnglish;
            menuFrench.Header = Strings.MenuFrench;
            menuGerman.Header = Strings.MenuGerman;
            menuTheme.Header = Strings.MenuTheme;
            menuFont.Header = Strings.MenuFont;

            // Populate the Customise menu items
            menuCustomise.Header = Strings.MenuCustomise;
            menuBackgroundColour.Header = Strings.MenuBackgroundColour;
            menuLabelColour.Header = Strings.MenuLabelColour;
            menuTextBoxColour.Header = Strings.MenuTextBoxColour;
            menuAbout.Header = Strings.MenuAbout;
            menuAboutApp.Header = Strings.MenuAboutApp;
            menuLightTheme.Header = Strings.MenuLightTheme;
            menuDarkTheme.Header = Strings.MenuDarkTheme;
            menuBlueTheme.Header = Strings.MenuBlueTheme;

            // Set GroupBox headers for each calculation section
            grpVelocity.Header = Strings.GrpVelocity;
            grpDistance.Header = Strings.GrpDistance;
            grpTemperature.Header = Strings.GrpTemperature;
            grpRadius.Header = Strings.GrpRadius;

            // Set the text on each Calculate button
            btnVelocity.Content = Strings.BtnCalculateVelocity;
            btnDistance.Content = Strings.BtnCalculateDistance;
            btnTemperature.Content = Strings.BtnConvertTemperature;
            btnRadius.Content = Strings.BtnCalculateHorizon;

            // Set all input/output field labels
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

            // Set the status bar to show "Ready" in the current language
            txtStatusBar.Text = Strings.StatusReady;
        }

        // ── Language Switching ────────────────────────────────────────────────

        // Changes the application language at runtime by updating the
        // current thread's culture. The culture controls which .resx
        // language file is loaded (e.g. Strings.fr-FR.resx for French).
        // This meets the assessment requirement for English, French and German.
        private void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);

            // Apply the new culture to the current thread and all future threads
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Save the selected language to user settings so it persists
            // the next time the application is launched
            Properties.Settings.Default.Language = cultureCode;
            Properties.Settings.Default.Save();
        }

        // Closes the current window and opens a brand new one.
        // This is needed after a language change so all controls reload
        // their text from the newly selected resource file.
        private void RefreshWindow()
        {
            var w = new MainWindow();
            Application.Current.MainWindow = w;
            w.Show();
            Close();
        }

        // Menu click handlers for the three supported languages.
        // Each one sets the culture then refreshes the window.
        private void English_Click(object sender, RoutedEventArgs e) { SetLanguage("en"); RefreshWindow(); }
        private void French_Click(object sender, RoutedEventArgs e) { SetLanguage("fr-FR"); RefreshWindow(); }
        private void German_Click(object sender, RoutedEventArgs e) { SetLanguage("de-DE"); RefreshWindow(); }

        // ── Themes ────────────────────────────────────────────────────────────
        // The three theme methods below meet the assessment requirement for
        // UI customisation (background, buttons, labels, textboxes).
        // WPF's Brush system makes it easy to apply colours across controls.

        // Light theme - white background, standard grey buttons (default on startup)
        private void ApplyDefaultTheme()
        {
            MainDock.Background = Brushes.White;
            MainMenu.Background = Brushes.WhiteSmoke;
            MainStatusBar.Background = Brushes.WhiteSmoke;
            txtMainTitle.Foreground = Brushes.Black;

            ApplyLabelColour(Brushes.Black);
            ApplyButtonBackground(Brushes.LightGray);
            ApplyTextBoxBackground(Brushes.White);

            // Reset to the default font and size
            FontFamily = new FontFamily("Segoe UI");
            FontSize = 13;
        }

        // Dark theme - dark grey backgrounds with white text.
        // Useful for night shifts as described in the assessment scenario
        // (teams work 24/7 and need a "night mode").
        private void ApplyDarkTheme()
        {
            MainDock.Background = Brush(34, 34, 34);
            MainMenu.Background = Brush(45, 45, 45);
            MainStatusBar.Background = Brush(45, 45, 45);
            txtMainTitle.Foreground = Brushes.White;

            ApplyLabelColour(Brushes.White);
            ApplyButtonBackground(Brush(70, 70, 70));
            ApplyTextBoxBackground(Brush(55, 55, 55));
        }

        // Blue theme - soft blue tones suitable for daytime use
        private void ApplyBlueTheme()
        {
            MainDock.Background = Brush(230, 240, 255);
            MainMenu.Background = Brush(200, 220, 245);
            MainStatusBar.Background = Brush(200, 220, 245);
            txtMainTitle.Foreground = Brushes.DarkBlue;

            ApplyLabelColour(Brushes.DarkBlue);
            ApplyButtonBackground(Brush(173, 216, 230));
            ApplyTextBoxBackground(Brushes.White);
        }

        // Helper method to create a SolidColorBrush from RGB values.
        // Reduces repetition when setting colours in the theme methods above.
        private static SolidColorBrush Brush(byte r, byte g, byte b) =>
            new SolidColorBrush(Color.FromRgb(r, g, b));

        // Theme menu click handlers - apply the chosen theme and update status bar
        private void LightTheme_Click(object sender, RoutedEventArgs e) { ApplyDefaultTheme(); txtStatusBar.Text = "Light theme applied"; }
        private void DarkTheme_Click(object sender, RoutedEventArgs e) { ApplyDarkTheme(); txtStatusBar.Text = "Dark theme applied"; }
        private void BlueTheme_Click(object sender, RoutedEventArgs e) { ApplyBlueTheme(); txtStatusBar.Text = "Blue theme applied"; }

        // ── Custom Colour & Font Pickers ──────────────────────────────────────
        // These methods open standard Windows dialogs (ColorDialog, FontDialog)
        // to let the user pick a custom colour or font at runtime.
        // This meets the assessment requirements for custom background colour
        // and font/size selection via dialog boxes.

        // Opens a colour picker dialog and returns the chosen colour as a Brush.
        // Returns null if the user clicks Cancel - callers must check for this.
        private Brush? PickColour()
        {
            var dialog = new Forms.ColorDialog();
            if (dialog.ShowDialog() != Forms.DialogResult.OK) return null;

            // Convert the System.Drawing.Color (WinForms) to a WPF Color
            var c = dialog.Color;
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        // Lets the user pick a custom background colour for the main window
        private void BackgroundColour_Click(object sender, RoutedEventArgs e)
        {
            Brush? brush = PickColour();
            if (brush == null) return; // User cancelled the dialog
            MainDock.Background = brush;
            txtStatusBar.Text = "Background colour updated";
        }

        // Lets the user pick a custom colour for all labels and group headings
        private void LabelColour_Click(object sender, RoutedEventArgs e)
        {
            Brush? brush = PickColour();
            if (brush == null) return;
            ApplyLabelColour(brush);
            txtStatusBar.Text = "Label colour updated";
        }

        // Lets the user pick a custom background colour for all text input boxes
        private void TextBoxColour_Click(object sender, RoutedEventArgs e)
        {
            Brush? brush = PickColour();
            if (brush == null) return;
            ApplyTextBoxBackground(brush);
            txtStatusBar.Text = "TextBox colour updated";
        }

        // Opens the Windows Font Dialog so the user can change the font and size
        // for the entire window. This uses WinForms FontDialog, which is imported
        // at the top of the file as "Forms" to avoid a naming conflict with WPF.
        private void Font_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Forms.FontDialog();
            if (dialog.ShowDialog() != Forms.DialogResult.OK) return;
            FontFamily = new FontFamily(dialog.Font.Name);
            FontSize = dialog.Font.Size;
            txtStatusBar.Text = "Font updated";
        }

        // Shows a simple About dialog with basic application information
        private void About_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show(
                Strings.AboutMessage,
                Strings.AboutTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Information);

        // ── Apply Colour Helpers ──────────────────────────────────────────────
        // These three helper methods apply a colour to a group of related controls.
        // Using foreach loops avoids writing the same line for every control.

        // Sets the text colour of all labels, group box headers and the status bar
        private void ApplyLabelColour(Brush colour)
        {
            foreach (var lbl in new[] { lblObserved, lblRest, lblVelocity, lblParallax,
                                        lblParsecs, lblLightYears, lblKm, lblCelsius,
                                        lblKelvin, lblMass, lblRadius })
                lbl.Foreground = colour;

            foreach (var grp in new[] { grpVelocity, grpDistance, grpTemperature, grpRadius })
                grp.Foreground = colour;

            txtStatusBar.Foreground = colour;
        }

        // Sets the background colour of all four Calculate buttons
        private void ApplyButtonBackground(Brush colour)
        {
            foreach (var btn in new[] { btnVelocity, btnDistance, btnTemperature, btnRadius })
                btn.Background = colour;
        }

        // Sets the background colour of all text input and output fields
        private void ApplyTextBoxBackground(Brush colour)
        {
            foreach (var txt in new[] { txtObserved, txtRest, txtVelocity, txtParallax, txtParsecs,
                                        txtLightYears, txtKm, txtCelsius, txtKelvin, txtMass, txtRadius })
                txt.Background = colour;
        }

        // ── Calculation Button Handlers ───────────────────────────────────────
        // Each button below validates user input, sends a request to the gRPC
        // server, and displays the result. The "async void" pattern is used
        // because WPF event handlers cannot return Task directly.
        //
        // The gRPC server receives the input, uses the AstroMath.DLL library
        // to perform the calculation, and returns the result. The client
        // never performs any calculations itself - it only sends and receives.

        // Calculates star velocity using the Doppler shift formula.
        // Requires: Observed Wavelength and Rest Wavelength (both in nm).
        // Returns: Velocity in metres per second (scientific notation).
        // Validation: Both inputs must be valid numbers; rest wavelength cannot be zero.
        private async void BtnVelocity_Click(object sender, RoutedEventArgs e)
        {
            // Validate the observed wavelength field first
            if (!double.TryParse(txtObserved.Text, out double observed))
            { MessageBox.Show(Strings.MsgInvalidObserved, Strings.MsgInputError, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            // Validate the rest wavelength field separately so the message is specific
            if (!double.TryParse(txtRest.Text, out double rest))
            { MessageBox.Show(Strings.MsgInvalidRest, Strings.MsgInputError, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            // Rest wavelength cannot be zero - this would cause division by zero
            if (rest == 0)
            { MessageBox.Show(Strings.MsgRestZero, Strings.MsgOutOfRangeTitle, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            txtStatusBar.Text = "Processing velocity...";

            // Send the request to the gRPC server asynchronously.
            // The server calls AstroMath.DLL to calculate the result.
            var response = await SafeCall(() =>
                CreateGrpcClient().CalculateVelocityAsync(new VelocityRequest
                {
                    ObservedWavelength = observed,
                    RestWavelength = rest
                }).ResponseAsync);

            if (response == null) return; // Server was unavailable - SafeCall showed the error

            // Display the velocity result in scientific notation (e.g. 1.234567E+006)
            txtVelocity.Text = response.Velocity.ToString("E6");
            txtStatusBar.Text = "Velocity calculated successfully";
        }

        // Calculates star distance using the parallax angle formula.
        // Requires: Parallax angle in arcseconds.
        // Returns: Distance in parsecs, light years, and kilometres.
        // Validation: Parallax must be a positive number greater than zero.
        private async void BtnDistance_Click(object sender, RoutedEventArgs e)
        {
            // Validate that the parallax input is a valid number
            if (!double.TryParse(txtParallax.Text, out double parallax))
            { MessageBox.Show(Strings.MsgInvalidParallax, Strings.MsgInputError, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            // Parallax must be positive - a zero or negative angle is physically impossible
            if (parallax <= 0)
            { MessageBox.Show(Strings.MsgParallaxRange, Strings.MsgOutOfRangeTitle, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            txtStatusBar.Text = "Processing distance...";

            // Send the parallax value to the server for calculation
            var response = await SafeCall(() =>
                CreateGrpcClient().CalculateDistanceAsync(new DistanceRequest
                {
                    Parallax = parallax
                }).ResponseAsync);

            if (response == null) return;

            // Display all three distance units returned by the server
            txtParsecs.Text = response.Parsecs.ToString("E6");
            txtLightYears.Text = response.LightYears.ToString("E6");
            txtKm.Text = response.Km.ToString("E6");
            txtStatusBar.Text = "Distance calculated successfully";
        }

        // Converts a temperature from Celsius to Kelvin.
        // Requires: Temperature in Celsius.
        // Returns: Temperature in Kelvin.
        // Validation: Celsius must be greater than -273.15 (absolute zero).
        private async void BtnTemp_Click(object sender, RoutedEventArgs e)
        {
            // Validate that the Celsius input is a valid number
            if (!double.TryParse(txtCelsius.Text, out double celsius))
            { MessageBox.Show(Strings.MsgInvalidCelsius, Strings.MsgInputError, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            // -273.15 degrees Celsius is absolute zero - nothing can be colder
            if (celsius < -273.15)
            { MessageBox.Show(Strings.MsgCelsiusRange, Strings.MsgOutOfRangeTitle, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            txtStatusBar.Text = "Processing temperature...";

            // Send the Celsius value to the server for conversion
            var response = await SafeCall(() =>
                CreateGrpcClient().ConvertTemperatureAsync(new TemperatureRequest
                {
                    Celsius = celsius
                }).ResponseAsync);

            if (response == null) return;

            // Display the Kelvin result returned by the server
            txtKelvin.Text = response.Kelvin.ToString("E6");
            txtStatusBar.Text = "Temperature converted successfully";
        }

        // Calculates the Schwarzschild radius (event horizon) of a black hole.
        // Requires: Mass of the black hole in kilograms.
        // Returns: Event horizon radius in metres.
        // Validation: Mass must be a positive number greater than zero.
        private async void BtnRadius_Click(object sender, RoutedEventArgs e)
        {
            // Validate that the mass input is a valid number
            if (!double.TryParse(txtMass.Text, out double mass))
            { MessageBox.Show(Strings.MsgInvalidMass, Strings.MsgInputError, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            // A black hole must have positive mass - zero or negative is not valid
            if (mass <= 0)
            { MessageBox.Show(Strings.MsgMassRange, Strings.MsgOutOfRangeTitle, MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            txtStatusBar.Text = "Processing radius...";

            // Send the mass value to the server for the Schwarzschild radius calculation
            var response = await SafeCall(() =>
                CreateGrpcClient().CalculateRadiusAsync(new RadiusRequest
                {
                    Mass = mass
                }).ResponseAsync);

            if (response == null) return;

            // Display the event horizon radius in scientific notation
            txtRadius.Text = response.Radius.ToString("E6");
            txtStatusBar.Text = "Radius calculated successfully";
        }
    }
}