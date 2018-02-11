﻿using Assets.Controller.Map.Tile;
using Assets.Data.Map.Landmark.Table;
using Assets.Model.Map.Tile;
using Assets.Template.Util;
using System.Collections.Generic;

namespace Assets.Model.Map.Landmark.Builder
{
    public class PondBuilder : ALandmarkBuilder
    {
        public override void BuildLandmark(CTile tile)
        {
            var lmParams = LandmarkTable.Instance.Table[ELandmark.Pond];
            int radius = RNG.Instance.Next(lmParams.RadiusMin, lmParams.RadiusMax);
            var mTiles = tile.Model.GetAoETiles(radius - 1);
            int minHeight = 3;

            var set = new List<MTile>();
            foreach(var mTile in mTiles)
            {
                if (!set.Contains(mTile))
                    set.Add(mTile);
                foreach (MTile neighbor in mTile.GetAdjacent())
                    if (!set.Contains(neighbor))
                        set.Add(neighbor);
            }

            foreach (var mTile in set)
                if (mTile.Height < minHeight)
                    minHeight = mTile.Height;

            foreach (var mTile in set)
            {
                mTile.SetHeight(minHeight);
                FTile.SetLandmarkFlagTrue(mTile.GetFlags());
            }

            foreach (var mTile in mTiles)
            {
                var controller = mTile.Controller;
                FTile.SetEnvironmentFlagTrue(mTile.GetFlags());
                mTile.SetLiquid(true);
                mTile.SetType(ETile.Water);
            }
        }
    }
}