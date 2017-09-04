using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Controller.Map.Combat;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.View;
using Assets.View.Event;
using System;
using UnityEngine;

namespace Assets.Controller.Map.Tile
{
    public class STile : MonoBehaviour
    {
        private TileController _tile;
        private BoxCollider2D _collider;
        private double _clickDelta = 0.5;
        private DateTime _clickTime;
        private bool _doubleClick = false;

        public void Update()
        {
            if (_doubleClick && (DateTime.Now > (this._clickTime.AddSeconds(this._clickDelta))))
                this._doubleClick = false;
        }

        public void InitTile(TileController t)
        {
            this._tile = t;
            this._collider = this._tile.Handle.AddComponent<BoxCollider2D>();
            this._collider.size = ViewParams.TILE_COLLIDER_SIZE;
        }

        public void OnMouseDown()
        {
            if (this._tile != null && !GUIManager.Instance.GetInteractionLocked())
            {
                var data = new EvTileClickData();
                if (this._doubleClick)
                    data.DoubleClick = true;
                data.Target = this._tile;
                this._clickTime = DateTime.Now;
                this._doubleClick = true;
                var e = new EvTileClick(data);
                e.TryProcess();
            }
        }

        public void OnMouseEnter()
        {
            this.HandleHover();
        }

        public void OnMouseExit()
        {
            GUIManager.Instance.SetHoverModalInactive();
        }

        private void HandleHover()
        {
            if (!GUIManager.Instance.GetGUILocked())
            {
                this.HandleHoverTargetStats();
                this.HandleHoverTargetDamage();
                VMapCombatController.Instance.DecorateHover(this._tile);
            }
        }

        private void HandleHoverTargetStats()
        {
            if (this._tile.Current != null && this._tile.Current.GetType().Equals(typeof(CharController)))
            {
                var fov = Camera.main.fieldOfView;
                var character = this._tile.Current as CharController;
                var position = character.Handle.transform.position;
                position.x += (float)(fov * 0.025);
                position.y += (float)(fov * 0.025);
                GUIManager.Instance.SetHoverModalHeaderText(character.View.Name);
                GUIManager.Instance.SetHoverModalLocation(position);
                var target = this._tile.Current as CharController;
                GUIManager.Instance.SetHoverModalStatValues(target.Proxy);
            }
        }

        private void HandleHoverTargetDamage()
        {
            if (this._tile.Current != null && this._tile.Current.GetType().Equals(typeof(CharController)))
            {
                if (CombatManager.Instance.GetCurrentAbility() != EAbility.None)
                {
                    var data = new ActionData();
                    data.Ability = CombatManager.Instance.GetCurrentAbility();
                    data.LWeapon = CombatManager.Instance.GetLWeapon();
                    data.Source = CombatManager.Instance.GetCurrentlyActing();
                    data.Target = this._tile;
                    var action = new MAction(data);
                    action.TryPredict();
                    GUIManager.Instance.SetHoverModalDamageValues(action);
                }
                else
                    GUIManager.Instance.SetHoverModalDamageActive(false);
            }
            else
                GUIManager.Instance.SetHoverModalDamageActive(false);
        }
    }
}
