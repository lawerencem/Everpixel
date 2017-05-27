using System;

namespace Model.Combat
{
    public class AttackEventFlags
    {
        [Flags]
        public enum Flags
        {
            Dodge = 0,
            Parry = 1,
            Block = 2,
            Critical = 4,
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

        public static void SetDodgeTrue(AttackEventFlags f) { f.CurFlags |= Flags.Dodge; }
        public static void SetParryTrue(AttackEventFlags f) { f.CurFlags |= Flags.Parry; }
        public static void SetBlockTrue(AttackEventFlags f) { f.CurFlags |= Flags.Block; }
        public static void SetCritTrue(AttackEventFlags f) { f.CurFlags |= Flags.Critical; }

        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
    }    
}
