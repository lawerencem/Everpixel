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
        private CTile _tile;
        private BoxCollider2D _collider;
        private double _clickDelta = 0.5;
        private DateTime _clickTime;
        private bool _doubleClick = false;

        public void Update()
        {
            if (_doubleClick && (DateTime.Now > (this._clickTime.AddSeconds(this._clickDelta))))
                this._doubleClick = false;
        }

        public void InitTile(CTile t)
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
            if (this._tile.Current != null && this._tile.Current.GetType().Equals(typeof(CChar)))
            {
                var fov = Camera.main.fieldOfView;
                var charController = this._tile.Current as CChar;
                var position = charController.Handle.transform.position;
                position.x += (float)(fov * 0.025);
                position.y += (float)(fov * 0.025);
                GUIManager.Instance.SetHoverModalHeaderText(charController.View.Name.Replace("_", " "));
                GUIManager.Instance.SetHoverModalLocation(position);
                GUIManager.Instance.SetHoverModalStatValues(charController.Proxy);
            }
        }

        private void HandleHoverTargetDamage()
        {
            if (this._tile.Current != null && this._tile.Current.GetType().Equals(typeof(CChar)))
            {
                if (CombatManager.Instance.GetCurrentAbility() != EAbility.None)
                {
                    var data = new ActionData();
                    data.Ability = CombatManager.Instance.GetCurrentAbility();
                    data.LWeapon = CombatManager.Instance.GetLWeapon();
                    data.ParentWeapon = CombatManager.Instance.GetCurrentWeapon();
                    data.Source = CombatManager.Instance.GetCurrentlyActing();
                    data.Target = this._tile;
                    data.WpnAbility = CombatManager.Instance.GetIsWpnAbility();
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
