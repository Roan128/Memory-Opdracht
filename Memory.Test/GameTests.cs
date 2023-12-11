

using Memory.Model.Classes;

namespace Memory.Test
{
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

        //Tests for GenerateCards()
        [TestCase("10")]
        [TestCase("20")]
        public void Test_GenerateCards_ShouldGenerateCards(string number)
        {
            //Setup
            _testGame.GenerateCards(number);

            //Assert
            Assert.That(int.Parse(number) * 2, Is.EqualTo(_testGame.Cards.Count));
        }

        /*[TestCase("-10")]
        [TestCase("0")]
        public void Test_GenerateCards_ShouldNotGenerateCards(string number)
        {
            //Setup
            _testGame.GenerateCards(number);

            //Assert
            Assert.Throws();
        }*/

        //Tests for CheckIfGameOver()
        [Test]
        public void Test_CheckIfGameOver_ShouldReturnFalse()
        {
            _testGame.GenerateCards("1");
            _testGame.Cards = _testGame.Cards.Select(c =>
            {
                c.TurnedOver = true;
                return c;
            }).ToList();

            Assert.That(_testGame.CheckIfGameOver(), Is.False);
        }

        [Test]
        public void Test_CheckIfGameOver_ShouldReturnTrue()
        {
            _testGame.GenerateCards("1");
            Assert.That(_testGame.CheckIfGameOver(), Is.True);
        }
    }
}