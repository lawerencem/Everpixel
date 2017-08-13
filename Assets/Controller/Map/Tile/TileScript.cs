﻿using System;
using UnityEngine;

namespace Assets.Controller.Map.Tile
{
    public class TileScript : MonoBehaviour
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
        }

        public void OnMouseDown()
        {
            if (this._tile != null)
            {
                if (FTileController.HasFlag(this._tile.Flags.CurFlags, FTileController.Flags.AwaitingAction))
                {
                    //var perform = new EvPerformAction(CombatEventManager.Instance, this, CombatEventManager.Instance.ActionPerformedCallback);
                }
                else if (this._tile.Current == null)
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