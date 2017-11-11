using Assets.Controller.Character;
using Assets.Model.Map.Tile;
using Assets.Model.Zone;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Map.Tile
{
    public class CTile
    {
        private object _current;
        private FCTile _flags;
        private GameObject _handle;
        private MTile _model;
        private List<CChar> _nonCurrent;
        private VTile _view;
        private List<AZone> _zones;

        public object Current { get { return this._current; } }
        public FCTile Flags { get { return this._flags; } }
        public GameObject Handle { get { return this._handle; } }
        public MTile Model { get { return this._model; } }
        public VTile View { get { return this._view; } }

        public void AddNonCurrent(CChar c) { this._nonCurrent.Add(c); }
        public void AddZone(AZone zone) { this._zones.Add(zone); }

        public List<CTile> GetAdjacent()
        {
            var adjacent = new List<CTile>();
            var tiles = this._model.GetAdjacent();
            foreach (var tile in tiles)
                adjacent.Add(tile.Controller);
            return adjacent;
        }
        public List<CChar> GetNonCurrent() { return this._nonCurrent; }
        public List<AZone> GetZones() { return this._zones; }

        public void RemoveZone(AZone zone) { this._zones.Remove(zone); }

        public void SetCurrent(object o) { this._current = o; }

        public CTile(MTile tile)
        {
            this._flags = new FCTile();
            this._handle = new GameObject(Layers.TILE);
            this._handle.transform.position = tile.Center;
            this._nonCurrent = new List<CChar>();
            this._zones = new List<AZone>();
            this.SetModel(tile);
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

        public CTile GetRandomNearbyTile(int probes)
        {
            var tile = this._model.GetRandomNearbyTile(probes);
            return tile.Controller;
        }

        public void ProcessEnterTile(CChar c)
        {
            foreach(var zone in this._zones)
            {
                zone.ProcessEnterZone(c);
            }
        }

        public void ProcessExitTile(CChar c)
        {
            foreach (var zone in this._zones)
            {
                zone.ProcessExitZone(c);
            }
        }

        public void ProcessTurnInTile(CChar c)
        {
            foreach (var zone in this._zones)
            {
                zone.ProcessTurnInZone(c);
            }
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
    }
}
