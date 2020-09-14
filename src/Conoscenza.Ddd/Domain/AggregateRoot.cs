using System.Collections.Generic;
using System.Linq;

namespace Conoscenza.Ddd.Domain
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler where TId: ValueObject<TId>
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; }
        protected abstract void When<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();

        protected void Apply<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            When(domainEvent);
            EnsureValidState();
            _changes.Add(domainEvent);
        }

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void Load(IEnumerable<DomainEvent> history)
        {
            foreach (var e in history)
            {
                When(e);
                Version++;
            }
        }

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, DomainEvent domainEvent)
            => entity?.Handle(domainEvent);

        void IInternalEventHandler.Handle<TDomainEvent>(TDomainEvent domainEvent) => When(domainEvent);
    }
}
