namespace Memory.DAL.Services;

public class ImageSetService
{
    ImageSetRepository imageSetRepository = new ImageSetRepository();

    public ImageSetService()
    {

    }

    public ImageSetService(ImageSetRepository imagesetRepository)
    {
        imageSetRepository = imagesetRepository;
    }

    public void UploadSet(ImageSet set)
    {
        imageSetRepository.Create(set);
    }

    public List<ImageSet> GetImageSets()
    {
        var sets = (List<ImageSet>)imageSetRepository.GetAll();
        return sets;
    }
}
