using Assets.Controller.Character;
using Assets.Model.Map;
using Assets.Model.Zone;
using Assets.View.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Map
{
    public class TileController : MonoBehaviour
    {
        private List<TileController> _adjacent;
        private BoxCollider2D _collider;
        private object _current;
        private double _clickDelta = 0.5;
        private DateTime _clickTime;
        private bool _doubleClick = false;
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

        public void SetHandle(GameObject o) { this._handle = o; }

        public TileController(MTile tile)
        {
            this._adjacent = new List<TileController>();
            this._nonCurrent = new List<CharController>();
            this._flags = new FTileController();
            this._zones = new List<AZone>();
            this.SetModel(tile);
        }

        public void Update()
        {
            if (_doubleClick && (DateTime.Now > (this._clickTime.AddSeconds(this._clickDelta))))
                this._doubleClick = false;
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

        public void Init(GameObject o)
        {
            this.SetHandle(o);
            this._collider = this.Handle.AddComponent<BoxCollider2D>();
        }

        public void OnMouseDown()
        {
            if (FTileController.HasFlag(this.Flags.CurFlags, FTileController.Flags.AwaitingAction))
            {
                //var perform = new EvPerformAction(CombatEventManager.Instance, this, CombatEventManager.Instance.ActionPerformedCallback);
            }
            else if (this.Current == null)
            {
                //var e = new HexSelectedForMoveEvent(this, CombatEventManager.Instance);

                if (this._doubleClick)
                {
                    //var doubleClick = new EvTileDoubleClick(CombatEventManager.Instance, this);
                }
                else
                {
                    this._clickTime = System.DateTime.Now;
                    this._doubleClick = true;
                }
            }
        }

        public void OnMouseEnter()
        {
            //this.HandleHover();
            //this.HandleAoE();
        }

        public void OnMouseExit()
        {
            //CMapGUIController.Instance.SetHoverModalInactive();
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

        //        private void HandleHover()
        //        {
        //            this.HandleHoverTargetStats();
        //            this.HandleHoverTargetDamage();
        //        }

        //        private void HandleHoverTargetDamage()
        //        {
        //            if (this.Model.Current != null &&
        //                this.Model.Current.GetType() == typeof(CharController) &&
        //                CombatEventManager.Instance.GetCurrentAbility() != null)
        //            {
        //                var predict = new EvPredictAction(CombatEventManager.Instance);

        //                predict.Container.Ability = CombatEventManager.Instance.GetCurrentAbility();
        //                predict.Container.Source = CombatEventManager.Instance.GetCurrentCharacter();
        //                predict.Container.Target = this;
        //                var targets = predict.Container.Ability.GetAoETiles(
        //                    predict.Container.Source.CurrentTile, 
        //                    predict.Container.Target, 
        //                    predict.Container.Ability.Range);

        //                foreach (var target in targets)
        //                {
        //                    var hit = new Hit(predict.Container.Source, predict.Container.Target, predict.Container.Ability);
        //                    predict.Container.Hits.Add(hit);
        //                }

        //                predict.Process();
        //                CMapGUIController.Instance.SetHoverModalDamageValues(predict);
        //            }
        //            else
        //                CMapGUIController.Instance.SetDmgModalInactive();
        //        }

        //        private void HandleHoverTargetStats()
        //        {
        //            if (this.Model.Current != null && this.Model.Current.GetType() == typeof(CharController))
        //            {
        //                var fov = Camera.main.fieldOfView;
        //                var character = this.Model.Current as CharController;
        //                var position = character.Handle.transform.position;
        //                position.x += (float)(fov * 0.025);
        //                position.y += (float)(fov * 0.025);
        //                CMapGUIController.Instance.SetHoverModalHeaderText(character.View.Name);
        //                CMapGUIController.Instance.SetHoverModalLocation(position);
        //                var controller = this.Model.Current as CharController;
        //                CMapGUIController.Instance.SetHoverModalStatValues(controller.Model);
        //            }
        //        }
        //    }
        //}
    }
}