using System;

namespace Conoscenza.Ddd.Domain
{
    public abstract class Entity<TId> : IInternalEventHandler where TId : ValueObject<TId>
    {
        public TId Id { get; protected set; }
        private readonly Action<DomainEvent> _applier;

        protected Entity(Action<DomainEvent> applier) => _applier = applier;
        protected abstract void When<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
        protected void Apply<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            When(domainEvent);
            _applier(domainEvent);
        }

        void IInternalEventHandler.Handle<TDomainEvent>(TDomainEvent domainEvent) => When(domainEvent);
    }
}
