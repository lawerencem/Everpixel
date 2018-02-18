using System;

namespace Assets.Model.Character.Enum
{
    public class FCharacterStatus
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            Casting = 1,
            Dead = 2,
            Shapeshifted = 4,
            Undead = 8,
            Vampire = 16,
            Demon = 32,
            Eldritch = 64,
            Lycanthrope = 128,
        }

        public Flags CurFlags { get; set; }

        public FCharacterStatus()
        {
            this.CurFlags = new Flags();
        }

        public static void SetCastingFalse(FCharacterStatus f) { f.CurFlags &= ~Flags.Casting; }
        public static void SetDeadFalse(FCharacterStatus f) { f.CurFlags &= ~Flags.Dead; }
        public static void SetShapeshiftedFalse(FCharacterStatus f) { f.CurFlags &= ~Flags.Shapeshifted; }

        public static void SetCastingTrue(FCharacterStatus f) { f.CurFlags |= Flags.Casting; }
        public static void SetDeadTrue(FCharacterStatus f) { f.CurFlags |= Flags.Dead; }
        public static void SetShapeshiftedTrue(FCharacterStatus f) { f.CurFlags &= ~Flags.Shapeshifted; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }
}
