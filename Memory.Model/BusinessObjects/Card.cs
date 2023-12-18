namespace Memory.BLL.BusinessObjects;

public class Card
{
    public int Id { get; set; }

    public int CardValue { get; set; }

    public bool TurnedOver { get; set; } = false;

    public CardImage? Image { get; set; }

    //Zonder image
    public Card(int id, int value)
    {
        Id = id;
        CardValue = value;
    }

    public Card(int id, CardImage image, int value)
    {
        Id = id;
        Image = image;
        CardValue = value;
    }
}
