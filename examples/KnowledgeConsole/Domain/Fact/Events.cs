using System;
using Conoscenza.Ddd.Domain;

namespace KnowledgeConsole.Domain.Fact
{
    public static class Events
    {
        public class Created : DomainEvent
        {
            public Guid Id { get; }
            public string Title { get; }

            public Created(Guid id, string title)
            {
                Id = id;
                Title = title;
            }

            public override string ToString()
            {
                return $"[{GetType().Name}] => {Title}";
            }
        }

        public class TitleUpdated : DomainEvent
        {
            public Guid Id { get; }
            public string Title { get; }

            public TitleUpdated(Guid id, string title)
            {
                Id = id;
                Title = title;
            }
            public override string ToString()
            {
                return $"[{GetType().Name}] => {Title}";
            }
        }

        public class BodyUpdated : DomainEvent
        {
            public Guid Id { get; }
            public string Body { get; }

            public BodyUpdated(Guid id, string body) 
            {
                Id = id;
                Body = body;
            }

            public override string ToString()
            {
                return $"[{GetType().Name}] => {Body}";
            }

        }

        public class Published : DomainEvent
        {
            public Guid Id { get; }
            public Published(Guid id) => Id = id;

            public override string ToString()
            {
                return $"[{GetType().Name}]";
            }
        }

        public class UpvoteReceived : DomainEvent
        {
            public Guid Id { get; }
            public UpvoteReceived(Guid id) => Id = id;

            public override string ToString()
            {
                return $"[{GetType().Name}]";
            }
        }

        public class DownvoteReceived : DomainEvent
        {
            public Guid Id { get; }
            public DownvoteReceived(Guid id) => Id = id;

            public override string ToString()
            {
                return $"[{GetType().Name}]";
            }
        }

        public class CommentReceived : DomainEvent
        {
            public Guid Id { get; }
            public string Comment { get; }

            public CommentReceived(Guid id, string comment)
            {
                Id = id;
                Comment = comment;
            }

            public override string ToString()
            {
                return $"[{GetType().Name}] => {Comment}";
            }
        }

        public class Deleted : DomainEvent
        {
            public Guid Id { get; }
            public Deleted(Guid id) => Id = id;

            public override string ToString()
            {
                return $"[{GetType().Name}]";
            }
        }
    }
}
