using Assets.Scripts;
using Controller.Characters;
using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;

namespace Controller.Managers.Map
{
    public class CombatMapGUIControllerHit
    {
        // TODO: Make this a static config item
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

        private const float WEAPON_PARRY = 4f;
        private const float WEAPON_OFFSET = 0.075f;

        private readonly Color RED = new Color(255, 0, 0, 150);
        private readonly Color WHITE = new Color(255, 255, 255, 255);

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            foreach (var particle in e.Killed.Particles)
                GameObject.Destroy(particle);
            var roll = RNG.Instance.NextDouble();
            e.Killed.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
            this.ProcessSplatterLevelFive(e);
        }

        public void ProcessMeleeHitGraphics(DisplayHitStatsEvent e)
        {
            var attackerJolt = e.Hit.Source.Handle.AddComponent<BoomerangScript>();
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, e.Hit.Source.CurrentTile.Model.Center, 0.85f);
            attackerJolt.Init(e.Hit.Source.Handle, position, 10f);

            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
                this.ProcessDodge(e);
            else if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
                this.ProcessParry(e);
            else if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                this.ProcessBlock(e);
            else
                this.ProcessNormalHit(e);
        }

        public void ProcessSplatter(DisplayHitStatsEvent e)
        {
            var dmgPercentage = e.Hit.Dmg / e.Hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
            if (dmgPercentage < 0.25 && !e.Hit.IsHeal)
                this.ProcessSplatterLevelOne(e);
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

        private Vector3 GetRandomDodgePosition(DisplayHitStatsEvent e)
        {
            var random = ListUtil<TileController>.GetRandomListElement(e.Hit.Target.CurrentTile.Adjacent);
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, random.Model.Center, 0.35f);
            return position;
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

        private void ProcessBlock(DisplayHitStatsEvent e)
        {
            this.DisplayText("Block", e, WHITE, 0.35f);
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                this.DisplayText("Critical!", e, RED, 0.40f);
            if (e.Hit.Target.Model.LWeapon != null && e.Hit.Target.Model.LWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharLWeapon"];
                var boomerang = weapon.AddComponent<BoomerangScript>();
                var position = weapon.transform.position;
                if (e.Hit.Target.LParty)
                    position.x -= WEAPON_OFFSET;
                else
                    position.x += WEAPON_OFFSET;
                boomerang.Init(weapon, position, WEAPON_PARRY, this.UnlockUserInteraction);
            }
            if (e.Hit.Target.Model.RWeapon != null && e.Hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharRWeapon"];
                var boomerang = weapon.AddComponent<BoomerangScript>();
                var position = weapon.transform.position;
                if (e.Hit.Target.LParty)
                    position.x -= WEAPON_OFFSET;
                else
                    position.x += WEAPON_OFFSET;
                boomerang.Init(weapon, position, WEAPON_PARRY, this.UnlockUserInteraction);
            }
            this.DisplayText(e.Hit.Dmg.ToString(), e, RED, 0.025f);
        }

        private void ProcessDodge(DisplayHitStatsEvent e)
        {
            var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
            defenderJolt.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e), 6f, this.UnlockUserInteraction);
            this.DisplayText("Dodge", e, WHITE, 0.30f);
        }

        private void ProcessNormalHit(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target.Model.CurrentHP - e.Hit.Dmg > 0)
            {
                var position = e.Hit.Target.transform.position;
                position.y -= 0.08f;
                var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
                defenderJolt.Init(e.Hit.Target.Handle, position, 10f, this.UnlockUserInteraction);
            }
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                this.DisplayText("Crit!", e, RED, 0.40f);
            this.DisplayText(e.Hit.Dmg.ToString(), e, RED, 0.025f);
        }

        private void ProcessParry(DisplayHitStatsEvent e)
        {
            this.DisplayText("Parry", e, WHITE, 0.30f);
            if (e.Hit.Target.Model.LWeapon != null && !e.Hit.Target.Model.LWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharLWeapon"];
                var position = weapon.transform.position;
                var boomerang = weapon.AddComponent<BoomerangScript>();
                if (e.Hit.Target.LParty)
                    position.x -= WEAPON_OFFSET;
                else
                    position.x += WEAPON_OFFSET;
                boomerang.Init(weapon, position, WEAPON_PARRY, this.UnlockUserInteraction);
            }
            if (e.Hit.Target.Model.RWeapon != null && !e.Hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharRWeapon"];
                var position = weapon.transform.position;
                var boomerang = weapon.AddComponent<BoomerangScript>();
                if (e.Hit.Target.LParty)
                    position.x -= WEAPON_OFFSET;
                else
                    position.x += WEAPON_OFFSET;
                boomerang.Init(weapon, position, WEAPON_PARRY, this.UnlockUserInteraction);
            }

            this.UnlockUserInteraction();
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

        private void UnlockUserInteraction()
        {
            CombatEventManager.Instance.UnlockInteraction();
        }
    }
}
