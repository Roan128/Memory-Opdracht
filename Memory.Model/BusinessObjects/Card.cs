using Memory.BLL.BusinessObjects;

namespace Memory.Model.BusinessObjects
{
    public class Card
    {
        public int Id { get; set; }

        public int? CardValue { get; set; }

        public bool TurnedOver { get; set; } = false;

        public CardImage? Image { get; set; }

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
