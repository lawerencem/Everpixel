using Assets.Scripts;
using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;
using View.Characters;
using View.Fatalities;
using View.Scripts;

namespace Controller.Managers.Map
{
    public class CMapGUIControllerHit
    {
        private class DefenderFXListener
        {
            private CMapGUIControllerHit _parent;
            private DisplayHitStatsEvent _event;

            public DefenderFXListener(CMapGUIControllerHit parent, DisplayHitStatsEvent e)
            {
                this._parent = parent;
                this._event = e;
            }

            public void ProcessDefenderGraphics()
            {
                if (AttackEventFlags.HasFlag(this._event.Hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
                    this._parent.ProcessDodge(this._event);
                else if (AttackEventFlags.HasFlag(this._event.Hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
                    this._parent.ProcessParry(this._event);
                else if (AttackEventFlags.HasFlag(this._event.Hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                    this._parent.ProcessBlock(this._event);
                else
                    this._parent.ProcessNormalHit(this._event);

                this._parent.ProcessSplatter(this._event);
                this._event.Done();
            }
        }

        public void DisplayHitStatsEvent(HitInfo hit)
        {
            this.DisplayText(
                hit.Ability.TypeStr, 
                hit.Source.CurrentTile.Model.Center, 
                CMapGUIControllerParams.WHITE, CMapGUIControllerParams.ATTACK_TEXT_OFFSET);
        }

        public void PaintSingleTile(TileController t, Sprite deco, float alpha = 1.0f)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Tile Deco";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }

        public void ProcessBulletGraphics(DisplayHitStatsEvent e)
        {
            this.DisplayHitStatsEvent(e.Hit);

            if (this.IsFatality(e))
            {
                if (!this.FatalitySuccessful(e))
                    this.ProcessBulletAttackNonFatality(e);
            }
            else
                this.ProcessBulletAttackNonFatality(e);
        }

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            foreach (var particle in e.Killed.Particles)
                GameObject.Destroy(particle);
            if (e.Killed.Model.Type == CharacterTypeEnum.Humanoid)
            {
                if (e.Killed.Model.LWeapon != null)
                    this.RandomMoveKill(e.Killed.SpriteHandlerDict["CharLWeapon"]);
                if (e.Killed.Model.RWeapon != null)
                    this.RandomMoveKill(e.Killed.SpriteHandlerDict["CharRWeapon"]);
                var eyes = e.Killed.SpriteHandlerDict["CharFace"].GetComponent<SpriteRenderer>();
                eyes.sprite = CharacterSpriteLoader.Instance.GetHumanoidDeadEyes(e.Killed.Model.Race);
            }
            this.RandomRotate(e.Killed.Handle);
            this.ProcessSplatterLevelFive(e);
        }

        public void ProcessInjury(ApplyInjuryEvent e)
        {
            var text = e.Injury.Type.ToString().Replace("_", " ");
            this.DisplayText(text, e.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, 0.50f);
        }

        public void ProcessMeleeHitGraphics(DisplayHitStatsEvent e)
        {
            this.DisplayHitStatsEvent(e.Hit);

            if (this.IsFatality(e))
            {
                if (!this.FatalitySuccessful(e))
                    this.ProcessMeleeHitGraphicsNonFatality(e);
            }
            else
                this.ProcessMeleeHitGraphicsNonFatality(e);
        }

        public void ProcessSplatter(DisplayHitStatsEvent e)
        {
            if (!AttackEventFlags.HasFlag(AttackEventFlags.Flags.Dodge, e.Hit.Flags.CurFlags) &&
                !AttackEventFlags.HasFlag(AttackEventFlags.Flags.Parry, e.Hit.Flags.CurFlags))
            {
                if (e.Hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP) > 0)
                {
                    var dmgPercentage = e.Hit.Dmg / e.Hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
                    if (dmgPercentage > 0.75 && !e.Hit.IsHeal)
                        this.ProcessSplatterHelper(4, e);
                    else if (dmgPercentage > 0.35 && !e.Hit.IsHeal)
                        this.ProcessSplatterHelper(2, e);
                    else if (dmgPercentage > 0.15 && !e.Hit.IsHeal)
                        this.ProcessSplatterHelper(1, e);
                }   
            }
        }

        private void ProcessBulletAttackNonFatality(DisplayHitStatsEvent e)
        {
            var attackerScript = e.Hit.Source.Handle.AddComponent<AttackerJoltScript>();
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, e.Hit.Source.CurrentTile.Model.Center, 0.85f);
            attackerScript.Init(e.Hit.Source, position, 8f);

            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(e.Hit.Ability as GenericActiveAbility);
            var bullet = new GameObject();
            var script = bullet.AddComponent<RaycastWithDeleteScript>();
            bullet.transform.position = e.Hit.Source.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
            var listener = new DefenderFXListener(this, e);
            script.Init(bullet, e.Hit.Target.transform.position, 5f, listener.ProcessDefenderGraphics);
        }

        private void DisplayText(string toDisplay, Vector3 pos, Color color, float yOffset = 0)
        {
            var canvas = GameObject.FindGameObjectWithTag("MainCanvas");
            var display = new GameObject();
            var text = display.AddComponent<Text>();
            var position = pos;
            position.y += yOffset;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = color;
            text.fontSize = 16;
            text.rectTransform.position = position;
            text.rectTransform.SetParent(canvas.transform);
            text.rectTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            text.rectTransform.sizeDelta = new Vector2(150f, 150f);
            text.name = "Hit Text";
            text.text = toDisplay;
            Font fontToUse = Resources.Load("Fonts/8bitOperatorPlus8-Bold") as Font;
            text.font = fontToUse;
            var zoom = Camera.main.fieldOfView;
            var scalar = 30f / zoom;
            text.transform.localScale = new Vector3(scalar, scalar);
            var script = display.AddComponent<DestroyByLifetime>();
            script.lifetime = 1;
            var floating = display.AddComponent<FloatingText>();
            floating.Init(display);
        }

        private bool FatalitySuccessful(DisplayHitStatsEvent e)
        {
            var success = false;

            var cast = new GenericFatality(FatalityEnum.None, this, e);
            var fatality = FatalityFactory.Instance.GetFatality(this, e);
            if (fatality != null)
            {
                switch (fatality.Type)
                {
                    case (FatalityEnum.Fighting): { cast = fatality as FightingFatality; success = true; } break;
                }
            }

            if (success)
            {
                fatality.Init();
                foreach (var particle in e.Hit.Target.Particles)
                    GameObject.Destroy(particle);
            }
                

            return success;
        }

        private Vector3 GetRandomDodgePosition(DisplayHitStatsEvent e)
        {
            var random = ListUtil<TileController>.GetRandomListElement(e.Hit.Target.CurrentTile.Adjacent);
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, random.Model.Center, 0.35f);
            return position;
        }

        private void RandomMoveKill(GameObject o)
        {
            this.RandomRotate(o);
            this.RandomTranslate(o);
        }
        
        private void RandomRotate(GameObject o)
        {
            var roll = RNG.Instance.NextDouble();
            o.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
        }

        private void RandomTranslate(GameObject o)
        {
            var x = RNG.Instance.Next(-15, 15) / 100;
            var y = RNG.Instance.Next(-15, 15) / 100;
            var position = o.transform.position;
            position.x += x;
            position.y += y;
            o.transform.position = position;
        }

        private bool IsFatality(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target.Model.CurrentHP - e.Hit.Dmg <= 0)
            {
                var roll = RNG.Instance.NextDouble();
                if (roll < CMapGUIControllerParams.FATALITY_CHANCE)
                    return true;
            }
            return false;
        }

