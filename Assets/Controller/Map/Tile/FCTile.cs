using System;

namespace Assets.Controller.Map.Tile
{
    public class FCTile
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            AwaitingAction = 1,
        }

        public Flags CurFlags { get; set; }

        public FCTile()
        {
            this.CurFlags = new Flags();
        }

        public static void SetAllFlagsFalse(FCTile f)
        {
            foreach (var flag in Enum.GetValues(typeof(Flags)))
                f.CurFlags &= ~(Flags)flag;
        }

        public static void SetAwaitingActionFlagFalse(FCTile f) { f.CurFlags &= ~Flags.AwaitingAction; }
        public static void SetAwaitingActionFlagTrue(FCTile f) { f.CurFlags |= Flags.AwaitingAction; }

        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
    }
}
