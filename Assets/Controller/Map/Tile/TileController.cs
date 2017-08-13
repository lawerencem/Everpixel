using Assets.Controller.Character;
using Assets.Model.Map;
using Assets.Model.Zone;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Map.Tile
{
    public class TileController
    {
        private List<TileController> _adjacent;
        private object _current;
        private FTileController _flags;
        private GameObject _handle;
        private MTile _model;
        private List<CharController> _nonCurrent;
        private VTile _view;
        private List<AZone> _zones;

        public object Current { get { return this._current; } }
        public FTileController Flags { get { return this._flags; } }
        public GameObject Handle { get { return this._handle; } }
        public MTile Model { get { return this._model; } }
        public VTile View { get { return this._view; } }

        public void AddZone(AZone zone) { this._zones.Add(zone); }

        public List<TileController> GetAdjacent() { return this._adjacent; }
        public List<CharController> GetNonCurrent() { return this._nonCurrent; }
        public List<AZone> GetZones() { return this._zones; }

        public void RemoveZone(AZone zone) { this._zones.Remove(zone); }

        public void SetCurrent(object o) { this._current = o; }

        public TileController(MTile tile)
        {
            this._adjacent = new List<TileController>();
            this._flags = new FTileController();
            this._handle = new GameObject(ViewParams.TILE);
            this._handle.transform.position = tile.Center;
            this._nonCurrent = new List<CharController>();
            this._zones = new List<AZone>();
            this.SetModel(tile);
        }

        public TileController GetNearestEmptyTile()
        {
            if (this.Current == null)
                return this;
            else
            {
                var openSet = new List<TileController>() { this };
                var closed = new List<TileController>();
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

        //        private void HandleAoE()
        //        {
        //            var curChar = CombatEventManager.Instance.GetCurrentCharacter();
        //            var curAbility = CombatEventManager.Instance.GetCurrentAbility();
        //            var aoe = new List<TileController>();
        //            if (curAbility != null && TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
        //            {
        //                if (!curAbility.isRayCast())
        //                {
        //                    var hexes = this.Model.GetAoETiles((int)curAbility.AoE);
        //                    foreach (var hex in hexes)
        //                        aoe.Add(hex.Parent);
        //                }
        //                else
        //                {
        //                    var hexes = this.Model.GetRaycastTiles(curChar.CurrentTile.Model, curAbility.Range);
        //                    foreach (var hex in hexes)
        //                        aoe.Add(hex.Parent);
        //                }
        //            }
        //            var hover = new EvTileHover(CombatEventManager.Instance, this, aoe);
        //        }
    }
}