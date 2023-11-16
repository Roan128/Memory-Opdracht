using Memory.Model.Classes;
using System.Collections.ObjectModel;
using System.Windows;

namespace Memory.GUI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public Game Game { get; set; }

        public Player Player { get; set; }

        public ObservableCollection<Card> Cards { get; }
        public GameWindow(Game game, Player player)
        {
            InitializeComponent();
            DataContext = this;
            this.Game = game;
            this.Player = player;

            game.GenerateCards(player.CardAmount);
            game.ShuffleCards();

            Cards = new ObservableCollection<Card>(game.Cards);


        }

        private void GuessBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
