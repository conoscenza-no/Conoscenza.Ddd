using System;

namespace Conoscenza.Ddd.Domain
{
    public abstract class DomainEvent
    {
        public DateTimeOffset UtcTimestamp { get; }

        protected DomainEvent()
        {
            UtcTimestamp = DateTimeOffset.UtcNow;
        }
    }
}
