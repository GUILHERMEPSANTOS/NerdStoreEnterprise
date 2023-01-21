using Core.Messages;

namespace Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        private List<Event> _events;
        public IReadOnlyCollection<Event> Notification => _events?.AsReadOnly();

        public void AddEvent(Event @event)
        {
            _events ??= new List<Event>();
            _events.Add(@event);    
        }
        public void RemoveEvent(Event _eventItem)
        {
            _events?.Remove(_eventItem);
        }

        public void ClearEvents()
        {
            _events?.Clear();
        }

        #region Comparisons
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;


            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;


            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string? ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
        #endregion
    }
}