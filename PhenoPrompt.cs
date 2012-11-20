using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Coding4Fun.Phone.Controls;

namespace G7Phenology
{
    public class PhenoPrompt : ToastPrompt
    {

        public PhenoPrompt()
        {
            Margin = new Thickness(0, 48, 0, 0);
            MillisecondsUntilHidden = 1500;
            VerticalContentAlignment = VerticalAlignment.Center;
            Background = new SolidColorBrush
            {
                Color = Colors.Black,
                Opacity = 0.6
            };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            StackPanel phenoPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 10, 0)
            };
            for (int phase = 0; phase < 6; phase++)
            {
                int intensity = Phenology[phase];
                while (intensity-- > 0)
                {
                    phenoPanel.Children.Add(PhenoTile.createIcon(phase));
                }
            }
            Grid panel = (((GetTemplateChild("ToastImage") as Image).Parent as Panel).Parent as Panel).Parent as Grid;
            panel.Children.Clear();
            panel.Height = 173;
            panel.Children.Add(new Image
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                Stretch = Stretch.None,
                Source = new BitmapImage(new Uri("Toolkit.Content/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute))
            });
            panel.Children.Add(phenoPanel);
        }

        public int[] Phenology
        {
            get { return (int[])GetValue(PhenologyProperty); }
            set { SetValue(PhenologyProperty, value); }
        }

        public static readonly DependencyProperty PhenologyProperty =
            DependencyProperty.Register("Phenology", typeof(int[]), typeof(PhenoPrompt), new PropertyMetadata(new int[] { 0, 0, 0, 0, 0, 0 }));
    }
}
