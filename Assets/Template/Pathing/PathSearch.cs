using Assets.Template.Hex;
using Assets.Template.Other;
using Assets.Template.Util;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Template.Pathing
{
    internal class GetPathDataClass
    {
        public List<Pair<int, int>> ClosedSet { get; set; }
        public Path InitPath { get; set; }
        public List<IHex> OpenSet { get; set; }
        public List<Path> PathsToGoal { get; set; }
        public Dictionary<Pair<int, int>, Path> PathsToGoalDict { get; set; }
    }

    public class PathSearch
    {
        public Path GetBruteForcePathViaFiniteSet(List<IHex> set, IHex s, IHex g)
        {
            var validPaths = new List<Path>();
            var openSet = new List<IHex>() { s };
            foreach (var tile in set)
                if (!tile.Equals(s))
                    openSet.Add(tile);
            bool found = false;
            var pathDict = new Dictionary<Pair<int, int>, Path>();
            var closedSet = new List<Pair<int, int>>();
            var initPath = new Path();
            initPath.AddTile(s);
            pathDict.Add(new Pair<int, int>(s.GetCol(), s.GetRow()), initPath);

            while (openSet.Count > 0 && !found)
            {
                var tile = openSet.ElementAt(0);
                foreach (var neighbor in tile.GetAdjacent())
                {
                    var neighborKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
                    if (neighbor.GetCurrentOccupant() == null)
                    {
                        var innerKey = new Pair<int, int>(tile.GetCol(), tile.GetRow());
                        var previousPath = pathDict[innerKey];
                        var newPath = previousPath.DeepCopy();
                        newPath.AddTile(neighbor);
                        var newKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
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
                closedSet.Add(new Pair<int, int>(tile.GetCol(), tile.GetRow()));
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

        public Path GetBruteForcePathUnknownSet(IHex s, IHex g)
        {
            var validPaths = new List<Path>();
            bool found = false;
            var pathDict = new Dictionary<Pair<int, int>, Path>();
            var openSet = new List<IHex>() { s };
            var closedSet = new List<Pair<int, int>>();
            var initPath = new Path();
            initPath.AddTile(s);
            pathDict.Add(new Pair<int, int>(s.GetCol(), s.GetRow()), initPath);

            while (openSet.Count > 0 && !found)
            {
                var tile = openSet.ElementAt(0);
                foreach (var neighbor in tile.GetAdjacent())
                {
                    var neighborKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
                    if (neighbor.GetCurrentOccupant() == null)
                    {
                        if (!closedSet.Contains(neighborKey))
                            openSet.Add(neighbor);
                        var innerKey = new Pair<int, int>(tile.GetCol(), tile.GetRow());
                        var previousPath = pathDict[innerKey];
                        var newPath = previousPath.DeepCopy();
                        newPath.AddTile(neighbor);
                        var newKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
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
                closedSet.Add(new Pair<int, int>(tile.GetCol(), tile.GetRow()));
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

        private GetPathDataClass GetPathHelperData(IHex t)
        {
            var data = new GetPathDataClass();
            data.OpenSet = new List<IHex>() { t };
            data.ClosedSet = new List<Pair<int, int>>();
            data.InitPath = new Path();
            data.PathsToGoal = new List<Path>();
            data.PathsToGoalDict = new Dictionary<Pair<int, int>, Path>();
            data.PathsToGoalDict.Add(new Pair<int, int>(t.GetCol(), t.GetRow()), data.InitPath);
            return data;
        }

        private void GetPathHelperIterateData(GetPathDataClass data)
        {
            var tile = data.OpenSet.ElementAt(0);
            foreach (var neighbor in tile.GetAdjacent())
            {
                var neighborKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
                if (neighbor.GetCurrentOccupant() == null)
                {
                    if (!data.ClosedSet.Contains(neighborKey))
                        data.OpenSet.Add(neighbor);
                    var pathKey = new Pair<int, int>(tile.GetCol(), tile.GetRow());
                    var previousPath = data.PathsToGoalDict[pathKey];
                    var newPath = previousPath.DeepCopy();
                    newPath.AddTile(neighbor);
                    var newKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
                    if (!data.PathsToGoalDict.ContainsKey(newKey))
                        data.PathsToGoalDict.Add(newKey, newPath);
                    else
                    {
                        if (newPath.Score < data.PathsToGoalDict[newKey].Score)
                            data.PathsToGoalDict[newKey] = newPath;
                    }
                    data.PathsToGoal.Add(newPath);
                }
                else
                    data.ClosedSet.Add(neighborKey);
            }
            data.ClosedSet.Add(new Pair<int, int>(tile.GetCol(), tile.GetRow()));
            data.OpenSet.Remove(tile);
        }

        public Path GetPath(IHex s, IHex g)
        {
            var quickPath = this.GetPathViaSourceAdjacentToGoal(s, g);
            if (quickPath != null)
                return quickPath;

            var goalData = this.GetPathHelperData(g);
            var sourceData = this.GetPathHelperData(s);
            var validPaths = new List<Path>();

            while (sourceData.OpenSet.Count > 0 && goalData.OpenSet.Count > 0)
            {
                this.GetPathHelperIterateData(sourceData);
                this.GetPathHelperIterateData(goalData);
                if (sourceData.PathsToGoal.Count > 0 && goalData.PathsToGoal.Count > 0)
                {
                    foreach (var sourcePath in sourceData.PathsToGoal)
                    {
                        foreach (var goalPath in goalData.PathsToGoal)
                        {
                            if (sourcePath.GetLastTile().Equals(goalPath.GetLastTile()))
                            {
                                var valid = new Path();
                                foreach (var tile in sourcePath.GetTiles())
                                    valid.AddTile(tile);
                                goalPath.GetTiles().Reverse();
                                for (int i = 1; i < goalPath.GetTiles().Count; i++)
                                    valid.AddTile(goalPath.GetTiles()[i]);
                                valid.AddTile(g);
                                validPaths.Add(valid);
                            }
                        }
                    }
                }
                if (validPaths.Count > 0)
                {
                    var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
                    return this.TryOptimizePath(bestPath, s, g);
                }
            }
            return null;
        }

        private Path GetPathViaSourceAdjacentToGoal(IHex s, IHex g)
        {
            foreach (var neighbor in s.GetAdjacent())
            {
                if (neighbor.Equals(g))
                {
                    var trimmed = new Path();
                    trimmed.AddTile(g);
                    return trimmed;
                }
            }
            return null;
        }

        private Path TrimRedundantPathing(Path path, IHex g)
        {
            var validPaths = new List<Path>();
            var openSet = path.GetTiles();
            var initPath = new Path();
            var pathDict = new Dictionary<Pair<int, int>, Path>();
            var firstTile = path.GetTiles()[0];
            initPath.AddTile(firstTile);
            pathDict.Add(new Pair<int, int>(firstTile.GetCol(), firstTile.GetRow()), initPath);
            while (openSet.Count > 0)
            {
                var tile = openSet.ElementAt(0);
                foreach (var neighbor in tile.GetAdjacent())
                {
                    if (openSet.Contains(neighbor))
                    {
                        var pathKey = new Pair<int, int>(tile.GetCol(), tile.GetRow());
                        var previousPath = pathDict[pathKey];
                        var newPath = previousPath.DeepCopy();
                        newPath.AddTile(neighbor);
                        var newKey = new Pair<int, int>(neighbor.GetCol(), neighbor.GetRow());
                        if (!pathDict.ContainsKey(newKey))
                            pathDict.Add(newKey, newPath);
                        else
                        {
                            if (newPath.Score < pathDict[newKey].Score)
                                pathDict[newKey] = newPath;
                        }

                        if (neighbor == g)
                            validPaths.Add(newPath);
                    }
                }
                openSet.Remove(tile);
            }
            var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
            return bestPath;
        }

        private Path TryOptimizePath(Path p, IHex s, IHex g)
        {
            var extendedSet = ListUtil<IHex>.ShallowClone(p.GetTiles());
            foreach (var tile in p.GetTiles())
            {
                foreach (var neighbor in tile.GetAdjacent())
                {
                    if (!extendedSet.Contains(neighbor))
                        extendedSet.Add(neighbor);
                }
            }
            return this.GetBruteForcePathViaFiniteSet(extendedSet, s, g);
        }
    }
}
