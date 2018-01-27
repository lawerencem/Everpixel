using Assets.Controller.Map.Tile;
using Assets.Model.Map.Combat.Tile;
using Assets.Model.Party.Enum;
using Assets.Template.Hex;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Map.Combat
{
    public class MMap
    {
        private HexMap _map;
        private Dictionary<Pair<int, int>, CTile> _tileDict;
        private List<CTile> _tiles;

        public Dictionary<Pair<int, int>, CTile> GetTileDict() { return this._tileDict; }
        public List<CTile> GetTiles() { return this._tiles; }

        public MMap(HexMap map)
        {
            this._tileDict = new Dictionary<Pair<int, int>, CTile>();
            this._tiles = new List<CTile>();
            this._map = map;
            var mTiles = new List<MTile>();
            foreach(MTile tile in this._map.Tiles)
            {
                tile.SetMap(this);
                var controller = new CTile(tile);
                this._tiles.Add(controller);
                this._tileDict.Add(new Pair<int, int>(tile.GetCol(), tile.GetRow()), controller);
            }
        }

        public void InitControllerAdjacent()
        {
            foreach (var tile in this._tiles)
                foreach (var neighbor in tile.GetAdjacent())
                    tile.GetAdjacent().Add(neighbor);
        }

        public CTile GetTileForRow(bool lParty, EStartCol col)
        {
            int rowInd = -1;
            int colInd = -1;
            if (!lParty)
            {
                if (col == EStartCol.Three)
                    colInd = this._map.GetLastCol() - 1;
                else if (col == EStartCol.Two)
                    colInd = this._map.GetLastCol() - 2;
                else
                    colInd = this._map.GetLastCol() - 3;
            }
            else
            {
                if (col == EStartCol.Three)
                    colInd = this._map.GetFirstCol();
                else if (col == EStartCol.Two)
                    colInd = this._map.GetFirstCol() + 1;
                else
                    colInd = this._map.GetFirstCol() + 2;
            }

            rowInd = this._map.GetMidRow();
            var key = new Pair<int, int>(colInd, rowInd);
            for (int i = 0; !this._tileDict.ContainsKey(key) || this._tileDict[key].Current != null; i++)
            {
                int counter = i / 2;
                if (i % 2 == 1) { counter *= -1; }
                if (rowInd + counter > this._map.GetLastCol() - 1)
                {
                    i = 0;
                    if (lParty) { colInd--; }
                    else { colInd++; }
                }
                else if (rowInd + counter < 0)
                {
                    i = 0;
                    if (lParty) { colInd--; }
                    else { colInd++; }
                }
                key = new Pair<int, int>(colInd, rowInd + counter);
            }
            return this._tileDict[key];
        }

        
    }
}
