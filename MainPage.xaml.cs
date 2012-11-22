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
            specNotification.Background.Opacity = 0.8;
            next();
        }

        private void OnCheck(object sender, RoutedEventArgs e)
        {
            if (save()) next();
        }
        private bool save()
        {
            int[] intensities = new int[6];
            for (int phase = 0; phase < 6; phase++)
            {
                PhenoTile pheno = ContentPanel.Children.ElementAt(phase) as PhenoTile;
                intensities[phase] = (pheno.Intensity);
            }
            if (intensities.Sum() == 0)
            {
                return missing();
            }
            ToastPrompt prompt = new PhenoPrompt
            {
                Title = mockId.ToString(),
                Phenology = intensities
            };
            prompt.Show();
            return true;
        }
        private void next()
        {
            progress.Value = ++mockId % progress.Maximum;
            if (progress.Value == 0)
                sector.Text = "BS" + mockSectorId++;
            for (int phase = 0; phase < 6; phase++)
            {
                PhenoTile pheno = ContentPanel.Children.ElementAt(phase) as PhenoTile;
                pheno.Intensity = 0;
            }
            specimen.Text = mockId.ToString();
            name.Text = mockSpecies[mockId % 4];
            image.Source = new BitmapImage(new Uri("Images/" + (mockId % 4 + 1) + ".jpg", UriKind.RelativeOrAbsolute));
        }
        private bool missing()
        {
            MessagePrompt message = new MessagePrompt
            {
                Title = "Nenhuma fenofase registrada",
                Body = new TextBlock { Text = "Registrar " + mockId + " como não encontrado?" },
                IsCancelVisible = true,
                VerticalContentAlignment = VerticalAlignment.Bottom
            };
            message.Completed += delegate(Object sender, PopUpEventArgs<string, PopUpResult> e)
            {
                if (e.PopUpResult == PopUpResult.Ok)
                    next();
            };
            message.Show();
            return false;
        }
    }
}