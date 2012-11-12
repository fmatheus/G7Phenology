using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Coding4Fun.Phone.Controls;

namespace G7Phenology
{
    public partial class MainPage : PhoneApplicationPage
    {
        int count = 0;
        //bool toogle = true;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void RoundButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Hello, %s!", "world");
            ToastPrompt toast = new ToastPrompt();
            toast.Title = "ToastPrompt" + ++count;
            toast.Message = "Some message";
            toast.ImageSource = new BitmapImage(new Uri("ApplicationIcon.png", UriKind.RelativeOrAbsolute));
            toast.Show();
        }

        private void Brotamento(Object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Tile has been clicked!" + ((Tile) sender).Content);
            //((StackPanel) sender.Content).;
            //Canvas1.Background = new SolidColorBrush(Colors.Blue);
            //WriteableBitmap icon = new WriteableBitmap((BitmapSource) Brotamento1.Source);
            //if (toogle)
            //    ReplaceColor(icon, Colors.White, Colors.Green);
            //else
            //    ReplaceColor(icon, Colors.Green, Colors.White);
            //toogle = !toogle;
            //Brotamento1.Source = icon;
        }

        private void ReplaceColor(WriteableBitmap bitmap, Color originalColor, Color replacementColor) {
            int[] pixels = bitmap.Pixels;
            int from = originalColor.A << 24 | originalColor.R << 16 | originalColor.G << 8 | originalColor.B;
            int to = replacementColor.A << 24 | replacementColor.R << 16 | replacementColor.G << 8 | replacementColor.B;
            for (var x = 0; x < pixels.Length; x++) {
                if (pixels[x] >> 24 != 0) {
                    pixels[x] = (BitConverter.GetBytes(pixels[x])[3] << 24)| (to & 0xffffff);
                }
            }
        }

        private void Queda(object sender, RoutedEventArgs e)
        {

        }
    }
}