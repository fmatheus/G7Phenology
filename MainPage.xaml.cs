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

        public MainPage()
        {
            InitializeComponent();
            specNotification.Background.Opacity = 0.8;
            progress.Minimum = PhenologyDataContext.Singleton().mockId + 1;
            progress.Maximum = PhenologyDataContext.Singleton().Specimens.Length;
            next();
        }

        private void OnCheck(object sender, RoutedEventArgs e)
        {
            if (!save())
            {
                missing();
                return;
            }
                ToastPrompt prompt = new PhenoPrompt
                {
                    Title = PhenologyDataContext.Singleton().selected().Id.ToString(),
                    Phenology = PhenologyDataContext.Singleton().selected().getPhenology()
                };
                prompt.Tap += delegate { PhenologyDataContext.Singleton().prev(); load(); };
                prompt.Show();
                next();
        }
        private bool save()
        {
            int[] intensities = new int[6];
            for (int phase = 0; phase < 6; phase++)
            {
                PhenoTile pheno = ContentPanel.Children.ElementAt(phase) as PhenoTile;
                intensities[phase] = (pheno.Intensity);
            }
            PhenologyDataContext.Singleton().selected().updatePhenology(intensities);
            return intensities.Sum() > 0;
        }

        private void next()
        {
            if (PhenologyDataContext.Singleton().next() != null)
                load();
        }
        private void load()
        {
            Specimen spec = PhenologyDataContext.Singleton().selected();
            progress.Value = spec.Id;
            sector.Text = spec.Sector;
            specimen.Text = spec.Id.ToString();
            name.Text = spec.Name;
            notes.Text = spec.Comment;
            notes.Visibility = String.IsNullOrEmpty(spec.Comment) ? Visibility.Collapsed : Visibility.Visible;
            image.Source = new BitmapImage(new Uri(spec.Pictures[0], UriKind.RelativeOrAbsolute));
            int[] phenology = spec.getPhenology();
            for (int phase = 0; phase < 6; phase++)
            {
                PhenoTile pheno = ContentPanel.Children.ElementAt(phase) as PhenoTile;
                pheno.Intensity = phenology[phase];
            }
        }

        private void missing()
        {
            MessagePrompt message = new MessagePrompt
            {
                Title = "Nenhuma fenofase registrada",
                Body = new TextBlock { Text = "Registrar " + PhenologyDataContext.Singleton().selected().Id + " como não encontrado?" },
                IsCancelVisible = true,
                VerticalContentAlignment = VerticalAlignment.Bottom
            };
            message.Completed += delegate(Object sender, PopUpEventArgs<string, PopUpResult> e)
            {
                if (e.PopUpResult == PopUpResult.Ok)
                {
                    new ToastPrompt
                    {
                        Title = PhenologyDataContext.Singleton().selected().Id.ToString(),
                        Message = "Não encontrado.",
                        MillisecondsUntilHidden = 1500,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Background = new SolidColorBrush
                        {
                            Color = Colors.Black,
                            Opacity = 0.9
                        },
                        ImageSource = new BitmapImage(new Uri("Toolkit.Content/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute))
                    }.Show();
                    next();
                }
            };
            message.Show();
        }

        private void OnChat(object sender, RoutedEventArgs e)
        {
            save();
            NavigationService.Navigate(new Uri("/Chat.xaml", UriKind.Relative));
        }
    }
}