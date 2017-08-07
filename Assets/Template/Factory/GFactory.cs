using System.Collections.Generic;
using Template.Other;

namespace Template.Factory
{
    public class GFactory<T, U> : ASingleton<GFactory<T, U>>
    {
        public GFactory() { }

        public virtual U CreateNewObject(List<T> arg) { return default(U); }
        public virtual U CreateNewObject(T arg) { return default(U); }
    }
}
