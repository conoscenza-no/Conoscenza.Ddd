using System;
using Conoscenza.Ddd.Domain;

namespace KnowledgeConsole.Domain.Fact
{
    public class Comment : Entity<CommentId>
    {
        public string Text { get; private set; }

        public Comment(Action<DomainEvent> applier) : base(applier) {} 

        protected override void When<TDomainEvent>(TDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case Events.CommentReceived e:
                    Text = e.Comment;
                    break;
            }
        }
    }
}
