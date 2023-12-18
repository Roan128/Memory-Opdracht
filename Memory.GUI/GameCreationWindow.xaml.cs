namespace Memory.GUI;

public partial class GameCreationWindow : Window
{
    private ImageSetService imageSetService { get; set; }

    private ImageSet? selectedSet { get; set; } = null;

    private Button? selectedButton { get; set; } = null;

    public GameCreationWindow()
    {
        imageSetService = new ImageSetService();
        InitializeComponent();
        DisplayedSets.ItemsSource = imageSetService.GetImageSets();
    }

    //-------- Buttons -----------//
    private void StartBtn_Click(object sender, RoutedEventArgs e)
    {
        if (selectedSet == null)
        {
            if (!IsValidNumber(TextBoxCards.Text) || TextBoxName.Text.Equals("") || TextBoxName.Text.Equals(null))
            {
                // Display an error message or take appropriate action
                MessageBox.Show("Invalid number or name entered. Please enter a valid number or name.");
                // Optionally, clear the TextBox or set a default value
                TextBoxCards.Text = "";
            }
            else
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
        else
        {
            string playername = TextBoxName.Text;
            Player player = new Player(playername);
            Game game = new Game();
            GameWindow gameWindow = new GameWindow(game, player, selectedSet);
            gameWindow.Show();
            Close();
        }
    }

    private void CreateSetBtn_Click(object sender, RoutedEventArgs e)
    {
        SetCreationPopup popup = new SetCreationPopup();
        popup.onClose += HandleCustomEvent;
        popup.Show();
    }

    private void SetSelect_Click(object sender, RoutedEventArgs e)
    {
        TextBoxCards.IsEnabled = false;
        TextBoxCards.Text = "Disabled...";

        //Button setten
        if (selectedSet == null)
        {
            Button button = (Button)sender;
            if (button.DataContext is ImageSet clickedSet)
            {
                selectedSet = clickedSet;
                selectedButton = button;
                button.Background = Brushes.Blue;
            }
        }
        else
        {
            selectedSet = null;
            selectedButton.Background = new SolidColorBrush(Color.FromRgb(35, 83, 161));

            Button button = (Button)sender;
            if (button.DataContext is ImageSet clickedSet)
            {
                selectedSet = clickedSet;
                selectedButton = button;
                button.Background = Brushes.Blue;
            }
        }
    }

    private bool IsValidNumber(string input)
    {
        if (int.TryParse(input, out _))
        {
            if (int.Parse(input) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }

    private void HandleCustomEvent(object? sender, EventArgs e)
    {
        DisplayedSets.ItemsSource = imageSetService.GetImageSets();
    }
}
