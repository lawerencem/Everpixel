using Controller.Characters;
using Controller.Managers;
using Controller.Managers.Map;
using Model.Events.Combat;
using Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.Map;

namespace Controller.Map
{
    public class TileController : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private bool _doubleClick = false;
        private double _clickDelta = 0.5;
        private DateTime _clickTime;

        public List<TileController> Adjacent { get; set; }
        public List<GenericCharacterController> DeadCharacters { get; set; }
        public TileControllerFlags Flags { get; set; }
        public GameObject Handle { get; set; }
        public HexTile Model { get; set; }
        public HexTileView View { get; set; }

        public TileController()
        {
            this.Adjacent = new List<TileController>();
            this.DeadCharacters = new List<GenericCharacterController>();
            this.Flags = new TileControllerFlags();
            this.Model = new HexTile();
        }

        public void Update()
        {
            if (_doubleClick && (DateTime.Now > (this._clickTime.AddSeconds(this._clickDelta))))
                this._doubleClick = false;       
        }

        public TileController GetNearestEmptyTile()
        {
            if (this.Model.Current == null)
                return this;
            else
            {
                var openSet = new List<TileController>() { this };
                var closed = new List<TileController>();
                while (openSet.Count > 0)
                {
                    var tile = openSet.ElementAt(0);
                    var probed = closed.Find(x => x == tile);
                    if (probed == null)
                    {
                        if (tile.Model.Current == null)
                            return tile;
                        else
                            openSet.Add(tile);
                    }
                    foreach (var neighbor in tile.Adjacent)
                        openSet.Add(neighbor);
                    closed.Add(tile);
                    openSet.RemoveAt(0);
                }
            }
            return null;
        }

        public void Init(GameObject o)
        {
            this.Handle = o;
            this._collider = this.Handle.AddComponent<BoxCollider2D>();
        }

        public void OnMouseDown()
        {
            if (TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
            {
                var perform = new PerformActionEvent(CombatEventManager.Instance, this, CombatEventManager.Instance.ActionPerformedCallback);
            }
            else if (this.Model.Current == null)
            {
                var e = new HexSelectedForMoveEvent(this, CombatEventManager.Instance);

                if (this._doubleClick)
                {
                    var doubleClick = new TileDoubleClickEvent(CombatEventManager.Instance, this);
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
            this.HandleHover();
            this.HandleAoE();
        }

        public void OnMouseExit()
        {
            CMapGUIController.Instance.SetHoverModalInactive();
        }

        public void SetModel(HexTile t)
        {
            this.Model = t;
            this.Model.Parent = this;
        }

        public void SetView(HexTileView v)
        {
            this.View = v;
        }

        private void HandleAoE()
        {
            var curChar = CombatEventManager.Instance.GetCurrentCharacter();
            var curAbility = CombatEventManager.Instance.GetCurrentAbility();
            var aoe = new List<TileController>();
            if (curAbility != null && TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
            {
                if (!curAbility.isLoSCast())
                {
                    var hexes = this.Model.GetAoETiles((int)curAbility.AoE);
                    foreach (var hex in hexes)
                        aoe.Add(hex.Parent);
                }
                else
                {
                    var hexes = this.Model.GetLOSTiles(curChar.CurrentTile.Model, curAbility.Range);
                    foreach (var hex in hexes)
                        aoe.Add(hex.Parent);
                }
            }
            var hover = new TileHoverDecoEvent(CombatEventManager.Instance, this, aoe);
        }

        private void HandleHover()
        {
            
            if (this.Model.Current != null && this.Model.Current.GetType() == typeof(GenericCharacterController))
            {
                var fov = Camera.main.fieldOfView;
                var character = this.Model.Current as GenericCharacterController;
                var position = character.Handle.transform.position;
                position.x += (float)(fov * 0.025);
                position.y += (float)(fov * 0.025);
                CMapGUIController.Instance.SetHoverModalHeaderText(character.View.Name);
                CMapGUIController.Instance.SetHoverModalLocation(position);
                var controller = this.Model.Current as GenericCharacterController;
                CMapGUIController.Instance.SetHoverModalStatValues(controller.Model);
            }
        }
    }
}
