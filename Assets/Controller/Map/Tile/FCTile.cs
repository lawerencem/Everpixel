using System;

namespace Assets.Controller.Map.Tile
{
    public class FTile
    {
        [Flags]
        public enum Flags
        {
            None = 0,
            Deco = 1,
            Landmark = 2,
        }

        public Flags CurFlags { get; set; }

        public FTile()
        {
            this.CurFlags = new Flags();
        }

        public static void SetAllFlagsFalse(FTile f)
        {
            foreach (var flag in Enum.GetValues(typeof(Flags)))
                f.CurFlags &= ~(Flags)flag;
        }

        public static void SetDecoFlagFalse(FTile f) { f.CurFlags &= ~Flags.Deco; }
        public static void SetLandmarkFlagFalse(FTile f) { f.CurFlags &= ~Flags.Landmark; }

        public static void SetDecoFlagTrue(FTile f) { f.CurFlags |= Flags.Deco; }
        public static void SetLandmarkFlagTrue(FTile f) { f.CurFlags |= Flags.Landmark; }

        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
    }
}
