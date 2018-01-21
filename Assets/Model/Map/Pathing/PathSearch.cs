//using Assets.Model.Map.Combat.Tile;
//using Assets.Template.Other;
//using System.Collections.Generic;
//using System.Linq;

//namespace Assets.Model.Map.Pathing
//{
//    public class PathSearch
//    {
//        public Path GetBruteForcePathViaFiniteSet(List<MTile> set, MTile s, MTile g)
//        {
//            var validPaths = new List<Path>();
//            var openSet = new List<MTile>();
//            foreach (var tile in set)
//                openSet.Add(tile);
//            bool found = false;
//            var pathDict = new Dictionary<Pair<int, int>, Path>();
//            var closedSet = new List<Pair<int, int>>();
//            var initPath = new Path();
//            initPath.AddTile(s);
//            pathDict.Add(new Pair<int, int>(s.Col, s.Row), initPath);

//            while (openSet.Count > 0 && !found)
//            {
//                var tile = openSet.ElementAt(0);
//                foreach (var neighbor in tile.GetAdjacent())
//                {
//                    var neighborKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                    if (neighbor.Controller.Current == null)
//                    {
//                        var innerKey = new Pair<int, int>(tile.Col, tile.Row);
//                        var previousPath = pathDict[innerKey];
//                        var newPath = previousPath.DeepCopy();
//                        newPath.AddTile(neighbor);
//                        var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                        if (!pathDict.ContainsKey(newKey))
//                            pathDict.Add(newKey, newPath);
//                        else
//                        {
//                            if (newPath.Score < pathDict[newKey].Score)
//                                pathDict[newKey] = newPath;
//                        }

//                        if (neighbor == g)
//                        {
//                            validPaths.Add(newPath);
//                            found = true;
//                        }

//                    }
//                    else
//                        closedSet.Add(neighborKey);
//                }
//                closedSet.Add(new Pair<int, int>(tile.Col, tile.Row));
//                openSet.Remove(tile);
//            }

//            if (validPaths.Count > 0)
//            {
//                var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
//                return bestPath;
//            }
//            else
//                return initPath;
//        }

//        public Path GetBruteForcePathUnknownSet(MTile s, MTile g)
//        {
//            var validPaths = new List<Path>();
//            bool found = false;
//            var pathDict = new Dictionary<Pair<int, int>, Path>();
//            var openSet = new List<MTile>() { s };
//            var closedSet = new List<Pair<int, int>>();
//            var initPath = new Path();
//            initPath.AddTile(s);
//            pathDict.Add(new Pair<int, int>(s.Col, s.Row), initPath);

//            while (openSet.Count > 0 && !found)
//            {
//                var tile = openSet.ElementAt(0);
//                foreach (var neighbor in tile.GetAdjacent())
//                {
//                    var neighborKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                    if (neighbor.Controller.Current == null)
//                    {
//                        if (!closedSet.Contains(neighborKey))
//                            openSet.Add(neighbor);
//                        var innerKey = new Pair<int, int>(tile.Col, tile.Row);
//                        var previousPath = pathDict[innerKey];
//                        var newPath = previousPath.DeepCopy();
//                        newPath.AddTile(neighbor);
//                        var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                        if (!pathDict.ContainsKey(newKey))
//                            pathDict.Add(newKey, newPath);
//                        else
//                        {
//                            if (newPath.Score < pathDict[newKey].Score)
//                                pathDict[newKey] = newPath;
//                        }

//                        if (neighbor == g)
//                        {
//                            validPaths.Add(newPath);
//                            found = true;
//                        }

//                    }
//                    else
//                        closedSet.Add(neighborKey);
//                }
//                closedSet.Add(new Pair<int, int>(tile.Col, tile.Row));
//                openSet.Remove(tile);
//            }

//            if (validPaths.Count > 0)
//            {
//                var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
//                return bestPath;
//            }
//            else
//                return initPath;
//        }

//        public Path GetPath(MTile s, MTile g)
//        {
//            var quickPath = this.GetPathViaSourceAdjacentToGoal(s, g);
//            if (quickPath != null)
//                return quickPath;

//            var goalOpenSet = new List<MTile>() { g };
//            var goalClosedSet = new List<Pair<int, int>>();
//            var goalInitPath = new Path();
//            var goalPaths = new List<Path>();
//            var goalPathDict = new Dictionary<Pair<int, int>, Path>();
//            goalPathDict.Add(new Pair<int, int>(g.Col, g.Row), goalInitPath);

//            var sourceOpenSet = new List<MTile>() { s };
//            var sourceClosedSet = new List<Pair<int, int>>();
//            var sourceInitPath = new Path();
//            var sourcePaths = new List<Path>();
//            var sourcePathDict = new Dictionary<Pair<int, int>, Path>();
//            sourcePathDict.Add(new Pair<int, int>(s.Col, s.Row), sourceInitPath);

//            var validPaths = new List<Path>();

