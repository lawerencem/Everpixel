using System;
using System.Collections.Generic;

namespace Generics
{
    public class GenericBuilder<T, U> : AbstractBuilder<T, U>
    {
        public override U Build()
        {
            throw new NotImplementedException();
        }

        public override U Build(List<T> args)
        {
            throw new NotImplementedException();
        }

        public override U Build(T arg)
        {
            throw new NotImplementedException();
        }
    }
}