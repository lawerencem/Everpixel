using System;

namespace Assets.Model.Character.Enum
{
    public class FActionStatus
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            Spearwalling = 1,
        }

        public Flags CurFlags { get; set; }

        public FActionStatus()
        {
            this.CurFlags = new Flags();
        }

        public static void SetSpearwallingFalse(FActionStatus f) { f.CurFlags &= ~Flags.Spearwalling; }

        public static void SetSpearwallingTrue(FActionStatus f) { f.CurFlags |= Flags.Spearwalling; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }
}
