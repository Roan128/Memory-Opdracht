namespace Memory.GUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        public async void NavigateToGame(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        private void CounterBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void Btn_Clicked(object sender, EventArgs e)
        {

        }
    }
}