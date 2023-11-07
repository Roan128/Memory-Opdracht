using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Model.Classes
{
    public class Score
    {
        public int ScoreAmount { get; set; }

        public string PlayerName { get; set; }

        public Score(string name)
        {
           PlayerName = name;
        }

        public void GetScore(Game game)
        {
            //Besteedde tijd berekenen
            TimeSpan timespent = game.EndTime - game.StartTime;
            int time = Math.Abs(timespent.Seconds);
            Console.WriteLine($"Time spent: {time}");

            //Kwadraat van kaart uitrekenen
            int squareofcards = (int)Math.Pow(game.Cards.Count(), 2);
            Console.WriteLine($"Kwadraat van kaarten {squareofcards}");
            Console.WriteLine($"Pogingen: {game.Attempts}");

            //Score berekenen
            int scoreamount = (squareofcards / (time * game.Attempts)) * 1000;

            this.ScoreAmount = scoreamount;
        }

        public void CheckIfHighScore()
        {

        }

        //TODO: Database maken, testen schrijven
    }
}
