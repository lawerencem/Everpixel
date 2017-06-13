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
using View.Scripts;

namespace Controller.Managers.Map
{
    public class CMapGUIControllerHit
    {
        private DisplayHitStatsEvent _e;

        public void DisplayHitStatsEvent(HitInfo hit)
        {
            this.DisplayText(
                hit.Ability.TypeStr, 
                hit.Source.CurrentTile.Model.Center, 
                CMapGUIControllerParams.WHITE, CMapGUIControllerParams.ATTACK_TEXT_OFFSET);
        }

        public void ProcessBulletAttack(DisplayHitStatsEvent e)
        {
            this.ProcessBulletGraphics(e);
        }

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            foreach (var particle in e.Killed.Particles)
                GameObject.Destroy(particle);
            var roll = RNG.Instance.NextDouble();
            e.Killed.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
            this.ProcessSplatterLevelFive(e);
        }

        public void ProcessInjury(ApplyInjuryEvent e)
        {
            var text = e.Injury.Type.ToString().Replace("_", " ");
            this.DisplayText(text, e.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, 0.50f);
        }

        public void ProcessMeleeHitGraphics(DisplayHitStatsEvent e)
        {
            var attackerScript = e.Hit.Source.Handle.AddComponent<AttackScript>();
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, e.Hit.Source.CurrentTile.Model.Center, 0.85f);
            attackerScript.Init(e.Hit.Source, position, 8f, e.Done);

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
            if (!AttackEventFlags.HasFlag(AttackEventFlags.Flags.Dodge, e.Hit.Flags.CurFlags) &&
                !AttackEventFlags.HasFlag(AttackEventFlags.Flags.Parry, e.Hit.Flags.CurFlags))
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
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Tile Deco";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
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
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY, this.UnlockUserInteraction);
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
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY, this.UnlockUserInteraction);
            }
            this.DisplayText(e.Hit.Dmg.ToString(), e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.RED, CMapGUIControllerParams.DMG_TEXT_OFFSET);
        }

        private void ProcessBulletGraphics(DisplayHitStatsEvent e)
        {
            this._e = e;   
            var zoom = e.Hit.Source.Handle.AddComponent<DramaticZoom>();
            var position = e.Hit.Source.Handle.transform.position;
            position.y -= 0.35f;
            zoom.Init(position, 140f, 5f, 0.5f, this.ProcessFatality);
        }

        private void ProcessFatality()
        {
            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(this._e.Hit.Ability as GenericActiveAbility);
            var bullet = new GameObject();
            var script = bullet.AddComponent<RayCastWithDeleteScript>();
            bullet.transform.position = this._e.Hit.Source.transform.position;
            script.Init(bullet, this._e.Hit.Target.transform.position, 5f, this.ProcessExplosion);
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
        }

        private void ProcessExplosion()
        {
            var path = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                CMapGUIControllerParams.FIGHTING_FATALITY,
                CMapGUIControllerParams.PARTICLES_EXTENSION);
            var position = this._e.Hit.Target.transform.position;
            var boom = Resources.Load(path);
            var particles = GameObject.Instantiate(boom) as GameObject;
            particles.transform.position = position;
            particles.transform.SetParent(this._e.Hit.Target.Handle.transform);
            particles.name = CMapGUIControllerParams.FIGHTING_FATALITY + " Particles";
            var explosionPath = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                "FightingFatalityExplosion",
                CMapGUIControllerParams.PARTICLES_EXTENSION);
            var explosion = GameObject.Instantiate(Resources.Load(explosionPath)) as GameObject;
            explosion.transform.position = position;
            explosion.name = "BOOM";
            this._e.Hit.Target.Particles.Add(particles);
            this._e.Hit.Target.Particles.Add(explosion);
            this.ProcessGear();
        }

        private void ProcessGear()
        {
            var c = this._e.Hit.Target.Model;
            var sprite = MapBridge.Instance.GetSplatterSprites(5);
            foreach(var neighbor in this._e.Hit.Target.CurrentTile.Adjacent)
            {
                foreach(var outerNeighbor in neighbor.Adjacent)
                {
                    var spray = MapBridge.Instance.GetSplatterSprites(1);
                    this.PaintSingleTile(outerNeighbor, spray);
                }
                var blood = MapBridge.Instance.GetSplatterSprites(2);
                this.PaintSingleTile(neighbor, blood);
            }
            this.PaintSingleTile(this._e.Hit.Target.CurrentTile, sprite);
            if (c.Type == CharacterTypeEnum.Humanoid)
            {
                var renderer = c.ParentController.SpriteHandlerDict["CharTorso"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharDeco1"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharDeco2"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharDeco3"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharFace"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;

                if (c.Armor != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharArmor"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharArmor"], c.ParentController);
                }
                if (c.Helm != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharHelm"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharHelm"], c.ParentController);
                }
                if (c.LWeapon != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharLWeapon"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharLWeapon"], c.ParentController);
                }
                if (c.RWeapon != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharRWeapon"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharRWeapon"], c.ParentController);
                }
                
            }
        }

        private void ProcessDodge(DisplayHitStatsEvent e)
        {
            var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
            defenderJolt.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e), 6f, this.UnlockUserInteraction);
            this.DisplayText("Dodge", e.Hit.Target.CurrentTile.Model.Center, CMapGUIControllerParams.WHITE, 0.30f);
        }

        private void ProcessNormalHit(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target.Model.CurrentHP - e.Hit.Dmg > 0)
            {
                var position = e.Hit.Target.transform.position;
                position.y -= 0.08f;
                var defenderFlinch = e.Hit.Target.Handle.AddComponent<FlinchScript>();
                defenderFlinch.Init(e.Hit.Target, position, 8f, this.UnlockUserInteraction);
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
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY, this.UnlockUserInteraction);
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
                boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY, this.UnlockUserInteraction);
            }

            this.UnlockUserInteraction();
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

        private void UnlockUserInteraction()
        {
            CombatEventManager.Instance.UnlockInteraction();
        }
    }
}
