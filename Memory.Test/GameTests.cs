namespace Memory.Test;

public class GameTests
{
    public Game _testGame;
    public Player _testplayer;

    [SetUp]
    public void Setup()
    {
        _testGame = new Game();
        _testplayer = new Player("Test player");
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
        _testGame.GenerateCards(number);

        //Assert
        Assert.That(int.Parse(number) * 2, Is.EqualTo(_testGame.Cards.Count));
    }

    [TestCase("niet echt een nummer", typeof(FormatException))]
    [TestCase(null, typeof(ArgumentNullException))]
    public void Test_GenerateCards_ShouldThrow(string number, Type expectedExceptionType)
    {
        //Assert
        Assert.Throws(expectedExceptionType, () => _testGame.GenerateCards(number));
    }

    [Test]
    public void Test_ShuffleCards_ShouldChangeOrder()
    {
        //Setup
        List<Card> originalCards = _testGame.GenerateCards("4");

        //Act
        _testGame.ShuffleCards();

        //Assert
        Assert.That(_testGame.Cards.SequenceEqual(originalCards), Is.False);
    }

    [Test]
    public void Test_GetCardFromCards_ShouldReturnCard()
    {
        //Setup
        _testGame.GenerateCards("4");

        //Act
        Card firstCard = _testGame.GetCardFromCards("1");

        //Assert
        Assert.That(firstCard.Equals(_testGame.Cards[0]));
    }

    [TestCase("niet echt een nummer")]
    [TestCase(null)]
    [TestCase("10000")]
    public void Test_GetCardFromCards_ShouldThrow(string input)
    {
        //Setup
        _testGame.GenerateCards("4");

        //Act and assert
        Assert.Throws<CardNotFoundException>(() => _testGame.GetCardFromCards(input));
    }

    [Test]
    public void Test_CheckIfGameOver_ShouldReturnFalse()
    {
        //Setup
        _testGame.GenerateCards("1");
        _testGame.Cards = _testGame.Cards.Select(c =>
        {
            c.TurnedOver = true;
            return c;
        }).ToList();

        //Act and assert
        Assert.That(_testGame.CheckIfGameOver(), Is.False);
    }

    [Test]
    public void Test_CheckIfGameOver_ShouldReturnTrue()
    {
        //Setup
        _testGame.GenerateCards("1");

        //Act and assert
        Assert.That(_testGame.CheckIfGameOver(), Is.True);
    }
}