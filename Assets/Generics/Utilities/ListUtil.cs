using System;
using System.Collections.Generic;

namespace Generics.Utilities
{
    public class ListUtil<T>
    {
        public static T GetRandomListElement(List<T> toGet)
        {
            if (toGet.Count > 0)
            {
                int index = RNG.Instance.Next(toGet.Count);
                return toGet[index];
            }
            else
                return default(T);
            
        }
    }
}
