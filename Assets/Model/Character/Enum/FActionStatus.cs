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
            ShieldWalling = 2,
        }

        public Flags CurFlags { get; set; }

        public FActionStatus()
        {
            this.CurFlags = new Flags();
        }

        public static void SetShieldWallingFalse(FActionStatus f) { f.CurFlags &= ~Flags.ShieldWalling; }
        public static void SetSpearWallingFalse(FActionStatus f) { f.CurFlags &= ~Flags.Spearwalling; }

        public static void SetShieldWallingTrue(FActionStatus f) { f.CurFlags |= Flags.ShieldWalling; }
        public static void SetSpearWallingTrue(FActionStatus f) { f.CurFlags |= Flags.Spearwalling; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }
}
