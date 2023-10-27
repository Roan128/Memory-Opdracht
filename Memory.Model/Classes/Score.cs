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

        public int CardAmount { get; set; }

        public Score(int scoreAmount, string playerName, int cardAmount)
        {
            ScoreAmount = scoreAmount;
            PlayerName = playerName;
            CardAmount = cardAmount;

            //Meteen na aanmaken object checken of hij in de database komt.
            CheckIfHighScore();
        }

        public void GetScore(string playername)
        {
            /*//Besteedde tijd berekenen
            TimeSpan timespent = EndTime - StartTime;
            int time = Math.Abs(timespent.Seconds);

            //Kwadraat van kaart uitrekenen
            int squareofcards = (int)Math.Pow(Cards.Count(), 2);

            //Score berekenen
            int scoreamount = (squareofcards / (time * Attempts)) * 1000;

            Score score = new Score(scoreamount, playername, Cards.Count());

            return score;*/
        }

        public void CheckIfHighScore()
        {

        }
    }
}
