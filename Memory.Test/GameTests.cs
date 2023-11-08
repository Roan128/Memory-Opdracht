

using Memory.Model.Classes;
using Memory.Model.Exceptions;

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
        [TestCase(10)]
        [TestCase(20)]
        public void Test_GenerateCards_ShouldGenerateCards(int number)
        {
            //Setup
            _testGame.GenerateCards(number);

            //Assert
            Assert.That(number * 2, Is.EqualTo(_testGame.Cards.Count));
        }

        [TestCase(-10)]
        [TestCase(0)]
        public void Test_GenerateCards_ShouldNotGenerateCards(int number)
        {
            //Setup
            _testGame.GenerateCards(number);

            //Assert
            Assert.Throws();
        }

        //Tests for Check()
        [Test]
        public void Test_Check_ShouldReturn()
        {
            //Setup
            _testGame.GenerateCards(1);
            Card card = _testGame.Cards[0];

            //Simulate input
            Card card2 = _testGame.Check("1");

            //Assert
            Assert.That(card, Is.EqualTo(card2));
        }

        [TestCase("1000")]
        [TestCase("text")]
        public void Test_Check_ShouldGiveException(string input)
        {
            //Setup
            _testGame.GenerateCards(1);
            Card card = _testGame.Cards[0];

            //Assert
            Assert.Throws<CardNotFoundException>(() => _testGame.Check(input));
        }

        //Tests for CheckIfGameOver()
        [Test]
        public void Test_CheckIfGameOver_ShouldReturnFalse()
        {
            _testGame.GenerateCards(1);
            _testGame.Cards = _testGame.Cards.Select(c => { 
                c.TurnedOver = true;
                return c;
            }).ToList();

            Assert.That(_testGame.CheckIfGameOver(), Is.False);  
        }

        [Test]
        public void Test_CheckIfGameOver_ShouldReturnTrue()
        {
            _testGame.GenerateCards(1);
            Assert.That(_testGame.CheckIfGameOver(), Is.True);
        }
    }
}