using Controller.Characters;
using Controller.Managers;
using Controller.Managers.Map;
using Model.Events.Combat;
using Model.Map;
using System;
using System.Collections.Generic;
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

        public void Init(GameObject o)
        {
            this.Handle = o;
            this._collider = this.Handle.AddComponent<BoxCollider2D>();
        }

        public void OnMouseDown()
        {
            if (TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.PotentialTileSelect))
            {
                var confirmed = new ActionConfirmedEvent(CombatEventManager.Instance, this);
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
            else
            {
                if (TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.PotentialAttack))
                {
                    var confirmed = new ActionConfirmedEvent(CombatEventManager.Instance, this);
                }
            }
        }

        public void OnMouseEnter()
        {
            var hover = new TileHoverDecoEvent(CombatEventManager.Instance, this);
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
    }
}
