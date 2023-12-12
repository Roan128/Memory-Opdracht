using Memory.BLL.BusinessObjects;

namespace Memory.BLL.Interfaces;

public interface ICardImageRepository : IRepository<CardImage>
{
    public bool CreateMultiple(List<CardImage> values);

    public List<CardImage> GetCardsBySetId(Guid setId);
}
