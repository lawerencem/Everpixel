using Assets.Template.Util;
using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public class HexTileLogic
    {
        public HexTileLogic()
        {

        }
    
        public List<IHex> GetArcTiles(IHex source, IHex target)
        {
            List<IHex> tiles = new List<IHex>();
            if (source != null && target != null)
            {
                if (source.IsTileN(target, 1))
                {
                    if (source.GetNE() != null)
                        tiles.Add(source.GetNE());
                    if (source.GetNW() != null)
                        tiles.Add(source.GetNW());
                    tiles.Add(target);
                }
                else if (source.IsTileNE(target, 1))
                {
                    if (source.GetN() != null)
                        tiles.Add(source.GetN());
                    if (source.GetSE() != null)
                        tiles.Add(source.GetSE());
                    tiles.Add(target);
                }
                else if (source.IsTileSE(target, 1))
                {
                    if (source.GetNE() != null)
                        tiles.Add(source.GetNE());
                    if (source.GetS() != null)
                        tiles.Add(source.GetS());
                    tiles.Add(target);
                }
                else if (source.IsTileS(target, 1))
                {
                    if (source.GetSE() != null)
                        tiles.Add(source.GetNE());
                    if (source.GetSW() != null)
                        tiles.Add(source.GetSW());
                    tiles.Add(target);
                }
                else if (source.IsTileSW(target, 1))
                {
                    if (source.GetS() != null)
                        tiles.Add(source.GetS());
                    if (source.GetNW() != null)
                        tiles.Add(source.GetNW());
                    tiles.Add(target);
                }
                else if (source.IsTileNW(target, 1))
                {
                    if (source.GetSW() != null)
                        tiles.Add(source.GetSW());
                    if (source.GetN() != null)
                        tiles.Add(source.GetN());
                    tiles.Add(target);
                }
            }
            return tiles;
        }

        public IHex GetRandomNearbyTile(int probes, IHex tile)
        {
            var currNeighbors = tile.GetAdjacent();
            for(int i = 0; i < probes; i++)
            {
                var random = ListUtil<IHex>.GetRandomElement(currNeighbors);
                currNeighbors = random.GetAdjacent();
            }
            return ListUtil<IHex>.GetRandomElement(currNeighbors); 
        }

        public List<IHex> GetAoETiles(int dist, IHex tile)
        {
            var tiles = new List<IHex>() { tile };

            var closedSet = new List<IHex>();
            var probeSet = new List<IHex>() { tile };
            var waitingSet = new List<IHex>() { };

            for (int i = dist; i > 0; i--)
            {
                foreach (var probe in probeSet)
                {
                    foreach (var neighbor in probe.GetAdjacent())
                    {
                        var found = closedSet.Find(
                            x => x.GetCol() == neighbor.GetCol() && x.GetRow() == neighbor.GetRow());
                        if (found == null)
                        {
                            waitingSet.Add(neighbor);
                            tiles.Add(neighbor);
                            closedSet.Add(neighbor);
                        }
                    }
                }
                probeSet.Clear();
                foreach (var wait in waitingSet) { probeSet.Add(wait); }
                waitingSet.Clear();
            }
            return tiles;
        }

        public List<IHex> GetRaycastTiles(IHex source, IHex target, int dist)
        {
            var list = new List<IHex>();

            if (this.IsTileN(source, target, dist))
                list = this.GetRayTilesViaDistN(target, dist);
            else if (this.IsTileNE(source, target, dist))
                list = this.GetRayTilesViaDistNE(target, dist);
            else if (this.IsTileSE(source, target, dist))
                list = this.GetRayTilesViaDistSE(target, dist);
            else if (this.IsTileS(source, target, dist))
                list = this.GetRayTilesViaDistS(target, dist);
            else if (this.IsTileSW(source, target, dist))
                list = this.GetRayTilesViaDistSW(target, dist);
            else if (this.IsTileNW(source, target, dist))
                list = this.GetRayTilesViaDistNW(target, dist);

            return list;
        }

        public bool IsTileN(IHex source, IHex target, int dist)
        {
            var cur = source;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetN();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileNE(IHex source, IHex target, int dist)
        {
            var cur = source;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNE();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileSE(IHex source, IHex target, int dist)
        {
            var cur = source;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSE();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileS(IHex source, IHex target, int dist)
        {
            var cur = source;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetS();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileSW(IHex source, IHex target, int dist)
        {
            var cur = source;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSW();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileNW(IHex source, IHex target, int dist)
        {
            var cur = source;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNW();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public List<IHex> GetRayTilesViaDistN(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetN();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistNE(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNE();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistSE(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSE();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistS(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetS();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistSW(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSW();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistNW(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNW();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }
    }
}
