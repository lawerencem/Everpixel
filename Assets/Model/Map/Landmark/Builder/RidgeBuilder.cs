using Assets.Controller.Map.Tile;
using Assets.Data.Map.Landmark.Table;
using Assets.Model.Map.Tile;
using Assets.Template.Hex;
using Assets.Template.Util;

namespace Assets.Model.Map.Landmark.Builder
{
    public class RidgeBuilder : ALandmarkBuilder
    {
        public override void BuildLandmark(CTile tile)
        {
            var lmParams = LandmarkTable.Instance.Table[ELandmark.Ridge];
            int length = RNG.Instance.Next(lmParams.LengthMin, lmParams.LengthMin);
            var builder = new HexGeography();
            var direction = EDirectionUtil.GetRandomDirection();
            var tiles = builder.GetTiles(direction, length, tile.Model);

            foreach (MTile mTile in tiles)
            {
                if (mTile != null)
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
}
