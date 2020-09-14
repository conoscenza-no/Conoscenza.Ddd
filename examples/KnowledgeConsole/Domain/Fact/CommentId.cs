using System;
using Conoscenza.Ddd.Domain;

namespace KnowledgeConsole.Domain.Fact
{
    public class CommentId : ValueObject<CommentId>
    {
        public Guid Value { get; }

        public CommentId(Guid value)
        {
            if (value == default)
                throw new ArgumentException($"{nameof(CommentId)} cannot be empty", nameof(value));

            Value = value;
        }

        public static implicit operator Guid(CommentId self) => self.Value;

        public static implicit operator CommentId(string value)
            => new CommentId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
