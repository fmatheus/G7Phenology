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
        int count = -1;
        static String[] target = { "Phalaenopsis hieroglyphica", "Ophrys tenthredinifera", "Angraecum sesquipedale", "Paphiopedilum concolor" };

        public MainPage()
        {
            InitializeComponent();
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
            ToastPrompt toast = new ToastPrompt();
            toast.Title = (121 + count).ToString();
            toast.Message = target[count] + ": " + String.Join(",", states);
            toast.ImageSource = new BitmapImage(new Uri("ApplicationIcon.png", UriKind.RelativeOrAbsolute));
            toast.Show();
            next();
       }
        private void next()
        {
            count = ++count % 4;
            name.Content = 121 + count + " " + target[count];
            image.Source = new BitmapImage(new Uri("Images/" + (count+1) + ".jpg", UriKind.RelativeOrAbsolute));
        }


    }
}