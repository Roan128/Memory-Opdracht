﻿using Memory.BLL.BusinessObjects;

namespace Memory.DAL.Services
{
    public class ImageSetService
    {
        ImageSetRepository imageSetRepository = new ImageSetRepository();

        public void UploadSet(ImageSet set)
        {
            imageSetRepository.Create(set);
        }

        public List<ImageSet> GetImageSets()
        {
            List<ImageSet> sets = new List<ImageSet>();
            sets = (List<ImageSet>)imageSetRepository.GetAll();
            return sets;
        }
    }
}