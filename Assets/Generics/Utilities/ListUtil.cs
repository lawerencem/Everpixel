using System;
using System.Collections.Generic;

namespace Generics.Utilities
{
    public class ListUtil<T>
    {
        public static T GetRandomListElement(List<T> toGet)
        {
            var random = new Random();
            int index = random.Next(toGet.Count);
            return toGet[index];
        }
    }
}
