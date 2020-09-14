using System;

namespace KnowledgeConsole.Domain
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException()
        {
        }

        public InvalidEntityStateException(object entity, string message)
            : base($"Entity {entity.GetType().Name} state change rejected, {message}")
        {
        }
    }
}
