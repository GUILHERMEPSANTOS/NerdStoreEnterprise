namespace Core.DomainObjects.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        
    }
}