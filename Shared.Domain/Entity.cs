using System;

namespace Shared.Domain
{
    public class Entity<T>
    {
        public T Id { get; protected set; }

        public DateTime CreatedAt { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsHidden { get; private set; }
    }
}
