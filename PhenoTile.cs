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
            FontWeight = FontWeights.Bold;
            Margin = new Thickness(12, 12, 0, 0);
        }

        Rectangle createIcon(String name)
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
        void renderState()
        {
            switch (state)
            {
                case -1:
                    icon1.Fill = Foreground;
                    icon2.Fill = new SolidColorBrush(Colors.White);
                    break;
                case 0:
                    icon1.Fill = new SolidColorBrush(Colors.White);
                    icon2.Fill = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    icon1.Fill = Foreground;
                    break;
                case 2:
                    icon2.Fill = Foreground;
                    break;
            }
        }

        public int reset()
        {
            int state = this.state;
            this.state = 0;
            renderState();
            return state;
        }

        protected override void OnClick()
        {
            base.OnClick();
            state = (state + 1) % 3;
            renderState();
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
                renderState();
            }
        }
    }
}
