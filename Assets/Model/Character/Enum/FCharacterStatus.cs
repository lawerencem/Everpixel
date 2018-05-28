using System;

namespace Assets.Model.Character.Enum
{
    public class FCharacterStatus
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            Dead = 1,
            Shapeshifted = 2,
            Undead = 4,
            Vampire = 8,
            Demon = 16,
            Eldritch = 32,
            Lycanthrope = 64,
        }

        public Flags CurFlags { get; set; }

        public FCharacterStatus()
        {
            this.CurFlags = new Flags();
        }

        public static void SetDeadFalse(FCharacterStatus f) { f.CurFlags &= ~Flags.Dead; }
        public static void SetShapeshiftedFalse(FCharacterStatus f) { f.CurFlags &= ~Flags.Shapeshifted; }

        public static void SetDeadTrue(FCharacterStatus f) { f.CurFlags |= Flags.Dead; }
        public static void SetShapeshiftedTrue(FCharacterStatus f) { f.CurFlags &= ~Flags.Shapeshifted; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }
}
