using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + ". Play again?";
            }
        }

        private void SetUpGame()
        {
            const int matchCount = 8;
            List<string> animalEmoji2 = new List<string>()
            {
                "🦎", "🐶", "🐸", "🦝", "🐎", "🐘", "🦈", "🐒", "🐍","🐿", "🦥", "🦀", "🍤", "🐙", "🦅", "🐇", "🦁", "🐼", "🦌", "🐳", "🐢", "🐷"
            };
            List<string> animalEmojsiUsed = new List<string>();

            //animalEmoji.Add();
            var random = new Random();
            //Create list of pairs of emojis from animalEmoji2 equal to matchCount
            for (int i = 0; i < matchCount; i++)
            {
                int index = random.Next(animalEmoji2.Count);
                animalEmojsiUsed.Add(animalEmoji2[index]);
                animalEmojsiUsed.Add(animalEmoji2[index]);
                animalEmoji2.RemoveAt(index);
            }
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmojsiUsed.Count);
                    string nextEmoji = animalEmojsiUsed[index];
                    textBlock.Text = nextEmoji;
                    animalEmojsiUsed.RemoveAt(index);
                }

            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock; ;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if ( textBlock.Text == lastTextBlockClicked.Text )
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
