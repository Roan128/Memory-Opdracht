namespace Memory.GUI;

public partial class GameWindow : Window
{
    public Game Game { get; set; }

    public GameService gameService { get; set; }

    public Player Player { get; set; }

    public List<Button> Selected { get; set; } = new List<Button>();

    public bool UsesSet { get; set; } = false;

    //Stap 1: opzetten game window zonder set
    public GameWindow(Game game, Player player)
    {
        InitializeComponent();
        DataContext = this;

        //Eerder aangemaakte game en player setten
        Game = game;
        Player = player;

        gameService = new GameService(Game);

        //Kaarten genereren op basis van aantal aangegeven kaarten.
        gameService.GenerateCards(player.CardAmount);
        gameService.ShuffleCards();

        //De datasource meegeven zodat kaarten weergegeven worden.
        DisplayedCards.ItemsSource = game.Cards;

        //Attempts tonen
        UpdateAttemptsLabel();
    }

    //Stap 1: opzetten game window met set
    public GameWindow(Game game, Player player, ImageSet set)
    {
        InitializeComponent();
        DataContext = this;
        Game = game;
        Player = player;
        gameService = new GameService(Game);
        gameService.GenerateCards(set);
        gameService.ShuffleCards();
        UsesSet = true;
        DisplayedCards.ItemsSource = game.Cards;
        UpdateAttemptsLabel();
    }

    //Gokken of kaarten gelijk zijn.
    private void GuessBtn_Click(object sender, RoutedEventArgs e)
    {
        //Gok knop is ingedrukt, dus mag uit.
        GuessBtn.Visibility = Visibility.Hidden;
        List<Card> cards = new List<Card>();

        //Kaarten ophalen uit de datacontext van de button.
        foreach (Button selectedButton in Selected)
        {
            if (selectedButton.DataContext is Card card)
            {
                if (UsesSet == false)
                {
                    selectedButton.Content = $"Value: {card.CardValue}";
                }
                else
                {
                    Image imageControl = new Image();
                    imageControl.Source = LoadImage(card.Image.ImageData);
                    selectedButton.Content = imageControl;
                }
                cards.Add(card);
            }
        }

        //Card value met elkaar vergelijken.
        if (cards[0].CardValue.Equals(cards[1].CardValue))
        {
            //Met linq de kaarten op turnedover zetten als ze hetzelfde zijn. Spel gaat meteen door
            Game.Cards
             .Where(c => c.Id == cards[0].Id || c.Id == cards[1].Id)
             .ToList()
             .ForEach(matchingCard => matchingCard.TurnedOver = true);
            Game.Attempts += 1;
            Selected.Clear();
        }
        else
        {
            //Zo niet, dan wordt er alsnog een attempt bij gedaan en kan de speler rustig even de values onthouden voordat hij continue drukt om naar de volgende attempt te gaan.
            Game.Attempts += 1;
            ContinueBtn.Visibility = Visibility.Visible;
        }
        UpdateAttemptsLabel();

        //Na elke guess even kijken of de game eigenlijk niet al over is (Nieuwe button zodat er naar de score gegaan kan worden nog neerzetten.)
        if (!gameService.CheckIfGameOver())
        {
            FinishBtn.Visibility = Visibility.Visible;
        }
    }

    //Selecteren van kaarten.
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //Button setten
        Button button = (Button)sender;

        //Kijken of het echt wel een kaart is  en of er niet al twee geselcteerd zijn.
        if (button.DataContext is Card clickedCard && Selected.Count != 2)
        {
            //Kijken of de kaart niet al is turnedover is geweest nadat hij correct was.
            if (!clickedCard.TurnedOver)
            {
                //Zo niet dan wordt de knop uitgezet, zodat de kaart niet nog is geselecteerd kan worden. Ook toevegen aan de selected array.
                button.IsEnabled = false;
                Selected.Add(button);

                //Als er twee geselecteerd zijn, kan er gegokt worden.
                if (Selected.Count == 2)
                {
                    GuessBtn.Visibility = Visibility.Visible;
                }
            }
        }
    }

    //Voor het doorgaan na een foute ronde.
    private void ContinueBtn_Click(object sender, RoutedEventArgs e)
    {
        foreach (var selectedButton in Selected)
        {
            selectedButton.IsEnabled = true;
            selectedButton.Content = $"Id: {(selectedButton.DataContext as Card)?.Id}";
        }
        Selected.Clear();

        ContinueBtn.Visibility = Visibility.Hidden;
    }

    private void FinishBtn_Click(object sender, RoutedEventArgs e)
    {
        ScoreWindow scoreWindow = new ScoreWindow(Game, Player);
        scoreWindow.Show();
        Close();
    }

    private void UpdateAttemptsLabel()
    {
        AttemptsLabel.Content = $"Attempts: {Game.Attempts}";
    }

    private static BitmapImage LoadImage(byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0) return null;
        var image = new BitmapImage();
        using (var mem = new MemoryStream(imageData))
        {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
        }
        image.Freeze();
        return image;
    }
}
