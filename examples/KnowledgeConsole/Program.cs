using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KnowledgeConsole.Domain.Fact;

namespace KnowledgeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var fact = new Fact(new FactId(Guid.NewGuid()), "Hello world");
            fact.UpdateTitle("Hello world 2");
            fact.UpdateBody("This is the body if Hello World");
            fact.Publish();
            fact.Upvote();
            fact.Comment("Nice article");
            fact.Downvote();
            fact.Comment("Not so good");
            fact.Delete();

            PrintToConsole(fact);
        }

        private static void PrintToConsole(Fact fact)
        {
            Console.WriteLine("*** Current value ***");
            foreach (var propertyInfo in fact.GetType().GetProperties())
            {
                Console.WriteLine($"{propertyInfo.Name} = {propertyInfo.GetValue(fact)}");

                var typeinfo = (TypeInfo)propertyInfo.PropertyType;

                if (propertyInfo.PropertyType == typeof(string) ||
                    !typeinfo.ImplementedInterfaces.Contains(typeof(IEnumerable))) continue;

                var list = (IEnumerable)propertyInfo.GetValue(fact);
                if (list == null) continue;

                foreach (var item in list)
                {
                    foreach (var pi in item.GetType().GetProperties())
                    {
                        Console.WriteLine($"{pi.Name} = {pi.GetValue(item)}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("*** History ***");
            foreach (var change in fact.GetChanges())
            {
                Console.WriteLine(change.ToString());
            }
        }
    }
}
