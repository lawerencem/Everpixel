using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Template.Util
{
    public class ListUtil<T>
    {

        public static T GetRandomElement(List<T> toGet)
        {
            if (toGet.Count > 0)
            {
                int index = RNG.Instance.Next(toGet.Count);
                return toGet[index];
            }
            else
                return default(T);
        }

        public static List<T> ShallowClone(List<T> oldList)
        {
            var clone = new List<T>();
            foreach (var t in oldList)
                clone.Add(t);
            return clone;
        }
    }
}
