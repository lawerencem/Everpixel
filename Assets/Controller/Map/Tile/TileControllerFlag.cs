using System;

namespace Controller.Map
{
    public class TileControllerFlags
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            PotentialAttack = 1,
        }

        public Flags CurFlags { get; set; }

        public TileControllerFlags()
        {
            this.CurFlags = new Flags();
        }

        public static void SetAllFlagsFalse(TileControllerFlags f)
        {
            foreach(var flag in Enum.GetValues(typeof(Flags)))
                f.CurFlags &= ~(Flags)flag;
        }

        public static void SetPotentialAttackFlagFalse(TileControllerFlags f) { f.CurFlags &= ~Flags.PotentialAttack; }


        public static void SetPotentialAttackFlagTrue(TileControllerFlags f) { f.CurFlags |= Flags.PotentialAttack; }

        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
    }
}