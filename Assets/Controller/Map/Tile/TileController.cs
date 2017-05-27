using Controller.Managers;
using Generics.Hex;
using Model.Events.Combat;
using Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using View.Events;
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
        public TileControllerFlags Flags { get; set; }
        public GameObject Handle { get; set; }
        public HexTile Model { get; set; }
        public HexTileView View { get; set; }

        public TileController()
        {
            this.Adjacent = new List<TileController>();
            this.Flags = new TileControllerFlags();
            this.Model = new HexTile();
        }

        public void Start()
        {

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
            if (this.Model.Current == null)
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

        public void OnMouseOver()
        {
            var hover = new TileHoverEvent(GUIEventManager.Instance, this);
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
