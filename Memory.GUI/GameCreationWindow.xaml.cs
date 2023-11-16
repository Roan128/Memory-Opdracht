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
        string cardamount = TextBoxCards.Text;
        Player player = new Player(playername, cardamount);
        Game game = new Game();
        GameWindow gameWindow = new GameWindow(game, player);
        gameWindow.Show();
        Close();
    }
}
