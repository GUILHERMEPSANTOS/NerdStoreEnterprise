namespace Core.DomainObjects.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        
    }
}