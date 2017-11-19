using Assets.Controller.Map.Tile;
using Assets.Template.Hex;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Map.Combat.Tile
{
    public class MTile : IHex<MTile>
    {
        private List<MTile> _adjacent;
        private CTile _controller;
        private int _height;
        private MMap _map;
        private HexTile _tile;

        public Vector3 Center { get { return this._tile.Center; } }
        public int Col { get { return this._tile.Col; } }
        public CTile Controller { get { return this._controller; } }
        public int Height { get { return this._height; } }
        public MMap Map { get { return this._map; } }
        public int Row { get { return this._tile.Row; } }

        public MTile(HexTile tile)
        {
            this._adjacent = new List<MTile>();
            this._height = 1;
            this._tile = tile;
            this._tile.SetParentContainer(this);
        }

        public List<MTile> GetAdjacent() { return this._adjacent; }
        public HexTile GetTile() { return this._tile; }

        public void SetCenter(Vector3 c) { this._tile.SetCenter(c); }
        public void SetController(CTile c) { this._controller = c; }
        public void SetHeight(int h) { this._height = h; }
        public void SetMap(MMap m) { this._map = m; }

        public void Init()
        {
            foreach (var tile in this._tile.GetAdjacent())
                if (tile.ParentContainer.GetType().Equals(this.GetType()))
                    this._adjacent.Add(tile.ParentContainer as MTile);
        }

        public List<MTile> GetAoETiles(int dist)
        {
            var tiles = this._tile.GetAoETiles(dist);
            return this.Convert(tiles);
        }

        public int GetCost()
        {
            return this._tile.Cost;
        }

        public int GetTravelCost(MTile tile)
        {
            var adjacent = this.GetAdjacent().Find(x => x.Equals(tile));
            if (adjacent != null)
            {
                double cost = tile.GetCost();
                int heightDiff = Math.Abs(this._height - tile.Height);
                if (heightDiff > 0)
                    cost *= (heightDiff + 0.5);
                return (int)Math.Ceiling(cost);
            }   
            else
                return int.MaxValue; // Sentinel value
        }

        public MTile GetRandomNearbyTile(int probes)
        {
            var tile = this._tile.GetRandomNearbyTile(probes);
            return this.Convert(tile);
        }

        public List<MTile> GetRaycastTiles(MTile t, int dist)
        {
            var tiles = this._tile.GetRaycastTiles(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public List<MTile> GetRayTilesViaDistN(MTile t, int dist)
        {
            var tiles = this._tile.GetRayTilesViaDistN(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public List<MTile> GetRayTilesViaDistNE(MTile t, int dist)
        {
            var tiles = this._tile.GetRayTilesViaDistNE(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public List<MTile> GetRayTilesViaDistSE(MTile t, int dist)
        {
            var tiles = this._tile.GetRayTilesViaDistSE(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public List<MTile> GetRayTilesViaDistS(MTile t, int dist)
        {
            var tiles = this._tile.GetRayTilesViaDistS(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public List<MTile> GetRayTilesViaDistSW(MTile t, int dist)
        {
            var tiles = this._tile.GetRayTilesViaDistSW(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public List<MTile> GetRayTilesViaDistNW(MTile t, int dist)
        {
            var tiles = this._tile.GetRayTilesViaDistNW(t.GetTile(), dist);
            return this.Convert(tiles);
        }

        public MTile GetN()
        {
            var tile = this._tile.GetN();
            return this.Convert(tile);
        }

        public MTile GetNE()
        {
            var tile = this._tile.GetNE();
            return this.Convert(tile);
        }

        public MTile GetSE()
        {
            var tile = this._tile.GetSE();
            return this.Convert(tile);
        }

        public MTile GetS()
        {
            var tile = this._tile.GetS();
            return this.Convert(tile);
        }

        public MTile GetSW()
        {
            var tile = this._tile.GetSW();
            return this.Convert(tile);
        }

        public MTile GetNW()
        {
            var tile = this._tile.GetNW();
            return this.Convert(tile);
        }

        public bool IsTileN(MTile target, int dist)
        {
            return this._tile.IsTileN(target.GetTile(), dist);
        }

        public bool IsTileNE(MTile target, int dist)
        {
            return this._tile.IsTileNE(target.GetTile(), dist);
        }

        public bool IsTileSE(MTile target, int dist)
        {
            return this._tile.IsTileSE(target.GetTile(), dist);
        }

        public bool IsTileS(MTile target, int dist)
        {
            return this._tile.IsTileS(target.GetTile(), dist);
        }

        public bool IsTileSW(MTile target, int dist)
        {
            return this._tile.IsTileSW(target.GetTile(), dist);
        }

        public bool IsTileNW(MTile target, int dist)
        {
            return this._tile.IsTileNW(target.GetTile(), dist);
        }

        public void SetCost(int cost)
        {
            this._tile.SetCost(cost);
        }

        private List<MTile> Convert(List<HexTile> tiles)
        {
            var list = new List<MTile>();
            foreach(var tile in tiles)
            {
                if (tile.ParentContainer != null && tile.ParentContainer.GetType().Equals(this.GetType()))
                    list.Add(tile.ParentContainer as MTile);
            }
            return list;
        }

        private MTile Convert(HexTile tile)
        {
            if (tile == null)
                return null;
            else if (tile.ParentContainer != null && tile.ParentContainer.GetType().Equals(this.GetType()))
                return tile.ParentContainer as MTile;
            else
                return null;
        }
    }
}
