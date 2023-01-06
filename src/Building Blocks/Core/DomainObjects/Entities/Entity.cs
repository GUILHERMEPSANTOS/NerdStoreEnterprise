using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

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
    }
}