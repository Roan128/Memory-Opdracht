namespace Memory.Model.Classes
{
    public class Card
    {
        public int Id { get; set; }

        public int CardValue { get; set; }

        public bool TurnedOver { get; set; } = false;

        public string DisplayText { get; set; } = string.Empty;

        public Card(int id)
        {
            Id = id;
        }

        public Card(int id, int value)
        {
            Id = id;
            CardValue = value;
        }
    }
}
