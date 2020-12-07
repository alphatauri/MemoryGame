using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += Timer_Tick;

            SetupGame();
        }

        void SetupGame()
        {
            List<string> animalEmoji = new List<string>();
            List<string> animalEmojiTotal = new List<string>
            {
                "😺","😸","😹","😻","😼","😽","🙀","😿","😾","🙈","🙉","🙊","🐵","🐶","🐺","🐱","🦁","🐯","🦒","🦊","🦝","🐮",
                "🐷","🐗","🐭","🐹","🐰","🐻","🐨","🐼","🐸","🦓","🐴","🦄","🐔","🐲","🐒","🦍","🦧","🦮","🐕‍","🐩","🐕","🐈",
                "🐅","🐆","🐎","🦌","🦏","🦛","🐂","🐃","🐄","🐖","🐏","🐑","🐐","🐪","🐫","🦙","🦘","🦥","🦨","🦡","🐘","🐁",
                "🐀","🦔","🐇","🐿","🦎","🐊","🐢","🐍","🐉","🦕","🦖","🦦","🦈","🐬","🐳","🐋","🐟","🐠","🐡","🦐","🦑","🐙",
                "🦞","🦀","🐚","🦆","🐓","🦃","🦅","🕊","🦢","🦜","🦩","🦚","🦉","🐦","🐧","🐥","🐤","🐣","🦇","🦋","🐌","🐛",
                "🦟","🦗","🐜","🐝","🐞","🦂","🕷"
            };

            var rnd = new Random();
            for (var i = 0; i < 8; i++)
            {
                var j = rnd.Next(animalEmojiTotal.Count);
                var emoji = animalEmojiTotal[j];
                animalEmoji.Add(emoji);
                animalEmoji.Add(emoji);
                animalEmojiTotal.RemoveAt(j);
            }

            foreach (var t in mainGrid.Children.OfType<TextBlock>())
            {
                if (t.Name != "timeDisplay")
                {
                    t.Visibility = Visibility.Visible;
                    var i = rnd.Next(animalEmoji.Count);
                    t.Text = animalEmoji[i];
                    animalEmoji.RemoveAt(i);
                }
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        bool firstSelection;
        TextBlock previousSelection;
        DispatcherTimer timer;

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
                matchesFound++;
                currentSelection.Visibility = Visibility.Hidden;
                firstSelection = false;
            }
            else
            {
                previousSelection.Visibility = Visibility.Visible;
                firstSelection = false;
            }

            if (matchesFound == 8)
                timer.Stop();
        }

        int matchesFound = 0;
        int tenthOfSecondsElapsed = 0;
        void TimeDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
                SetupGame();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeDisplay.Text = (tenthOfSecondsElapsed / 10f).ToString("0.0s");
        }
    }
}
