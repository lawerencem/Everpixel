using Assets.Model.Map.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Map.Deco.Table
{
    public class DecoTable : ASingleton<DecoTable>
    {
        public Dictionary<ETileDeco, TileDecoParam> Table;
        public DecoTable()
        {
            this.Table = new Dictionary<ETileDeco, TileDecoParam>();
        }
    }
}
