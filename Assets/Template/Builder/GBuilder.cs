using System;
using System.Collections.Generic;

namespace Assets.Template.Builder
{
    public class GBuilder<T, U> : ABuilder<T, U>
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