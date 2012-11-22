using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Coding4Fun.Phone.Controls;

namespace G7Phenology
{
    public class PhenoTile : Tile
    {
        protected static readonly Brush whiteBrush = new SolidColorBrush(Colors.White);
        protected static readonly String[] phenophases = { "Brotamento", "Queda", "Botão", "Antese", "Fruto Verde", "Fruto Maduro" };
        protected static Brush[] phenoForeground = new Brush[6];
        public static Brush[] phenoBackground = new Brush[6];
        private static int phase = 0;

        Rectangle icon1;
        Rectangle icon2;

        public PhenoTile()
        {
            Width = 173;
            Height = 173;
            FontWeight = FontWeights.Bold;
            Margin = new Thickness(12, 12, 0, 0);
        }

        public int Intensity
        {
            get { return (int)GetValue(IntensityProperty); }
            set { SetValue(IntensityProperty, value); }
        }
        public static readonly DependencyProperty IntensityProperty =
            DependencyProperty.Register("Intensity", typeof(int), typeof(PhenoTile), new PropertyMetadata(0, OnIntensityChange));

        private static void OnIntensityChange(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as PhenoTile).renderIntensity();
        }
        
        static Rectangle createIcon(String name)
        {
            return new Rectangle
            {
                Width = 62,
                Height = 62,
                Margin = new Thickness(6, 0, 6, 0),
                OpacityMask = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("Icons/" + name + ".png", UriKind.RelativeOrAbsolute))
                }
            };
        }
        public static Rectangle createIcon(int phenophase)
        {
            Rectangle icon = createIcon(phenophases[phenophase]);
            icon.Fill = phenoForeground[phenophase];
            return icon;
        }
        private void renderIntensity()
        {
            switch (Intensity)
            {
                case -1:
                    icon1.Fill = Foreground;
                    icon2.Fill = whiteBrush;
                    break;
                case 0:
                    icon1.Fill = whiteBrush;
                    icon2.Fill = whiteBrush;
                    break;
                case 1:
                    icon1.Fill = Foreground;
                    icon2.Fill = whiteBrush;
                    break;
                case 2:
                    icon1.Fill = Foreground;
                    icon2.Fill = Foreground;
                    break;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            Intensity = (Intensity + 1) % 3;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Content == null)
            {
                Background.Opacity = 0.6;
                StackPanel icons = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Content = icons;
                icon1 = createIcon(Title);
                icon2 = createIcon(Title);
                icons.Children.Add(icon1);
                icons.Children.Add(icon2);
                renderIntensity();
            }
            phenoBackground[phase] = Background;
            phenoForeground[phase++] = Foreground;
        }
    }
}
