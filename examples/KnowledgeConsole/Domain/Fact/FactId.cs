using System;
using Conoscenza.Ddd.Domain;

namespace KnowledgeConsole.Domain.Fact
{
    public class FactId : ValueObject<FactId>
    {
        public Guid Value { get; }

        public FactId(Guid value)
        {
            if (value == default)
                throw new ArgumentException($"{nameof(FactId)} cannot be empty", nameof(value));

            Value = value;
        }

        public static implicit operator Guid(FactId self) => self.Value;

        public static implicit operator FactId(string value)
            => new FactId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
