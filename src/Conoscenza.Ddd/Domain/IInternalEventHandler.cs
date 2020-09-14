namespace Conoscenza.Ddd.Domain
{
    public interface IInternalEventHandler
    {
        void Handle<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
    }
}
