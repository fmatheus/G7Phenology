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

        protected ChatBubble myBubble;
        protected ChatBubbleTextBox myBubbleText;

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
                myBubble = new ChatBubble
                {
                    Margin = new Thickness(12, 12, 12, 12),
                    Background = App.Current.Resources["PhoneDisabledBrush"] as Brush,
                    ChatBubbleDirection = ChatBubbleDirection.LowerRight,
                    Content = PhenoPrompt.phenoPanel(phenology)
                };
            myBubbleText = new ChatBubbleTextBox
            {
                AcceptsReturn = true,
                Margin = new Thickness(12, 12, 12, 12),
                VerticalAlignment = VerticalAlignment.Bottom,
                ChatBubbleDirection = ChatBubbleDirection.LowerRight,
            };
            if (myBubble != null)
                chat.Children.Add(myBubble);
            chat.Children.Add(myBubbleText);
            myBubbleText.Focus();
        }

        private void OkClick(object sender, EventArgs e)
        {
            (myBubble.Content as Panel).Children.Add(new TextBlock { Text = myBubbleText.Text });
            NavigationService.GoBack();
        }
        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}