using Assets.Scripts;
using Controller.Characters;
using Controller.Map;
using Generics.Hex;
using Generics.Scripts;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;
using View.Scripts;

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

            if (c.Model.Armor != null)
                this.SetTagText(ARMOR, c.Model.Armor.Name);
            else
                this.SetTagText(ARMOR, "");
            if (c.Model.Helm != null)
                this.SetTagText(HELM, c.Model.Helm.Name);
            else
                this.SetTagText(HELM, "");
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
            this.ProcessHitGraphics(e);
            this.ProcessSplatter(e);
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

        private void DecorateSingleTile(TileController t, Sprite deco, float alpha = 0.50f)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
            this._singleTile = tView;
        }

        private void PaintSingleTile(TileController t, Sprite deco, float alpha = 1.0f)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = MAP_GUI_LAYER;
            tView.name = "Tile Deco";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            var roll = RNG.Instance.NextDouble();
            e.Killed.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
            this.ProcessSplatterLevelFive(e);
        }

        private void ProcessSplatter(DisplayHitStatsEvent e)
        {
            var dmgPercentage = e.Hit.Dmg / e.Hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
            if (dmgPercentage < 0.25 && !e.Hit.IsHeal)
                this.ProcessSplatterLevelOne(e);
        }

        private void ProcessSplatterLevelOne(DisplayHitStatsEvent e)
        {
            var sprite = MapBridge.Instance.GetSplatterLevelOneSprite();
            this.PaintSingleTile(e.Hit.Target.CurrentTile, sprite);
        }

        private void ProcessSplatterLevelFour(DisplayHitStatsEvent e)
        {
            var sprite = MapBridge.Instance.GetSplatterLevelFourSprite();
            this.PaintSingleTile(e.Hit.Target.CurrentTile, sprite);
        }

        private void ProcessSplatterLevelFive(CharacterKilledEvent e)
        {
            var sprite = MapBridge.Instance.GetSplatterLevelFiveSprite();
            this.PaintSingleTile(e.Killed.CurrentTile, sprite);
        }

        private void ProcessHitGraphics(DisplayHitStatsEvent e)
        {
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
            {
                var script = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
                script.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e));
            }
            else if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
                this.DisplayText("Parry", e, WHITE, 0.30f);
            else
            {
                if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                    this.DisplayText("Block", e, WHITE, 0.35f);
                if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                    this.DisplayText("Critical!", e, RED, 0.40f);
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
                foreach(var spriteHandler in c.SpriteHandlerDict.Values)
                {
                    var parentPosition = spriteHandler.transform.parent.position;
                    var xOffset = (spriteHandler.transform.position.x - parentPosition.x) * 2.5f;
                    var yOffset = (spriteHandler.transform.position.y - parentPosition.y) * 2.5f;
                    var renderer = spriteHandler.GetComponent<SpriteRenderer>();
                    var tempImage = new GameObject();
                    var r = tempImage.AddComponent<SpriteRenderer>();
                    r.sprite = renderer.sprite;                    
                    r.sortingLayerName = (UI_LAYER + renderer.sortingLayerName);
                    var position = box.transform.position;
                    position.x += xOffset;
                    position.y += yOffset;
                    if (r.sortingLayerName == "UICharMount")
                    {
                        var mountPos = position;
                        mountPos.x += 0.05f;
                        mountPos.y -= 0.15f;
                        r.transform.position = mountPos;
                    }
                    else if (r.sortingLayerName != "UICharTorso")
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

        private Vector3 GetRandomDodgePosition(DisplayHitStatsEvent e)
        {
            var random = ListUtil<TileController>.GetRandomListElement(e.Hit.Target.CurrentTile.Adjacent);
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, random.Model.Center, 0.35f);
            return position;
        }
    }
}
