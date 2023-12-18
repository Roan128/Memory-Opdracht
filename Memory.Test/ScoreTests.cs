namespace Memory.Test;

public class ScoreTests
{
    public Game _testGame;
    public Player _testplayer;
    public GameService _testGameService;

    [SetUp]
    public void Setup()
    {
        _testGame = new Game();
        _testplayer = new Player("Test player");
        _testGameService = new GameService(_testGame);
    }

    [TearDown]
    public void TearDown()
    {
        _testGame.Cards.Clear();
    }
}
