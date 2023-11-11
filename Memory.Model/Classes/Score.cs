using Memory.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Model.Classes
{
    public class Score
    {
        public Guid Id { get; set; } 

        public int ScoreAmount { get; set; }

        public string PlayerName { get; set; }

        ScoreRepository scorerepository = new ScoreRepository("Server=(localdb)\\mssqllocaldb;Database=ScoreDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        public Score() { }

        public Score(string name)
        {
           Id = Guid.NewGuid();
           PlayerName = name;
        }

        //Double gebruiken tijdens het rekenen. Daarna casten naar int voor result
        public void GetScore(Game game)
        {
            //Besteedde tijd berekenen
            TimeSpan timespent = game.EndTime - game.StartTime;
            double time = Math.Abs(timespent.Seconds);

            //Score berekenen
            double squareofcards = (int)Math.Pow(game.Cards.Count(), 2);
            double scorepart1 = time * game.Attempts;
            double scorepart2 = squareofcards / scorepart1;
            double finalscore = scorepart2 * 1000.0;
            
            //Score setten
            ScoreAmount = (int)finalscore;
        }

        public void CheckIfHighScore()
        {

        }

        public void SaveScore(Score score)
        {
            scorerepository.Insert(score);
        }

        public void GetHighScores()
        {
            List<Score> scorelist = scorerepository.GetAll().OrderByDescending(c => c.ScoreAmount).Take(10).ToList();
            DisplayHighScore(scorelist);
        }

        public void DisplayHighScore(List<Score> scores)
        {
            Console.WriteLine("Highscores (Top 10): ");
            foreach (Score score in scores)
            {
                Console.WriteLine($"Player: {score.PlayerName}, Score: {score.ScoreAmount}");
            }
        }
    }
}
