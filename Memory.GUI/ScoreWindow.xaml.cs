using Memory.Model.Classes;
using System.Windows;

namespace Memory.GUI
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        public Game Game { get; set; }

        public Player Player { get; set; }

        public Score Score { get; set; }

        public ScoreWindow(Game game, Player player)
        {

            Game = game;
            Player = player;

            InitializeComponent();

            Score = new Score(player.Name);
            Score.GetScore(game);
            ScoreLabel.Content = Score.ScoreAmount;
            DataContext = this;
            ScoreLists.ItemsSource = Score.GetHighScores(false);
        }

        private void JaButton_Click(object sender, RoutedEventArgs e)
        {
            JaButton.IsEnabled = false;
            Score.SaveScore(Score);
            ScoreLists.ItemsSource = Score.GetHighScores(false);
            NeeButton.Visibility = Visibility.Collapsed;
            JaButton.Visibility = Visibility.Collapsed;
            ScoreAsk.Visibility = Visibility.Collapsed;
            CloseButton.Visibility = Visibility.Visible;
        }

        private void NeeButton_Click(object sender, RoutedEventArgs e)
        {
            NeeButton.Visibility = Visibility.Collapsed;
            JaButton.Visibility = Visibility.Collapsed;
            ScoreAsk.Visibility = Visibility.Collapsed;
            CloseButton.Visibility = Visibility.Visible;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