        private void ProcessBlock(DisplayHitStatsEvent e)
        {
            this.DisplayText("Block", e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.BLOCK_TEXT_OFFSET);
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                this.DisplayText("Critical!", e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, CMapGUIControllerParams.CRIT_TEXT_OFFSET);
            if (e.Hit.Target.Model.LWeapon != null && e.Hit.Target.Model.LWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharLWeapon"];
                var boomerang = weapon.AddComponent<BoomerangScript>();
                var position = weapon.transform.position;
                if (e.Hit.Target.LParty)
                    position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
                else
                    position.x += CMapGUIControllerParams.WEAPON_OFFSET;
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
            }
            if (e.Hit.Target.Model.RWeapon != null && e.Hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharRWeapon"];
                var boomerang = weapon.AddComponent<BoomerangScript>();
                var position = weapon.transform.position;
                if (e.Hit.Target.LParty)
                    position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
                else
                    position.x += CMapGUIControllerParams.WEAPON_OFFSET;
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
            }
            this.DisplayText(e.Hit.Dmg.ToString(), e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, CMapGUIControllerParams.DMG_TEXT_OFFSET);
        }

        private void ProcessDodge(DisplayHitStatsEvent e)
        {
            var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
            defenderJolt.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e), 6f);
            this.DisplayText("Dodge", e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.WHITE, 0.30f);
        }

        public void ProcessMeleeHitGraphicsNonFatality(DisplayHitStatsEvent e)
        {
            var attackerScript = e.Hit.Source.Handle.AddComponent<AttackerJoltScript>();
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, e.Hit.Source.CurrentTile.Model.Center, 0.85f);
            var listener = new DefenderFXListener(this, e);
            attackerScript.Init(e.Hit.Source, position, 8f);
            listener.ProcessDefenderGraphics();
        }

        private void ProcessNormalHit(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target.Model.CurrentHP - e.Hit.Dmg > 0)
            {
                var position = e.Hit.Target.transform.position;
                position.y -= 0.08f;
                var defenderFlinch = e.Hit.Target.Handle.AddComponent<FlinchScript>();
                defenderFlinch.Init(e.Hit.Target, position, 8f);
            }
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                this.DisplayText("Crit!", e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
            this.DisplayText(e.Hit.Dmg.ToString(), e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, CMapGUIControllerParams.DMG_TEXT_OFFSET);
        }

        private void ProcessParry(DisplayHitStatsEvent e)
        {
            this.DisplayText("Parry", e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.PARRY_TEXT_OFFSET);
            if (e.Hit.Target.Model.LWeapon != null && !e.Hit.Target.Model.LWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharLWeapon"];
                var position = weapon.transform.position;
                var boomerang = weapon.AddComponent<BoomerangScript>();
                if (e.Hit.Target.LParty)
                    position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
                else
                    position.x += CMapGUIControllerParams.WEAPON_OFFSET;
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
            }
            if (e.Hit.Target.Model.RWeapon != null && !e.Hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                var weapon = e.Hit.Target.SpriteHandlerDict["CharRWeapon"];
                var position = weapon.transform.position;
                var boomerang = weapon.AddComponent<BoomerangScript>();
                if (e.Hit.Target.LParty)
                    position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
                else
                    position.x += CMapGUIControllerParams.WEAPON_OFFSET;
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
            }
        }

        private void ProcessSplatterHelper(int lvl, DisplayHitStatsEvent e)
        {
            var sprite = MapBridge.Instance.GetSplatterSprites(lvl);
            this.PaintSingleTile(e.Hit.Target.CurrentTile, sprite);
        }

        private void ProcessSplatterLevelFive(CharacterKilledEvent e)
        {
            var sprite = MapBridge.Instance.GetSplatterSprites(5);
            this.PaintSingleTile(e.Killed.CurrentTile, sprite);
        }
    }
}
