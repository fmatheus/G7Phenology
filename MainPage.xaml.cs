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
        int mockId = 119;
        int mockSectorId = 23;
        static String[] mockSpecies = { "Phalaenopsis hieroglyphica", "Ophrys tenthredinifera", "Angraecum sesquipedale", "Paphiopedilum concolor" };

        public MainPage()
        {
            InitializeComponent();
            progress.Maximum = 10;
            next();
        }

        private void OnCheck(object sender, RoutedEventArgs e)
        {
            List<int> states = new List<int>(6);
            for (int i = 0; i < 6; i++)
            {
                PhenoTile pheno = (PhenoTile)ContentPanel.Children.ElementAt(i);
                states.Add(pheno.reset());
            }
            new ToastPrompt
            {
                Height = 173,
                Margin = new Thickness {
                    Top = 48
                },
                Title = mockId.ToString(),
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush
                {
                    Color = Colors.Black,
                    Opacity = 0.85
                },
                Message = mockSpecies[mockId % 4] + ": " + String.Join(",", states),
                ImageSource = new BitmapImage(new Uri("Toolkit.Content/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute)),
            }.Show();
            next();
        }
        private void next()
        {
            progress.Value = ++mockId % progress.Maximum;
            if (progress.Value == 0)
                sector.Text = "BS" + mockSectorId++;
            name.Content = mockId + " " + mockSpecies[mockId % 4];
            image.Source = new BitmapImage(new Uri("Images/" + (mockId % 4 + 1) + ".jpg", UriKind.RelativeOrAbsolute));
        }


    }
}