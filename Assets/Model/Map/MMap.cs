using Assets.Controller.Map.Tile;
using Assets.Model.Party.Enum;
using Assets.Template.Hex;
using Assets.Template.Other;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Model.Map
{
    public class MMap
    {
        private HexMap _map;
        private Dictionary<Pair<int, int>, TileController> _tileDict;
        private List<TileController> _tiles;

        public Dictionary<Pair<int, int>, TileController> GetTileDict() { return this._tileDict; }
        public List<TileController> GetTiles() { return this._tiles; }

        public MMap(HexMap map)
        {
            this._tileDict = new Dictionary<Pair<int, int>, TileController>();
            this._tiles = new List<TileController>();
            this._map = map;
            var mTiles = new List<MTile>();
            foreach(var t in this._map.Tiles)
            {
                var tile = new MTile(t);
                tile.SetMap(this);
                mTiles.Add(tile);
            }
            foreach (var tile in mTiles)
            {
                tile.Init();
                var controller = new TileController(tile);
                this._tiles.Add(controller);
                this._tileDict.Add(new Pair<int, int>(tile.Col, tile.Row), controller);
            }
        }

        public void InitControllerAdjacent()
        {
            foreach (var tile in this._tiles)
                foreach (var neighbor in tile.GetAdjacent())
                    tile.GetAdjacent().Add(neighbor);
        }

        public Path GetPath(MTile s, MTile g)
        {
            int nodeCtr = 0;
            var validPaths = new List<Path>();
            bool found = false;
            var pathDict = new Dictionary<Pair<int, int>, Path>();
            var openSet = new List<MTile>() { s };
            var closedSet = new List<Pair<int, int>>();
            var initPath = new Path();
            initPath.AddTile(s);
            pathDict.Add(new Pair<int, int>(s.Col, s.Row), initPath);

            // TODO: Update this with directional search after so many nodes....
            while (openSet.Count > 0 && !found)
            {
                var tile = openSet.ElementAt(0);
                foreach (var neighbor in tile.GetAdjacent())
                {
                    var neighborKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
                    if (neighbor.Controller.Current == null)
                    {
                        nodeCtr++;
                        if (!closedSet.Contains(neighborKey))
                            openSet.Add(neighbor);
                        var innerKey = new Pair<int, int>(tile.Col, tile.Row);
                        var previousPath = pathDict[innerKey];
                        var newPath = previousPath.DeepCopy();
                        newPath.AddTile(neighbor);
                        var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
                        if (!pathDict.ContainsKey(newKey))
                            pathDict.Add(newKey, newPath);
                        else
                        {
                            if (newPath.Score < pathDict[newKey].Score)
                                pathDict[newKey] = newPath;
                        }

                        if (neighbor == g)
                        {
                            validPaths.Add(newPath);
                            found = true;
                        }

                    }
                    else
                        closedSet.Add(neighborKey);
                }

                closedSet.Add(new Pair<int, int>(tile.Col, tile.Row));
                openSet.Remove(tile);
            }

            if (validPaths.Count > 0)
            {
                var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
                return bestPath;
            }
            else
                return initPath;
        }

        public TileController GetTileForRow(bool lParty, EStartCol col)
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
