namespace Memory.BLL.BusinessObjects;

public class Game
{
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime EndTime { get; set; }

    public List<Card> Cards = new List<Card>();

    public int Attempts { get; set; }
}
