using System;

namespace Model.Characters
{
    public class CharacterStatusFlags
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            Casting = 1,
            Dead = 2,
            Shapeshifted = 4,
        }

        public Flags CurFlags { get; set; }

        public CharacterStatusFlags()
        {
            this.CurFlags = new Flags();
        }

        public static void SetCastingFalse(CharacterStatusFlags f) { f.CurFlags &= ~Flags.Casting; }
        public static void SetDeadFalse(CharacterStatusFlags f) { f.CurFlags &= ~Flags.Dead; }
        public static void SetShapeshiftedFalse(CharacterStatusFlags f) { f.CurFlags &= ~Flags.Shapeshifted; }

        public static void SetCastingTrue(CharacterStatusFlags f) { f.CurFlags |= Flags.Casting; }
        public static void SetDeadTrue(CharacterStatusFlags f) { f.CurFlags |= Flags.Dead; }
        public static void SetShapeshiftedTrue(CharacterStatusFlags f) { f.CurFlags &= ~Flags.Shapeshifted; }

        public static bool HasFlag(Flags a, Flags b)
        {
            return (a & b) == b;
        }
    }
}
