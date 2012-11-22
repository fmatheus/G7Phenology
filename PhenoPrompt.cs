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
            MillisecondsUntilHidden = 1500;
            VerticalAlignment = VerticalAlignment.Bottom;
            VerticalContentAlignment = VerticalAlignment.Center;
            Background = new SolidColorBrush
            {
                Color = Colors.Black,
                Opacity = 0.9
            };
        }

        public static StackPanel phenoPanel(int[] phenology)
        {
            StackPanel phenoPanel = new StackPanel
            {
                Height = 80,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            for (int phase = 0; phase < 6; phase++)
            {
                int intensity = phenology[phase];
                while (intensity-- > 0)
                {
                    phenoPanel.Children.Add(PhenoTile.createIcon(phase));
                }
            }
            if (phenoPanel.Children.Count == 0)
            {
                phenoPanel.Children.Add(new TextBlock
                {
                    Text = "Não encontrado.",
                    FontWeight = FontWeights.Bold
                });
            }
            return phenoPanel;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Grid panel = (((GetTemplateChild("ToastImage") as Image).Parent as Panel).Parent as Panel).Parent as Grid;
            panel.Children.Clear();
            panel.Children.Add(new TextBlock
            {
                Text = Title,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 10, 10)
            });
            panel.Children.Add(new Image
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                Stretch = Stretch.None,
                Source = new BitmapImage(new Uri("Toolkit.Content/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute))
            });
            panel.Children.Add(phenoPanel(Phenology));
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
