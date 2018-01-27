using Assets.Controller.Map.Tile;
using Assets.Data.Map.Landmark.Table;
using Assets.Template.Util;

namespace Assets.Model.Map.Combat.Landmark.Builder
{
    public class FlatHillBuilder : ALandmarkBuilder
    {
        public override void BuildLandmark(CTile tile)
        {
            var lmParams = LandmarkTable.Instance.Table[ELandmark.Flat_Hill];
            int radius = RNG.Instance.Next(lmParams.RadiusMin, lmParams.RadiusMax);
            var mTiles = tile.Model.GetAoETiles(radius - 1);
            foreach(var mTile in mTiles)
            {
                if (!FTile.HasFlag(mTile.Controller.GetFlags().CurFlags, FTile.Flags.Landmark))
                {
                    mTile.SetHeight(lmParams.Height);
                    var controller = mTile.Controller;
                    FTile.SetLandmarkFlagTrue(tile.GetFlags());
                }
            }
        }
    }
}
