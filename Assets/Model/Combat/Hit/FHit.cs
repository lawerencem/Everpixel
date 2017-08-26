using System;

namespace Assets.Model.Combat.Hit
{
    public class FHit
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            Dodge = 1,
            Parry = 2,
            Block = 4,
            Critical = 8,
            Head = 16,
            Summon = 32,
            Fatality = 64,
            Shapeshift = 128,
            Resist = 256,
        }

        public Flags CurFlags { get; set; }

        public FHit()
        {
            this.CurFlags = new Flags();
        }

        public static void SetDodgeFalse(FHit f) { f.CurFlags &= ~Flags.Dodge; }
        public static void SetParryFalse(FHit f) { f.CurFlags &= ~Flags.Parry; }
        public static void SetBlockFalse(FHit f) { f.CurFlags &= ~Flags.Block; }
        public static void SetCritFalse(FHit f) { f.CurFlags &= ~Flags.Critical; }
        public static void SetHeadFalse(FHit f) { f.CurFlags &= ~Flags.Head; }
        public static void SetShapeshiftFalse(FHit f) { f.CurFlags &= ~Flags.Shapeshift; }
        public static void SetSummonFalse(FHit f) { f.CurFlags &= ~Flags.Summon; }
        public static void SetFatalityFalse(FHit f) { f.CurFlags &= ~Flags.Fatality; }
        public static void SetResistFalse(FHit f) { f.CurFlags &= ~Flags.Resist; }

        public static void SetDodgeTrue(FHit f) { f.CurFlags |= Flags.Dodge; }
        public static void SetParryTrue(FHit f) { f.CurFlags |= Flags.Parry; }
        public static void SetBlockTrue(FHit f) { f.CurFlags |= Flags.Block; }
        public static void SetCritTrue(FHit f) { f.CurFlags |= Flags.Critical; }
        public static void SetHeadTrue(FHit f) { f.CurFlags |= Flags.Head; }
        public static void SetShapeshiftTrue(FHit f) { f.CurFlags |= Flags.Shapeshift; }
        public static void SetSummonTrue(FHit f) { f.CurFlags |= Flags.Summon; }
        public static void SetFatalityTrue(FHit f) { f.CurFlags |= Flags.Fatality; }
        public static void SetResistTrue(FHit f) { f.CurFlags |= Flags.Resist; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }    
}
