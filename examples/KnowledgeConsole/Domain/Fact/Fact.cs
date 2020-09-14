using System.Collections.Generic;
using System.Linq;
using Conoscenza.Ddd.Domain;

namespace KnowledgeConsole.Domain.Fact
{
    public class Fact : AggregateRoot<FactId>
    {
        private readonly List<Comment> _comments = new List<Comment>();

        public string Title { get; private set; }
        public string Body { get; private set; }
        public int Upvotes { get; private set; }
        public int Downvotes { get; private set; }
        public IEnumerable<Comment> Comments => _comments.AsEnumerable();
        public FactState State { get; private set; }

        private Fact() { }

        
        public Fact(FactId id, string title)                                          
        {
            // Enforcing business rule: "A fact needs at least a title to exist"
            Apply(new Events.Created(id, title));
        }


        public void UpdateTitle(string title)
        {
            // Enforcing business rule: "A fact title cannot be empty and must be at leat 5 char long"
            if (!string.IsNullOrWhiteSpace(title) && title.Length >= 5)
            {
                Apply(new Events.TitleUpdated(Id, title));
            }
        }

        public void UpdateBody(string body)
        {
            // Enforcing business rule: "Only facts witch status 'Draft' can be changed"
            if (State == FactState.Draft)
            {
                Apply(new Events.BodyUpdated(Id, body));
            }
        }

        public void Publish()
        {
            // Enforcing business rule: "Only drafts of facts can be published"
            if (State == FactState.Draft)
            {
                Apply(new Events.Published(Id));
            }
        }

        public void Upvote()
        {
            // Enforcing business rule: "Only published facts can be upvoted"
            if (State == FactState.Published)
            {
                Apply(new Events.UpvoteReceived(Id));
            }
        }

        public void Downvote()                                                        
        {
            // Enforcing business rule: "Only published facts can be downvoted"
            if (State == FactState.Published)
            {
                Apply(new Events.UpvoteReceived(Id));
            }
        }

        public void Comment(string comment)
        {
            // Enforcing business rule: "Only published facts can receive comments"
            if (State == FactState.Published)
            {
                Apply(new Events.CommentReceived(Id, comment));
            }
        }

        public void Delete()
        {
            // Enforcing business rule: "Only facts not already deleted can be deleted"
            if (State != FactState.Deleted)
            {
                Apply(new Events.Deleted(Id));
            }
        }

        protected override void When<TDomainEvent>(TDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case Events.Created e:
                    Id = new FactId(e.Id);
                    Title = e.Title;
                    State = FactState.Draft;                  // Enforcing business rule: "Newly created facts should have status 'Draft'
                    break;
                case Events.TitleUpdated e:
                    Title = e.Title;
                    break;
                case Events.BodyUpdated e:
                    Body = e.Body;
                    break;
                case Events.Published _:
                    State = FactState.Published;
                    break;
                case Events.UpvoteReceived _:
                    Upvotes++;
                    break;
                case Events.DownvoteReceived _:
                    Downvotes++;
                    break;
                case Events.CommentReceived e:
                    var comment = new Comment(Apply);
                    ApplyToEntity(comment, e);
                    _comments.Add(comment);
                    break;
                case Events.Deleted _:
                    State = FactState.Deleted;
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            var valid =
                Id != default &&
                !string.IsNullOrWhiteSpace(Title) &&
                State switch
                {
                    FactState.Draft =>
                        !string.IsNullOrWhiteSpace(Title)
                        && Title.Length >= 5,
                    FactState.Published =>
                        !string.IsNullOrWhiteSpace(Title)
                        && Title.Length >= 5
                        && !string.IsNullOrWhiteSpace(Body),
                    _ => true
                };

            if (!valid) throw new InvalidEntityStateException(this, $"Post-checks failed in state { State }");
        }
        
        public enum FactState
        {
            Draft,
            Published,
            Deleted
        }
    }
}
