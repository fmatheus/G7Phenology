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
            progress.Value = ++mockId % progress.Maximum;
            if (progress.Value == 0)
                sector.Text = "BS" + mockSectorId++;
            (sender as Button).IsEnabled = false;
            int[] intensities = new int[6];
            for (int phase = 0; phase < 6; phase++)
            {
                PhenoTile pheno = ContentPanel.Children.ElementAt(phase) as PhenoTile;
                intensities[phase] = (pheno.Intensity);
            }
            ToastPrompt prompt = new PhenoPrompt
            {
                Phenology = intensities.ToArray(),
            };
            prompt.Completed += delegate
            {
                next();
                (sender as Button).IsEnabled = true;
            };
            prompt.Show();
        }
        private void next()
        {
            for (int phase = 0; phase < 6; phase++)
            {
                PhenoTile pheno = ContentPanel.Children.ElementAt(phase) as PhenoTile;
                pheno.Intensity = 0;
            }
            name.Content = mockId + " " + mockSpecies[mockId % 4];
            image.Source = new BitmapImage(new Uri("Images/" + (mockId % 4 + 1) + ".jpg", UriKind.RelativeOrAbsolute));
        }


    }
}