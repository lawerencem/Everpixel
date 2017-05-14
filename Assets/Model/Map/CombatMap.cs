using Assets.Generics;
using Controller.Characters;
using Controller.Map;
using Generics.Hex;
using Model.Events.Combat;
using Model.Parties;
using System.Collections.Generic;
using System.Linq;

namespace Model.Map
{
    public class CombatMap
    {
        private GenericHexMap _map;
        public List<TileController> TileControllers { get; set; }
        private Dictionary<Pair<int, int>, TileController> TileControllerMap { get; set; }

        public CombatMap(GenericHexMap map)
        {
            this.TileControllers = new List<TileController>();
            this.TileControllerMap = new Dictionary<Pair<int, int>, TileController>();
            this._map = map;
        }

        public void AddTileController(TileController tile)
        {
            this.TileControllers.Add(tile);
            var key = new Pair<int, int>(tile.Model.Col, tile.Model.Row);
            this.TileControllerMap.Add(key, tile);
        }

        public void InitControllerAdjacent()
        {
            foreach(var genericTile in this.TileControllers)
                foreach(var neighbor in genericTile.Model.Adjacent)
                    genericTile.Adjacent.Add(neighbor.Parent);
        }

        public Path GetPath(HexTile s, HexTile g)
        {
            var itr = 0;
            var validPaths = new List<Path>();
            bool found = false;
            var pathDict = new Dictionary<Pair<int, int>, List<Path>>();
            var key = new Pair<int, int>(s.Col, s.Row);
            var openSet = new List<HexTile>() { s };
            var closedSet = new List<HexTile>();
            var initPath = new Path();
            pathDict.Add(key, new List<Path> { initPath });
            initPath.Tiles.Add(s);

            while (openSet.Count > 0 && !found && itr < 20)
            {
                var tile = openSet.ElementAt(0);
                foreach (var neighbor in tile.Adjacent)
                {
                    if (neighbor.Current == null)
                    {
                        var exists = closedSet.Find(x => (x.Col == neighbor.Col && x.Row == neighbor.Row));
                        if (exists == null)
                            openSet.Add(neighbor);
                        var innerKey = new Pair<int, int>(tile.Col, tile.Row);
                        var paths = pathDict[innerKey];
                        foreach(var path in paths)
                        {
                            var newPath = path.DeepCopy();
                            newPath.AddTile(neighbor);
                            var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
                            if (!pathDict.ContainsKey(newKey))
                                pathDict.Add(newKey, new List<Path> { newPath });
                            else
                                pathDict[newKey].Add(newPath);

                            if (neighbor == g)
                            {
                                validPaths.Add(newPath);
                                found = true;
                            }
                        }
                    }
                    else
                        closedSet.Add(neighbor);
                }

                closedSet.Add(tile);
                openSet.Remove(tile);
                itr++;
            }

            if (validPaths.Count > 0)
            {
                var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
                return bestPath;
            }
            else
                return initPath;
        }

        public TileController GetTileForRow(bool enemyParty, StartingColEnum col)
        {
            int rowInd = -1;
            int colInd = -1;
            if (enemyParty)
            {
                if (col == StartingColEnum.Three)
                    colInd = this._map.GetLastCol() - 1;
                else if (col == StartingColEnum.Two)
                    colInd = this._map.GetLastCol() - 2;
                else
                    colInd = this._map.GetLastCol() - 3;

            }
            else
            {
                if (col == StartingColEnum.Three)
                    colInd = this._map.GetFirstCol();
                else if (col == StartingColEnum.Two)
                    colInd = this._map.GetFirstCol() + 1;
                else
                    colInd = this._map.GetFirstCol() + 2;
            }
            
            rowInd = this._map.GetMidRow();
            var key = new Pair<int, int>(colInd, rowInd);
            for (int i = 0; !this.TileControllerMap.ContainsKey(key) || this.TileControllerMap[key].Model.Current != null; i++)
            {
                int counter = i / 2;
                if (i % 2 == 1) { counter *= -1; }
                if (rowInd + counter > this._map.GetLastCol() - 1)
                {
                    i = 0;
                    if (enemyParty) { colInd--; }
                    else { colInd++; }
                }
                else if (rowInd + counter < 0)
                {
                    i = 0;
                    if (enemyParty) { colInd--; }
                    else { colInd++; }
                }
                key = new Pair<int, int>(colInd, rowInd + counter);
            }
            return this.TileControllerMap[key];
        }
    }
}