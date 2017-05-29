﻿using Assets.Scripts;
using Controller.Characters;
using Controller.Map;
using Generics.Hex;
using Generics.Scripts;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;

namespace Controller.Managers.Map
{
    public class CombatMapGuiController
    {
        private const string MAP_GUI_LAYER = "BackgroundTileGUI";
        private const string IMG = "ActingBoxImgTag";
        private const string NAME = "ActingBoxNameTag";
        private const string UI_LAYER = "UI";

        private const string ARMOR = "ArmorTextTag";
        private const string AP = "APTextTag";
        private const string HELM = "HelmTextTag";
        private const string HP = "HPTextTag";
        private const string L_WEAP = "LWeaponTextTag";
        private const string MORALE = "MoraleTextTag";
        private const string R_WEAP = "RWeaponTextTag";
        private const string STAM = "StaminaTextTag";

        private readonly Color RED = new Color(255, 0, 0, 150);
        private readonly Color WHITE = new Color(255, 255, 255, 255);

        private List<GameObject> _boxImages = new List<GameObject>();
        private List<GameObject> _decorateTileFamily = new List<GameObject>();
        private GameObject _singleTile;

        public void ClearDecoratedTiles()
        {
            foreach (var old in this._decorateTileFamily)
                GameObject.Destroy(old);
            this._decorateTileFamily.Clear();
        }

        public void SetActingBoxToController(GenericCharacterController c)
        {
            this.SetTagText(NAME, c.View.Name);
            this.SetTagText(AP, c.Model.CurrentAP + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.AP).ToString());
            this.SetTagText(HP, c.Model.CurrentHP + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.HP).ToString());
            this.SetTagText(STAM, c.Model.CurrentStamina + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Stamina).ToString());
            this.SetTagText(MORALE, c.Model.CurrentMorale + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Morale).ToString());
            this.SetTagText(STAM, c.Model.CurrentStamina + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Stamina).ToString());

            if (c.Model.LWeapon != null)
                this.SetTagText(L_WEAP, c.Model.LWeapon.Name);
            else
                this.SetTagText(L_WEAP, "");    
            
            if (c.Model.RWeapon != null)
                this.SetTagText(R_WEAP, c.Model.RWeapon.Name);
            else
                this.SetTagText(R_WEAP, "");

            this.SetBoxImg(IMG, c);
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
            this.ProcessTextToDisplay(e);   
            // TODO: Any effects that go with it...
        }

        private void DisplayText(string toDisplay, DisplayHitStatsEvent e, Color color, float yOffset = 0)
        {
            var canvas = GameObject.FindGameObjectWithTag("MainCanvas");
            var display = new GameObject();
            var text = display.AddComponent<Text>();
            var position = e.Hit.Target.transform.position;
            position.y += yOffset;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = color;
            text.fontSize = 20;
            text.rectTransform.position = position;
            text.rectTransform.SetParent(canvas.transform);
            text.rectTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            text.name = "Hit Text";
            text.text = toDisplay;
            Font fontToUse = Resources.Load("Fonts/8bitOperatorPlus8-Bold") as Font;
            text.font = fontToUse;
            var script = display.AddComponent<DestroyByLifetime>();
            script.lifetime = 3;
            var floating = display.AddComponent<FloatingText>();
            floating.Init(display);
        }

        private void DecorateFamilyOfTiles(TileController tile, Sprite deco)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = tile.Model.Center;
            renderer.sortingLayerName = MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = 0.50f;
            renderer.color = color;
            this._decorateTileFamily.Add(tView);
        }

        private void DecorateSingleTile(TileController t, Sprite deco)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = 0.50f;
            renderer.color = color;
            this._singleTile = tView;
        }

        private void ProcessTextToDisplay(DisplayHitStatsEvent e)
        {
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
                this.DisplayText("Dodge", e, WHITE, 0.35f);
            else if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
                this.DisplayText("Parry", e, WHITE, 0.35f);
            else
            {
                if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                    this.DisplayText("Block", e, WHITE, 0.45f);
                if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                    this.DisplayText("Critical!", e, RED, 0.35f);
                this.DisplayText(e.Hit.Dmg.ToString(), e, RED, 0.025f);
            }
        }

        private void SetBoxImg(string boxTag, GenericCharacterController c)
        {
            var box = GameObject.FindGameObjectWithTag(IMG);
            if (box != null)
            {
                foreach (var img in this._boxImages) { GameObject.Destroy(img); }
                this._boxImages.Clear();
                foreach(var h in c.SpriteHandlers)
                {
                    var parentPosition = h.transform.parent.position;
                    var xOffset = (h.transform.position.x - parentPosition.x) * 2.5f;
                    var yOffset = (h.transform.position.y - parentPosition.y) * 2.5f;
                    var renderer = h.GetComponent<SpriteRenderer>();
                    var tempImage = new GameObject();
                    var r = tempImage.AddComponent<SpriteRenderer>();
                    r.sprite = renderer.sprite;                    
                    r.sortingLayerName = (UI_LAYER + renderer.sortingLayerName);
                    var position = box.transform.position;
                    position.x += xOffset;
                    position.y += yOffset;
                    if (r.sortingLayerName != "UICharTorso")
                        r.transform.position = position;
                    else
                        r.transform.position = box.transform.position;
                    r.transform.localScale = new Vector3(2.5f, 2.5f);
                    r.name = "ActingImg";
                    r.transform.SetParent(box.transform);
                    this._boxImages.Add(tempImage);
                }
            }
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
