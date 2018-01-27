using Assets.Template.Util;

namespace Assets.Template.Hex
{
    public enum EDirection
    {
        N,
        NE,
        SE,
        S,
        SW,
        NW
    }

    public class EDirectionUtil
    {
        public static EDirection GetRandomDirection()
        {
            int roll = RNG.Instance.Next(0, 5);
            if (roll == 0)
                return EDirection.N;
            else if (roll == 1)
                return EDirection.NE;
            else if (roll == 2)
                return EDirection.SE;
            else if (roll == 3)
                return EDirection.S;
            else if (roll == 4)
                return EDirection.SW;
            else
                return EDirection.NW;
        }
    }
}
