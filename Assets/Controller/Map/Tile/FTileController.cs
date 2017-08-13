using System;

namespace Assets.Controller.Map.Tile
{
    public class FTileController
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            AwaitingAction = 1,
        }

        public Flags CurFlags { get; set; }

        public FTileController()
        {
            this.CurFlags = new Flags();
        }

        public static void SetAllFlagsFalse(FTileController f)
        {
            foreach (var flag in Enum.GetValues(typeof(Flags)))
                f.CurFlags &= ~(Flags)flag;
        }

        public static void SetAwaitingActionFlagFalse(FTileController f) { f.CurFlags &= ~Flags.AwaitingAction; }
        public static void SetAwaitingActionFlagTrue(FTileController f) { f.CurFlags |= Flags.AwaitingAction; }

        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
    }
}
