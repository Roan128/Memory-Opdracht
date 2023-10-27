using Memory.Model.Classes;

Console.WriteLine("Welkom, geef je naam op.");
string name = Console.ReadLine();

Player player = new Player(name);

Game game = new Game();

game.StartGame(player);