//            while (sourceOpenSet.Count > 0 && goalOpenSet.Count > 0)
//            {
//                var goalTile = goalOpenSet.ElementAt(0);
//                foreach (var neighbor in goalTile.GetAdjacent())
//                {
//                    var neighborKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                    if (neighbor.Controller.Current == null)
//                    {
//                        if (!goalClosedSet.Contains(neighborKey))
//                            goalOpenSet.Add(neighbor);
//                        var innerKey = new Pair<int, int>(goalTile.Col, goalTile.Row);
//                        var previousPath = goalPathDict[innerKey];
//                        var newPath = previousPath.DeepCopy();
//                        newPath.AddTile(neighbor);
//                        var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                        if (!goalPathDict.ContainsKey(newKey))
//                            goalPathDict.Add(newKey, newPath);
//                        else
//                        {
//                            if (newPath.Score < goalPathDict[newKey].Score)
//                                goalPathDict[newKey] = newPath;
//                        }
//                        goalPaths.Add(newPath);
//                    }
//                    else
//                        goalClosedSet.Add(neighborKey);
//                }
//                goalClosedSet.Add(new Pair<int, int>(goalTile.Col, goalTile.Row));
//                goalOpenSet.Remove(goalTile);

//                var sourceTile = sourceOpenSet.ElementAt(0);

//                foreach (var neighbor in sourceTile.GetAdjacent())
//                {
//                    var neighborKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                    if (neighbor.Controller.Current == null)
//                    {
//                        if (!sourceClosedSet.Contains(neighborKey))
//                            sourceOpenSet.Add(neighbor);
//                        var innerKey = new Pair<int, int>(sourceTile.Col, sourceTile.Row);
//                        var previousPath = sourcePathDict[innerKey];
//                        var newPath = previousPath.DeepCopy();
//                        newPath.AddTile(neighbor);
//                        var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                        if (!sourcePathDict.ContainsKey(newKey))
//                            sourcePathDict.Add(newKey, newPath);
//                        else
//                        {
//                            if (newPath.Score < sourcePathDict[newKey].Score)
//                                sourcePathDict[newKey] = newPath;
//                        }
//                        sourcePaths.Add(newPath);
//                    }
//                    else
//                        sourceClosedSet.Add(neighborKey);
//                }
//                sourceClosedSet.Add(new Pair<int, int>(sourceTile.Col, sourceTile.Row));
//                sourceOpenSet.Remove(sourceTile);

//                if (goalPaths.Count > 0 && sourcePaths.Count > 0)
//                {
//                    foreach (var sourcePath in sourcePaths)
//                    {
//                        foreach (var goalPath in goalPaths)
//                        {
//                            if (sourcePath.GetTiles()[sourcePath.GetTiles().Count - 1].Equals(
//                                goalPath.GetTiles()[goalPath.GetTiles().Count - 1]))
//                            {
//                                var valid = new Path();
//                                foreach (var tile in sourcePath.GetTiles())
//                                    valid.AddTile(tile);
//                                goalPath.GetTiles().Reverse();
//                                for (int i = 1; i < goalPath.GetTiles().Count; i++)
//                                    valid.AddTile(goalPath.GetTiles()[i]);
//                                valid.AddTile(g);
//                                validPaths.Add(valid);
//                            }
//                        }
//                    }
//                    goalPaths.Clear();
//                    sourcePaths.Clear();
//                }
//                if (validPaths.Count > 0)
//                {
//                    var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
//                    return this.TrimRedundantPathing(bestPath, g);
//                }
//            }
//            return null;
//        }

//        private Path GetPathViaSourceAdjacentToGoal(MTile s, MTile g)
//        {
//            foreach (var neighbor in s.GetAdjacent())
//            {
//                if (neighbor.Equals(g))
//                {
//                    var trimmed = new Path();
//                    trimmed.AddTile(g);
//                    return trimmed;
//                }
//            }
//            return null;
//        }

//        private Path TrimRedundantPathing(Path path, MTile g)
//        {
//            var validPaths = new List<Path>();
//            var openSet = path.GetTiles();
//            var initPath = new Path();
//            var pathDict = new Dictionary<Pair<int, int>, Path>();
//            var firstTile = path.GetTiles()[0];
//            initPath.AddTile(firstTile);
//            pathDict.Add(new Pair<int, int>(firstTile.Col, firstTile.Row), initPath);
//            while (openSet.Count > 0)
//            {
//                var tile = openSet.ElementAt(0);
//                foreach (var neighbor in tile.GetAdjacent())
//                {
//                    if (openSet.Contains(neighbor))
//                    {
//                        var pathKey = new Pair<int, int>(tile.Col, tile.Row);
//                        var previousPath = pathDict[pathKey];
//                        var newPath = previousPath.DeepCopy();
//                        newPath.AddTile(neighbor);
//                        var newKey = new Pair<int, int>(neighbor.Col, neighbor.Row);
//                        if (!pathDict.ContainsKey(newKey))
//                            pathDict.Add(newKey, newPath);
//                        else
//                        {
//                            if (newPath.Score < pathDict[newKey].Score)
//                                pathDict[newKey] = newPath;
//                        }

//                        if (neighbor == g)
//                            validPaths.Add(newPath);
//                    }
//                }
//                openSet.Remove(tile);
//            }
//            var bestPath = validPaths.OrderBy(x => x.Score).ToList()[0];
//            return bestPath;
//        }

//        private Path TryOptimizePath(Path p, MTile s, MTile g)
//        {
//            var openSet = p.GetTiles();
//            var extendedSet = p.GetTiles();
//            foreach (var tile in openSet)
//            {
//                foreach (var neighbor in tile.GetAdjacent())
//                {
//                    if (!extendedSet.Contains(neighbor))
//                        extendedSet.Add(neighbor);
//                }
//            }
//            return this.GetBruteForcePathViaFiniteSet(extendedSet, s, g);
//        }
//    }
//}
