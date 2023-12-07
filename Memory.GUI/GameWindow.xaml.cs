using Memory.Model.Classes;
using System.Collections.Generic;
using System.Linq;
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

        public List<Button> Selected { get; set; } = new List<Button>();

        public GameWindow(Game game, Player player)
        {
            InitializeComponent();
            DataContext = this;
            this.Game = game;
            this.Player = player;
            game.GenerateCards(player.CardAmount);
            game.ShuffleCards();
            DisplayedCards.ItemsSource = game.Cards;
            AttemptsLabel.Content = $"Attempts: {game.Attempts}";
        }

        private void GuessBtn_Click(object sender, RoutedEventArgs e)
        {
            GuessBtn.Visibility = Visibility.Hidden;
            List<Card> cards = new List<Card>();
            foreach (Button selectedButton in Selected)
            {
                if (selectedButton.DataContext is Card card)
                {
                    selectedButton.Content = "Value: " + card.CardValue;
                    cards.Add(card);
                }
            }

            if (Game.Compare(cards[0], cards[1]))
            {
                Game.Attempts += 1;
                Selected.Clear();
            }
            else
            {
                Game.Attempts += 1;
                foreach (var selectedButton in Selected)
                {
                    selectedButton.IsEnabled = true;
                    selectedButton.Content = "Id: " + (selectedButton.DataContext as Card)?.Id;
                }
                Selected.Clear();
            }
            AttemptsLabel.Content = $"Attempts: {Game.Attempts}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.DataContext is Card clickedCard && Selected.Count() != 2)
            {
                if (!clickedCard.TurnedOver)
                {
                    button.IsEnabled = false;
                    // Set the display text for the clicked card
                    Selected.Add(button);
                    if (Selected.Count() == 2)
                    {
                        GuessBtn.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
