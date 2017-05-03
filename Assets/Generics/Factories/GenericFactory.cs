using System;
using System.Collections.Generic;

namespace Generics
{
    public class GenericFactory<T, U> : AbstractSingleton<GenericFactory<T, U>>
    {
        public GenericFactory() { }

        public virtual U CreateNewObject(List<T> arg) { return default(U); }
        public virtual U CreateNewObject(T arg) { return default(U); }
    }
}
