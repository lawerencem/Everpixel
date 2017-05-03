using System.Collections.Generic;

namespace Generics
{
    abstract public class AbstractBuilder<T, U>
    {
        public abstract U Build();
        public abstract U Build(T arg);
        public abstract U Build(List<T> args);
    }
}