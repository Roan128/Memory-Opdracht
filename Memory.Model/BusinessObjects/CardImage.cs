namespace Memory.BLL.BusinessObjects;

public class CardImage
{
    public Guid Id { get; set; }

    public Guid SetId { get; set; }

    public byte[]? ImageData { get; set; }

    public CardImage() { }

    public CardImage(byte[] imageData)
    {
        ImageData = imageData;
        Id = Guid.NewGuid();
    }
}
