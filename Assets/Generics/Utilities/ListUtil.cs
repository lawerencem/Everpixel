using System;
using System.Collections.Generic;

namespace Generics.Utilities
{
    public class ListUtil<T>
    {
        public static T GetRandomListElement(List<T> toGet)
        {
            int index = RNG.Instance.Next(toGet.Count);
            return toGet[index];
        }
    }
}
