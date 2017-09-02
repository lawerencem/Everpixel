using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Template.Factory
{
    public class GFactory<T, U> : ASingleton<GFactory<T, U>>
    {
        public GFactory() { }

        public virtual U CreateNewObject(List<T> arg) { return default(U); }
        public virtual U CreateNewObject(T arg) { return default(U); }
    }
}
