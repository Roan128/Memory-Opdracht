using Memory.DAL.Services;
using Memory.Model.BusinessObjects;
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

        public ScoreService Scoreservice { get; set; }

        public ScoreWindow(Game game, Player player)
        {

            Game = game;
            Player = player;

            InitializeComponent();
            Scoreservice = new ScoreService();
            Score = new Score(player.Name);
            Score.CalculateScore(game);
            ScoreLabel.Content = Score.ScoreAmount;
            DataContext = this;
            ScoreLists.ItemsSource = Scoreservice.GetHighScores();
        }

        private void JaButton_Click(object sender, RoutedEventArgs e)
        {
            Scoreservice.SaveScore(Score);
            JaButton.IsEnabled = false;
            ScoreLists.ItemsSource = Scoreservice.GetHighScores();
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
