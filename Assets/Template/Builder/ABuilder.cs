using System.Collections.Generic;

namespace Assets.Template.Builder
{
    abstract public class ABuilder<T, U>
    {
        public abstract U Build();
        public abstract U Build(T arg);
        public abstract U Build(List<T> args);
    }
}