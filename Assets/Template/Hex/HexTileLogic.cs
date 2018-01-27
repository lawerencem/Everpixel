using Assets.Template.Util;
using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public class HexTileLogic
    {
        public HexTileLogic()
        {

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

        public List<IHex> GetRaycastTiles(IHex t, int dist)
        {
            var list = new List<IHex>();

            if (this.IsTileN(t, dist))
                list = this.GetRayTilesViaDistN(t, dist);
            else if (this.IsTileNE(t, dist))
                list = this.GetRayTilesViaDistNE(t, dist);
            else if (this.IsTileSE(t, dist))
                list = this.GetRayTilesViaDistSE(t, dist);
            else if (this.IsTileS(t, dist))
                list = this.GetRayTilesViaDistS(t, dist);
            else if (this.IsTileSW(t, dist))
                list = this.GetRayTilesViaDistSW(t, dist);
            else if (this.IsTileNW(t, dist))
                list = this.GetRayTilesViaDistNW(t, dist);

            return list;
        }

        public bool IsTileN(IHex target, int dist)
        {
            var cur = this as IHex;
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

        public bool IsTileNE(IHex target, int dist)
        {
            var cur = this as IHex;
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

        public bool IsTileSE(IHex target, int dist)
        {
            var cur = this as IHex;
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

        public bool IsTileS(IHex target, int dist)
        {
            var cur = this as IHex;
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

        public bool IsTileSW(IHex target, int dist)
        {
            var cur = this as IHex;
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

        public bool IsTileNW(IHex target, int dist)
        {
            var cur = this as IHex;
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
