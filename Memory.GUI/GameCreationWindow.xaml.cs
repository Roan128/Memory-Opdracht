using Memory.Model.Classes;
using System.Windows;

namespace Memory.GUI;

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class GameCreationWindow : Window
{
    public GameCreationWindow()
    {
        InitializeComponent();
    }

    private void StartBtn_Click(object sender, RoutedEventArgs e)
    {
        string playername = TextBoxName.Text;

        if (!IsValidNumber(TextBoxCards.Text))
        {
            // Display an error message or take appropriate action
            MessageBox.Show("Invalid number entered. Please enter a valid number.");
            // Optionally, clear the TextBox or set a default value
            TextBoxCards.Text = "";
        }
        else
        {
            string cardamount = TextBoxCards.Text;
            Player player = new Player(playername, cardamount);
            Game game = new Game();
            GameWindow gameWindow = new GameWindow(game, player);
            gameWindow.Show();
            Close();
        }
    }

    private bool IsValidNumber(string input)
    {
        return int.TryParse(input, out _);
    }
}
