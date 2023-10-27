

using Memory.Model.Classes;

namespace Memory.Test
{
    public class Tests
    {
        public Game _testGame;   
        public Player _testplayer;

        [SetUp]
        public void Setup()
        {
            _testGame = new Game();
            _testplayer = new Player("Test player");
        }

        
    }
}