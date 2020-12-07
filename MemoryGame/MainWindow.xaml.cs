﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupGame();
        }

        void SetupGame()
        {
            List<string> animalEmoji = new List<string>
            {
                "🐶","🐶",
                "🐱","🐱",
                "🐰","🐰",
                "🐔","🐔",
                "🦄","🦄",
                "🐼","🐼",
                "🐩","🐩",
                "🦍","🦍"
            };

            var rnd = new Random();
            foreach (var t in mainGrid.Children.OfType<TextBlock>())
            {
                var i = rnd.Next(animalEmoji.Count);
                t.Text = animalEmoji[i];
                animalEmoji.RemoveAt(i);
            }
        }

        bool firstSelection;
        TextBlock previousSelection;
        void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var currentSelection = (TextBlock)sender;
            if (!firstSelection)
            {
                currentSelection.Visibility = Visibility.Hidden;
                previousSelection = currentSelection;
                firstSelection = true;
            }
            else if (currentSelection.Text == previousSelection.Text)
            {
                currentSelection.Visibility = Visibility.Hidden;
                firstSelection = false;
            }
            else
            {
                previousSelection.Visibility = Visibility.Visible;
                firstSelection = false;
            }
        }
    }
}
