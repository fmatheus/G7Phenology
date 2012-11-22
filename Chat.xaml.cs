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
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Coding4Fun.Phone.Controls;

namespace G7Phenology
{
    public partial class Chat : PhoneApplicationPage
    {
        public Chat()
        {
            InitializeComponent();
            Specimen spec = PhenologyDataContext.Singleton().selected();
            PageTitle.Text = spec.Id + " " + spec.Name;
            for (int date = 0; date < PhenologyDataContext.Singleton().Date; date++)
            {
                chat.Children.Add(new ChatBubble
                {
                    Margin = new Thickness(12, 12, 12, 12),
                    Background = App.Current.Resources["PhoneDisabledBrush"] as Brush,
                    ChatBubbleDirection = ChatBubbleDirection.UpperLeft,
                    Content = PhenoPrompt.phenoPanel(spec.Phenologies[date])
                });
            }
            int[] phenology = spec.getPhenology();
            if (phenology.Sum() > 0)
                chat.Children.Add(new ChatBubble
                {
                    Margin = new Thickness(12,12,12,12),
                    Background = App.Current.Resources["PhoneDisabledBrush"] as Brush,
                    ChatBubbleDirection = ChatBubbleDirection.LowerRight,
                    Content = PhenoPrompt.phenoPanel(phenology)
                });
        }
    }
}