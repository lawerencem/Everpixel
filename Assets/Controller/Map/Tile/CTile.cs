using Assets.Controller.Character;
using Assets.Model.Map.Tile;
using Assets.Model.Zone;
using Assets.Template.CB;
using Assets.Template.Hex;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Controller.Map.Tile
{
    public class CTile
    {
        private GameObject _handle;
        private GameObject _liquidHandle;
        private MTile _model;
        private List<CChar> _nonCurrent;
        private VTile _view;
        private List<AZone> _zones;

        public object Current { get { return this._model.GetCurrentOccupant(); } }
        public Vector3 Center { get { return this._model.Center; } }
        public GameObject LiquidHandle { get { return this._liquidHandle; } }
        public GameObject Handle { get { return this._handle; } }
        public MTile Model { get { return this._model; } }
        public VTile View { get { return this._view; } }

        public void AddNonCurrent(CChar c)
        {
            this._nonCurrent.Add(c);
        }

        public void AddZone(AZone zone)
        {
            this._zones.Add(zone);
            zone.SetParent(this);
        }

        public CTile(MTile tile)
        {
            this._handle = new GameObject(Layers.TILE);
            this._handle.transform.position = tile.Center;
            this._nonCurrent = new List<CChar>();
            this._zones = new List<AZone>();
            this.SetModel(tile);
        }

        public List<CTile> GetAdjacent()
        {
            var adjacent = new List<CTile>();
            var tiles = this._model.GetAdjacentMTiles();
            foreach (var tile in tiles)
                adjacent.Add(tile.Controller);
            return adjacent;
        }

        public FTile GetFlags()
        {
            return this._model.GetFlags();
        }

        public List<CChar> GetNonCurrent()
        {
            return this._nonCurrent;
        }

        public List<AZone> GetZones()
        {
            return this._zones;
        }

        public CTile GetNearestEmptyTile()
        {
            if (this.Current == null)
                return this;
            else
            {
                var openSet = new List<CTile>() { this };
                var closed = new List<CTile>();
                while (openSet.Count > 0)
                {
                    var tile = openSet[0];
                    var probed = closed.Find(x => x == tile);
                    if (probed == null)
                    {
                        if (tile.Current == null)
                            return tile;
                        else
                            openSet.Add(tile);
                    }
                    foreach (var neighbor in tile.GetAdjacent())
                        openSet.Add(neighbor);
                    closed.Add(tile);
                    openSet.RemoveAt(0);
                }
            }
            return null;
        }

        public CTile GetRandomNearbyEmptyTile(int probes)
        {
            for(int i = 0; i < 5; i++)
            {
                var tile = (MTile) this._model.GetRandomNearbyTile(probes);
                if (tile.Controller.Current == null)
                    return tile.Controller;
            }

            return this.GetNearestEmptyTile();
        }

        public CTile GetRandomNearbyTile(int probes)
        {
            var tile = (MTile) this._model.GetRandomNearbyTile(probes);
            return tile.Controller;
        }

        public void InitLiquidTile()
        {
            if (this._liquidHandle == null)
                this._liquidHandle = new GameObject(Layers.TILE_LIQUID);
        }

        public void ProcessEnterTile(CChar c, Callback cb)
        {
            foreach(var zone in this._zones)
                zone.ProcessEnterZone(c, cb);
            this.Model.ProcessEnterTile(c);
        }

        public void ProcessExitTile(CChar tgt, bool doAttackOfOpportunity, Callback callback)
        {
            foreach (var zone in this._zones)
            {
                zone.ProcessExitZone(tgt, doAttackOfOpportunity, callback);
            }
        }

        public void ProcessTurnInTile(CChar c, Callback cb)
        {
            foreach (var zone in this._zones)
            {
                zone.ProcessTurnInZone(c, cb);
            }
        }

        public void SetCurrent(IHexOccupant o)
        {
            this.Model.SetCurrentOccupant(o);
            if (o != null)
                o.SetCurrentHex(this.Model);
        }

        public void SetModel(MTile t)
        {
            this._model = t;
            this.Model.SetController(this);
            this.SetView(new VTile(this._model));
        }

        public void SetView(VTile v)
        {
            this._view = v;
        }

        public void RemoveZone(AZone zone)
        {
            this._zones.Remove(zone);
        }
    }
}
