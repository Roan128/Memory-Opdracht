namespace Memory.BLL.BusinessObjects
{
    public class ImageSet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ImageSet(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
