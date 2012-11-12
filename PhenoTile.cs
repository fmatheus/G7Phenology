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
        int state = 0;
        Rectangle icon1;
        Rectangle icon2;

        public PhenoTile()
        {
            Width = 173;
            Height = 173;
            TextWrapping = TextWrapping.Wrap;
            Title = "Brotamento";
            Background = new SolidColorBrush(Colors.DarkGray);
            StackPanel icons = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Content = icons;
            icon1 = createIcon("Brotamento");
            icon2 = createIcon("Brotamento");
            icons.Children.Add(icon1);
            icons.Children.Add(icon2);
            renderState();
        }

        Rectangle createIcon(String name)
        {
            return new Rectangle
            {
                Width = 62,
                Height = 62,
                Margin = new Thickness(6),
                OpacityMask = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("Icons/" + name + ".png", UriKind.RelativeOrAbsolute))
                }
            };
        }
        void renderState()
        {
            switch (state)
            {
                case 0:
                    icon1.Fill = new SolidColorBrush(Colors.White);
                    icon2.Fill = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    icon1.Fill = new SolidColorBrush(Colors.Green);
                    break;
                case 2:
                    icon2.Fill = new SolidColorBrush(Colors.Green);
                    break;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            state = (state + 1) % 3;
            renderState();
        }
    }
}
