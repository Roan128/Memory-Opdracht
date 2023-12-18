namespace Memory.GUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void StartBtn_Click(object sender, RoutedEventArgs e)
    {
        GameCreationWindow gameCreationWindow = new GameCreationWindow();
        gameCreationWindow.Show();
        Close();
    }
}
