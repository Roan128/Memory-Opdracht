using Memory.DAL.Services;

namespace Memory.Test;

public class GameTests
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

    [TestCase("10")]
    [TestCase("20")]
    public void Test_GenerateCards_ShouldGenerateCards(string number)
    {
        //Setup
        _testGameService.GenerateCards(number);

        //Assert
        Assert.That(int.Parse(number) * 2, Is.EqualTo(_testGame.Cards.Count));
    }

    [TestCase("niet echt een nummer", typeof(FormatException))]
    [TestCase(null, typeof(ArgumentNullException))]
    public void Test_GenerateCards_ShouldThrow(string number, Type expectedExceptionType)
    {
        //Assert
        Assert.Throws(expectedExceptionType, () => _testGameService.GenerateCards(number));
    }

    [Test]
    public void Test_ShuffleCards_ShouldChangeOrder()
    {
        //Setup
        List<Card> originalCards = _testGameService.GenerateCards("4");

        //Act
        _testGameService.ShuffleCards();

        //Assert
        Assert.That(_testGame.Cards.SequenceEqual(originalCards), Is.False);
    }

    [Test]
    public void Test_GetCardFromCards_ShouldReturnCard()
    {
        //Setup
        _testGameService.GenerateCards("4");

        //Act
        Card firstCard = _testGameService.GetCardFromCards("1");

        //Assert
        Assert.That(firstCard.Equals(_testGame.Cards[0]));
    }

    [TestCase("niet echt een nummer")]
    [TestCase(null)]
    [TestCase("10000")]
    public void Test_GetCardFromCards_ShouldThrow(string input)
    {
        //Setup
        _testGameService.GenerateCards("4");

        //Act and assert
        Assert.Throws<CardNotFoundException>(() => _testGameService.GetCardFromCards(input));
    }

    [Test]
    public void Test_CheckIfGameOver_ShouldReturnFalse()
    {
        //Setup
        _testGameService.GenerateCards("1");
        _testGame.Cards = _testGame.Cards.Select(c =>
        {
            c.TurnedOver = true;
            return c;
        }).ToList();

        //Act and assert
        Assert.That(_testGameService.CheckIfGameOver(), Is.False);
    }

    [Test]
    public void Test_CheckIfGameOver_ShouldReturnTrue()
    {
        //Setup
        _testGameService.GenerateCards("1");

        //Act and assert
        Assert.That(_testGameService.CheckIfGameOver(), Is.True);
    }
}