using Assets.Generics;
using Controller.Characters;
using Controller.Map;
using Generics.Hex;
using Model.Events.Combat;
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
            this._map = map;
            foreach(var tile in this.TileControllers)
            {
                var key = new Pair<int, int>(tile.Model.Col, tile.Model.Row);
                this.TileControllerMap.Add(key, tile);
            }
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
    }
}