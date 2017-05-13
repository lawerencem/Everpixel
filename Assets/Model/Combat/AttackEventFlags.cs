using System;

namespace Model.Combat
{
    public class AttackEventFlags
    {
        [Flags]
        public enum Flags
        {
            Dodge = 0,
        }

        public Flags CurFlags { get; set; }

        public AttackEventFlags()
        {
            this.CurFlags = new Flags();
        }

        public static void SetDodgeFalse(AttackEventFlags f) { f.CurFlags &= ~Flags.Dodge; }

        public static void SetDodgeTrue(AttackEventFlags f) { f.CurFlags |= Flags.Dodge; }

        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
    }

    

    
}
