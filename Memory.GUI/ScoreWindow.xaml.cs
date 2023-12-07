using Memory.Model.Classes;
using System.Collections.Generic;
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

        public List<Score> TopScores { get; set; } = new List<Score>();

        public ScoreWindow(Game game, Player player)
        {

            Game = game;
            Player = player;

            InitializeComponent();

            Score = new Score(player.Name);
            Score.GetScore(game);
            TopScores = Score.GetHighScores(false);
            DataContext = this;
            ScoreLists.ItemsSource = TopScores;
        }
    }
}
