using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.AI.Particle;
using Assets.Template.Hex;
using Assets.Template.Other;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Map.Tile
{
    public class MTile : IHex
    {
        private MTile _n;
        private MTile _ne;
        private MTile _se;
        private MTile _s;
        private MTile _sw;
        private MTile _nw;

        private List<MTile> _adjacent;
        private Vector3 _center;
        private int _col;
        private int _cost;
        private CTile _controller;
        private IHexOccupant _currentOccupant;
        private FTile _flags;
        private int _height;
        private bool _liquid;
        private MMap _map;
        private ParticleContainer _particles;
        private int _row;
        private int _staminaCost;
        private double _threatMod;
        private ETile _type;
        private double _vulnMod;

        public CTile Controller { get { return this._controller; } }
        public Vector3 Center { get { return this._center; } }
        public bool Liquid { get { return this._liquid; } }
        public MMap Map { get { return this._map; } }
        public ETile Type { get { return this._type; } }

        public MTile()
        {
            this._adjacent = new List<MTile>();
            this._flags = new FTile();
            this._height = 1;
            this._liquid = false;
            this._particles = new ParticleContainer();
        }

        public void SetN(MTile t) { this._adjacent.Add(t); this._n = t; }
        public void SetNE(MTile t) { this._adjacent.Add(t); this._ne = t; }
        public void SetSE(MTile t) { this._adjacent.Add(t); this._se = t; }
        public void SetS(MTile t) { this._adjacent.Add(t); this._s = t; }
        public void SetSW(MTile t) { this._adjacent.Add(t); this._sw = t; }
        public void SetNW(MTile t) { this._adjacent.Add(t); this._nw = t; }

        public void SetAPCost(int cost) { this._cost = cost; }
        public void SetCenter(Vector3 center) { this._center = center; }
        public void SetCol(int col) { this._col = col; }
        public void SetController(CTile c) { this._controller = c; }
        public void SetCurrentOccupant(IHexOccupant o) { this._currentOccupant = o; }
        public void SetHeight(int h) { this._height = h; }
        public void SetLiquid(bool liquid) { this._liquid = liquid; }
        public void SetMap(MMap m) { this._map = m; }
        public void SetRow(int row) { this._row = row; }
        public void SetStaminaCost(int cost) { this._staminaCost = cost; }
        public void SetThreatMod(double mod) { this._threatMod = mod; }
        public void SetType(ETile type) { this._type = type; }
        public void SetVulnMod(double mod) { this._vulnMod = mod; }

        public MTile GetN() { return this._n; }
        public MTile GetNE() { return this._ne; }
        public MTile GetSE() { return this._se; }
        public MTile GetS() { return this._s; }
        public MTile GetSW() { return this._sw; }
        public MTile GetNW() { return this._nw; }

        public FTile GetFlags() { return this._flags; }
        public int GetHeight() { return this._height; }
        public double GetThreatMod() { return this._threatMod; }
        public double GetVulnMod() { return this._vulnMod; }

        public ParticleContainer GetParticles()
        {
            return this._particles;
        }

        public List<MTile> GetArcTiles(MTile target)
        {
            var logic = new HexTileLogic();
            return this.ConvertIHexToMTile(logic.GetArcTiles(this, target));
        }

        public List<MTile> GetAoETiles(int dist)
        {
            var logic = new HexTileLogic();
            return this.ConvertIHexToMTile(logic.GetAoETiles(dist, this));
        }

        public List<Pair<MTile, int>> GetAoETilesWithDistance(int dist)
        {
            var logic = new HexTileLogic();
            var pairs = logic.GetAoETilesWithDistance(dist, this);
            var distTiles = new List<Pair<MTile, int>>();
            foreach (var kvp in pairs)
                distTiles.Add(new Pair<MTile, int>(this.ConvertIHexToMTile(kvp.X), kvp.Y));
            return distTiles;
        }

        public int GetCost()
        {
            return this._cost;
        }

        public List<MTile> GetEmptyAoETiles(int dist)
        {
            var logic = new HexTileLogic();
            return this.ConvertIHexToMTile(logic.GetEmptyAoETiles(dist, this));
        }

        public MTile GetPushTile(MTile target)
        {
            var logic = new HexTileLogic();
            var tile = logic.GetPushTile(this, target);
            if (tile != null && tile.GetCurrentOccupant() == null)
                return tile as MTile;
            else
                return null;
        }

        public int GetTravelCost(MTile tile)
        {
            var adjacent = this.GetAdjacent().Find(x => x.Equals(tile));
            if (adjacent != null)
            {
                double cost = tile.GetCost();
                int heightDiff = Math.Abs(this._height - tile.GetHeight());
                if (heightDiff > 0)
                    cost *= (heightDiff + 0.5);
                return (int)Math.Ceiling(cost);
            }   
            else
                return int.MaxValue; // Sentinel value
        }

        public int GetStaminaCost()
        {
            return this._staminaCost;
        }

        public IHex GetRandomNearbyTile(int probes)
        {
            var logic = new HexTileLogic();
            return logic.GetRandomNearbyTile(probes, this);
        }

        public int GetCol()
        {
            return this._col;
        }

        public int GetRow()
        {
            return this._row;
        }

        public IHexOccupant GetCurrentOccupant()
        {
            return this._currentOccupant;
        }

        public List<IHex> GetRaycastTiles(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRaycastTiles(this, target, dist);
        }

        public List<MTile> GetConvertedRaycastTiles(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return this.ConvertIHexToMTile(logic.GetRaycastTiles(this, target, dist));
        }

        public List<IHex> GetRayTilesViaDistN(int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRayTilesViaDistN(this, dist);
        }

        public List<IHex> GetRayTilesViaDistNE(int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRayTilesViaDistNE(this, dist);
        }

        public List<IHex> GetRayTilesViaDistSE(int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRayTilesViaDistSE(this, dist);
        }

        public List<IHex> GetRayTilesViaDistS(int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRayTilesViaDistS(this, dist);
        }

        public List<IHex> GetRayTilesViaDistSW(int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRayTilesViaDistSW(this, dist);
        }

        public List<IHex> GetRayTilesViaDistNW(int dist)
        {
            var logic = new HexTileLogic();
            return logic.GetRayTilesViaDistNW(this, dist);
        }

        IHex IHex.GetN()
        {
            return this.GetN();
        }

        IHex IHex.GetNE()
        {
            return this.GetNE();
        }

        IHex IHex.GetSE()
        {
            return this.GetSE();
        }

        IHex IHex.GetS()
        {
            return this.GetS();
        }

        IHex IHex.GetSW()
        {
            return this.GetSW();
        }

        IHex IHex.GetNW()
        {
            return this.GetNW();
        }

        public bool IsTileN(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.IsTileN(this, target, dist);
        }

        public bool IsTileNE(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.IsTileNE(this, target, dist);
        }

        public bool IsTileSE(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.IsTileSE(this, target, dist);
        }

        public bool IsTileS(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.IsTileS(this, target, dist);
        }

        public bool IsTileSW(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.IsTileSW(this, target, dist);
        }

        public bool IsTileNW(IHex target, int dist)
        {
            var logic = new HexTileLogic();
            return logic.IsTileNW(this, target, dist);
        }

        public List<IHex> GetAdjacent()
        {
            return this._adjacent.ConvertAll(o => (IHex) o);
        }

        public List<MTile> GetAdjacentMTiles()
        {
            return this._adjacent;
        }

        List<IHex> IHex.GetAoETiles(int dist)
        {
            throw new NotImplementedException();
        }

        public void ProcessEnterTile(CChar c)
        {
            var logic = new TileLogic();
            logic.ProcessEnterTile(c, this);
        }

        private MTile ConvertIHexToMTile(IHex hex)
        {
            return hex as MTile;
        }

        private List<MTile> ConvertIHexToMTile(List<IHex> hexes)
        {
            var mTiles = new List<MTile>();
            foreach (var hex in hexes)
                mTiles.Add(hex as MTile);
            return mTiles;
        }
    }
}
