using AstroProto;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;



namespace ClientApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Language
        private void English_Click(object sender, RoutedEventArgs e)
        {
        }

        private void French_Click(object sender, RoutedEventArgs e)
        {
        }

        private void German_Click(object sender, RoutedEventArgs e)
        {
        }

        // Theme
        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
        }

        // Customise
        private void BackgroundColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Background = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(
                        dialog.Color.R,
                        dialog.Color.G,
                        dialog.Color.B
                    )
                );
            }
        }

        private void LabelColour_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var brush = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(
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

        private void ApplyToVisualTree(MainWindow mainWindow, Action<object> value)
        {
            if (mainWindow == null || value == null) return;

            var parent = (System.Windows.DependencyObject)mainWindow;

            // Recursively traverse the visual tree and invoke the provided action for each node
            void Traverse(System.Windows.DependencyObject node)
            {
                if (node == null) return;

                int count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(node);
                for (int i = 0; i < count; i++)
                {
                    var child = System.Windows.Media.VisualTreeHelper.GetChild(node, i);
                    // Invoke the action on the child element
                    try
                    {
                        value(child);
                    }
                    catch
                    {
                        // Swallow exceptions from user provided action to avoid breaking traversal
                    }

                    // Recurse into child's children
                    Traverse(child);
                }
            }

            Traverse(parent);
        }

        private void TextBoxColour_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new System.Windows.Forms.ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                var brush = new System.Windows.Media.SolidColorBrush(

                  System.Windows.Media.Color.FromRgb(

                   dialog.Color.R,
                   dialog.Color.G,
                   dialog.Color.B
                   )
                );

                ApplyToVisualTree(this, control =>
                {
                    if (control is TextBox textbox)
                    {
                        textbox.Background = brush;
                    }

                }

                  );
            
            }

        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new System.Windows.Forms.FontDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                var fontFamily = new System.Windows.Media.FontFamily(dialog.Font.Name);
                double fontSize = dialog.Font.SizeInPoints;

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
                });
            }



        }

        // About
        private void About_Click(object sender, RoutedEventArgs e)
        {
        }

        // Calculation buttons
        private void BtnVelocity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDistance_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnTemp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnRadius_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}