namespace Memory.Model.BusinessObjects;

public class Score
{
    public Guid Id { get; set; }

    public int ScoreAmount { get; set; }

    public string PlayerName { get; set; }

    public Score() { }

    public Score(string name)
    {
        Id = Guid.NewGuid();
        PlayerName = name;
    }

    //Double gebruiken tijdens het rekenen. Daarna casten naar int voor result
    public void CalculateScore(Game game)
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
}
