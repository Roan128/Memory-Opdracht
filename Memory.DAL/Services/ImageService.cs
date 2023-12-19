namespace Memory.DAL.Services;

public class ImageService
{
    public CardImageRepository cardImageRepository { get; set; } = new CardImageRepository();

    public ImageService()
    {

    }

    public ImageService(CardImageRepository cardimageRepository)
    {
        cardImageRepository = cardimageRepository;
    }

    public void UploadImages(List<CardImage> images)
    {
        cardImageRepository.CreateMultiple(images);
    }

    public List<CardImage> getImagesBySetId(ImageSet set)
    {
        var images = cardImageRepository.GetImagesBySetId(set.Id);
        return images;
    }
}
