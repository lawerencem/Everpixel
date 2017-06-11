﻿using Controller.Characters;
using Controller.Map;
using Model.Characters;
using Model.Events.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;
using View.GUI;

namespace Controller.Managers.Map
{
    public class CMapGUIController
    {
        private List<GameObject> _boxImages = new List<GameObject>();
        private List<GameObject> _decorateTileFamily = new List<GameObject>();
        private GameObject _singleTile;

        private CMapGUIControllerHit _hitHelper = new CMapGUIControllerHit();
        private HoverModal _modal;
        private CMapGUIControllerParticle _particleHelper = new CMapGUIControllerParticle();

        private static CMapGUIController _instance;
        public static CMapGUIController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CMapGUIController();
                return _instance;
            }
        }

        public CMapGUIController()
        {
            this._modal = new HoverModal();
            this._modal.Init();
        }

        public void ApplyInjuryGraphics(ApplyInjuryEvent e)
        {
            this._particleHelper.ApplyInjuryParticle(e);
            this._hitHelper.ProcessInjury(e);
        }

        public void ClearDecoratedTiles()
        {
            foreach (var old in this._decorateTileFamily)
                GameObject.Destroy(old);
            this._decorateTileFamily.Clear();
        }

        public void DecorateHover(TileController t)
        {
            if (this._singleTile != null && this._singleTile != t)
                GameObject.Destroy(this._singleTile);

            if (TileControllerFlags.HasFlag(t.Flags.CurFlags, TileControllerFlags.Flags.PotentialAttack))
            {
                var sprite = MapBridge.Instance.GetHostileHoverSprite();
                DecorateSingleTile(t, sprite);
            }
        }

        public void DecoratePath(List<TileController> p)
        {
            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }

            if (p != null)
            {
                var sprite = MapBridge.Instance.GetMovePathSprite();
                foreach (var tile in p)
                {
                    DecorateFamilyOfTiles(tile, sprite);
                }
            }
        }

        public void DecoratePotentialAttackTiles(List<TileController> tiles)
        {
            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }

            if (tiles != null)
            {
                var sprite = MapBridge.Instance.GetPotentialAttackLocSprite();
                foreach(var t in tiles)
                {
                    DecorateFamilyOfTiles(t, sprite);
                }
            }
        }

        public void DisplayHitStatsEvent(DisplayHitStatsEvent e)
        {
            this._hitHelper.DisplayHitStatsEvent(e.Hit);
            //TODO Change this to check for melee...
            this._hitHelper.ProcessMeleeHitGraphics(e);
            this._hitHelper.ProcessSplatter(e);
        } 

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            this._hitHelper.ProcessCharacterKilled(e);
        }

        public void SetActingBoxToController(GenericCharacterController c)
        {
            this.SetTagText(CMapGUIControllerParams.NAME, c.View.Name);
            this.SetTagText(CMapGUIControllerParams.AP, c.Model.CurrentAP + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.AP).ToString());
            this.SetTagText(CMapGUIControllerParams.HP, c.Model.CurrentHP + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.HP).ToString());
            this.SetTagText(CMapGUIControllerParams.STAM, c.Model.CurrentStamina + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Stamina).ToString());
            this.SetTagText(CMapGUIControllerParams.MORALE, c.Model.CurrentMorale + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Morale).ToString());
            this.SetTagText(CMapGUIControllerParams.STAM, c.Model.CurrentStamina + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Stamina).ToString());

            if (c.Model.Armor != null)
                this.SetTagText(CMapGUIControllerParams.ARMOR, c.Model.Armor.Name);
            else
                this.SetTagText(CMapGUIControllerParams.ARMOR, "");
            if (c.Model.Helm != null)
                this.SetTagText(CMapGUIControllerParams.HELM, c.Model.Helm.Name);
            else
                this.SetTagText(CMapGUIControllerParams.HELM, "");
            if (c.Model.LWeapon != null)
                this.SetTagText(CMapGUIControllerParams.L_WEAP, c.Model.LWeapon.Name);
            else
                this.SetTagText(CMapGUIControllerParams.L_WEAP, "");
            if (c.Model.RWeapon != null)
                this.SetTagText(CMapGUIControllerParams.R_WEAP, c.Model.RWeapon.Name);
            else
                this.SetTagText(CMapGUIControllerParams.R_WEAP, "");
        }

        public void SetModalActive()
        {
            this._modal.SetModalActive();
        }

        public void SetModalInactive()
        {
            this._modal.SetModalInactive();
        }

        public void SetModalHeaderText(string toSet)
        {
            this._modal.SetModalHeaderText(toSet);
        }

        public void SetModalLocation(Vector3 pos)
        {
            this._modal.SetModalLocation(pos);
        }

        public void SetModalStatValues(GenericCharacter c)
        {
            this._modal.SetModalStatValues(c);
        }

        private void DecorateFamilyOfTiles(TileController tile, Sprite deco)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = tile.Model.Center;
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = 0.50f;
            renderer.color = color;
            this._decorateTileFamily.Add(tView);
        }

        private void DecorateSingleTile(TileController t, Sprite deco, float alpha = 0.50f)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
            this._singleTile = tView;
        }
        
        private void SetTagText(string tag, string toSet)
        {
            var tagged = GameObject.FindGameObjectWithTag(tag);
            if (tagged != null)
            {
                var text = tagged.GetComponent<Text>();
                text.text = toSet;
            }
        }
    }
}
