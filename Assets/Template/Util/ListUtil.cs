using System.Collections.Generic;

namespace Assets.Template.Util
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
