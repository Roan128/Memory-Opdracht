namespace Memory.BLL.Interfaces;

public interface IRepository<T> where T : class
{
    public ICollection<T> GetAll();

    public bool Create(T businessObject);
}
