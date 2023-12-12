using Memory.BLL.BusinessObjects;

namespace Memory.DAL.Services
{
    public class ImageService
    {
        public CardImageRepository _CardImageRepository { get; set; } = new CardImageRepository();

        public void UploadImages(List<CardImage> images)
        {
            _CardImageRepository.CreateMultiple(images);
        }
    }
}
