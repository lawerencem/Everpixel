using System;

namespace Model.Combat
{
    public class AttackEventFlags
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
        }

        public Flags CurFlags { get; set; }

        public AttackEventFlags()
        {
            this.CurFlags = new Flags();
        }

        public static void SetDodgeFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Dodge; }
        public static void SetParryFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Parry; }
        public static void SetBlockFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Block; }
        public static void SetCritFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Critical; }
        public static void SetHeadFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Head; }
        public static void SetSummonFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Summon; }
        public static void SetFatalityFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Fatality; }

        public static void SetDodgeTrue(AttackEventFlags f) { f.CurFlags |= Flags.Dodge; }
        public static void SetParryTrue(AttackEventFlags f) { f.CurFlags |= Flags.Parry; }
        public static void SetBlockTrue(AttackEventFlags f) { f.CurFlags |= Flags.Block; }
        public static void SetCritTrue(AttackEventFlags f) { f.CurFlags |= Flags.Critical; }
        public static void SetHeadTrue(AttackEventFlags f) { f.CurFlags |= Flags.Head; }
        public static void SetSummonTrue(AttackEventFlags f) { f.CurFlags |= Flags.Summon; }
        public static void SetFatalityTrue(AttackEventFlags f) { f.CurFlags |= Flags.Fatality; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }    
}
