using Memory.Model.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Card clickedCard)
            {
                if (!clickedCard.TurnedOver)
                {
                    // Set the display text for the clicked card
                    clickedCard.DisplayText = clickedCard.CardValue.ToString();
                }
            }
        }
    }
}
