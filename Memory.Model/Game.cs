namespace Memory.Model
{
    public class Game
    {
        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; } 

        public List<Card> Cards = new List<Card> { };

        public int Attempts {  get; set; }

        public int GetScore()
        {
            //Besteedde tijd berekenen
            TimeSpan timespent = EndTime - StartTime;
            int time = Math.Abs(timespent.Seconds);

            //Kwadraat van kaart uitrekenen
            int squareofcards = (int)Math.Pow(Cards.Count(), 2);
            
            //Score berekenen
            int score = (squareofcards / (time * Attempts)) * 1000;

            return score;
        }
    }
}
