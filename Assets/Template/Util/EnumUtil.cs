using System;

namespace Assets.Template.Util
{
    public class EnumUtil<T>
    {
        public static bool TryGetEnumValue(string s, ref T t)
        {
            foreach(T e in Enum.GetValues(typeof(T)))
            {
                if (s.ToUpperInvariant().Equals(e.ToString().ToUpperInvariant()))
                {
                    t = e;
                    return true;
                }
            }
            return false;
        }
    }
}
