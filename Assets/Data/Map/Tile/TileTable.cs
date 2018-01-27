using Assets.Model.Map.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Map.Landmark.Table
{
    public class TileTable : ASingleton<TileTable>
    {
        public Dictionary<ETile, TileParams> Table;
        public TileTable()
        {
            this.Table = new Dictionary<ETile, TileParams>();
        }
    }
}
