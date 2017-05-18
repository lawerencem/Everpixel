using Assets.Generics;
using Controller.Map;
using Generics.Hex;
using Generics.Utilities;
using Model.Map;
using System.Collections.Generic;

namespace Model.Map
{
    public class Path
    {
        private int _score;
        public int Score { get { return this._score; } }
        public List<HexTile> Tiles { get; set; }

        public Path(ColRowPairPath intPath, GenericHexMap map)
        {
            this._score = 0;
            this.Tiles = new List<HexTile>();
            foreach(var colRowPair in intPath.Path)
            {
                var tile = map.GetTileViaColRowPair(colRowPair.X, colRowPair.Y);
                this._score += tile.Cost;
                this.Tiles.Add(tile);
            }
        }

        public TileController GetNextTile(TileController t)
        {
            if (this.Tiles.Contains(t.Model))
            {
                int index = this.Tiles.IndexOf(t.Model) + 1;
                if (this.Tiles.Count > index)
                    return this.Tiles[index].Parent;
            }
            return null;
        }
    }
}
