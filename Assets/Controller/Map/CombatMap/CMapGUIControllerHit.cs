using Assets.Scripts;
using Assets.View.Characters;
using Controller.Characters;
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
        public void DisplayActionEventName(DisplayActionEvent e)
        {
            this.DisplayText(
                e.EventController.Action.Type.ToString().Replace("_", " "), 
                e.EventController.Source.Handle, 
                CMapGUIControllerParams.WHITE, CMapGUIControllerParams.ATTACK_TEXT_OFFSET);
        }

        public void ProcessBlock(DisplayHitStatsEvent e)
        {
            this.DisplayText("Block", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.BLOCK_TEXT_OFFSET);
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                this.DisplayText("Critical!", e.Hit.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.CRIT_TEXT_OFFSET);
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
        }

        public void DisplayText(string toDisplay, GameObject toShow, Color color, float yOffset = 0, float dur = 1)
        {
            var parent = GameObject.FindGameObjectWithTag("WorldSpaceCanvas");
            var display = new GameObject();
            var text = display.AddComponent<Text>();
            var position = toShow.transform.position;
            position.y += yOffset;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = color;
            text.fontSize = 16;
            text.rectTransform.SetParent(parent.transform);
            text.rectTransform.position = position;
            text.rectTransform.sizeDelta = new Vector2(200f, 200f);
            text.rectTransform.localScale = new Vector3(0.0075f, 0.0075f);
            text.name = "Hit Text";
            text.text = toDisplay;
            Font fontToUse = Resources.Load("Fonts/8bitOperatorPlus8-Bold") as Font;
            text.font = fontToUse;
            var script = display.AddComponent<DestroyByLifetime>();
            script.lifetime = dur;
            var floating = display.AddComponent<FloatingText>();
            floating.Init(display);
        }

        public void ProcessBulletFX(DisplayActionEvent e)
        {
            this.DisplayActionEventName(e);
            if (this.IsFatality(e))
            {
                if (!this.FatalitySuccessful(e))
                    this.ProcessBulletFXNonFatality(e);
            }
            else
                this.ProcessBulletFXNonFatality(e);
        }


        public void ProcessCharacterKilled(GenericCharacterController c)
        {
            if (!c.KillFXProcessed)
            {
                foreach (var particle in c.Particles)
                    GameObject.Destroy(particle);
                this.ProcessCharacterKilledHelper(c);
                this.RandomRotate(c.Handle);
                this.ProcessSplatter(5, c.CurrentTile, 1);
            }
            c.KillFXProcessed = true;
        }

        public void ProcessDodge(DisplayHitStatsEvent e)
        {
            var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
            defenderJolt.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e), 6f);
            this.DisplayText("Dodge", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
        }

        public void ProcessInjury(ApplyInjuryEvent e)
        {
            var text = e.Injury.Type.ToString().Replace("_", " ");
            this.DisplayText(text, e.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.INJURY_TEXT_OFFSET);
        }

        public void ProcessMeleeHitFX(DisplayActionEvent e)
        {
            this.DisplayActionEventName(e);
            if (this.IsFatality(e))
            {
                if (!this.FatalitySuccessful(e))
                    this.ProcessMeleeHitFXNonFatality(e);
            }
            else
                this.ProcessMeleeHitFXNonFatality(e);
        }

        public void ProcessNormalHit(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target.Model.CurrentHP - e.Hit.Dmg > 0)
            {
                var position = e.Hit.Target.transform.position;
                position.y -= 0.08f;
                var defenderFlinch = e.Hit.Target.Handle.AddComponent<FlinchScript>();
                defenderFlinch.Init(e.Hit.Target, position, 8f);
            }
            if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                this.DisplayText("Crit!", e.Hit.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
        }

        public void ProcessSplatterOnHitEvent(DisplayHitStatsEvent e)
        {
            if (!AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge) &&
                !AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
            {
                if (e.Hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP) > 0)
                {
                    double dmg = (double)e.Hit.Dmg;
                    double hp = (double)e.Hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
                    double dmgPercentage = dmg / hp;
                    if (dmgPercentage > 0.75 && !e.Hit.IsHeal)
                        this.ProcessSplatter(4, e.Hit.Target.CurrentTile);
                    else if (dmgPercentage > 0.35 && !e.Hit.IsHeal)
                        this.ProcessSplatter(2, e.Hit.Target.CurrentTile);
                    else if (dmgPercentage > 0.15 && !e.Hit.IsHeal)
                        this.ProcessSplatter(1, e.Hit.Target.CurrentTile);
                }
            }
        }

        public void ProcessSummon(DisplayHitStatsEvent e)
        {
            e.Done();
        }

        public void ProcessSplatter(int lvl, TileController t, float alpha = 1.0f)
        {
            var sprite = MapBridge.Instance.GetSplatterSprites(lvl);
            var splatter = new GameObject("Splatter");
            splatter.transform.SetParent(t.Handle.transform);
            var renderer = splatter.AddComponent<SpriteRenderer>();
            renderer.transform.position = t.Model.Center;
            renderer.sprite = sprite;
            this.RandomRotate(splatter);
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            var color = renderer.color;
            color.a = alpha;
        }

        public void ProcessParry(DisplayHitStatsEvent e)
        {
            this.DisplayText("Parry", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.PARRY_TEXT_OFFSET);
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

        public void ProcessZoneFX(DisplayActionEvent e)
        {
            this.DisplayActionEventName(e);
            if (this.IsFatality(e))
            {
                if (!this.FatalitySuccessful(e))
                    this.ProcessZoneFXNonFatality(e);
            }
            else
                this.ProcessZoneFXNonFatality(e);
        }

        private bool FatalitySuccessful(DisplayActionEvent e)
        {
            var success = false;

            var cast = new GenericFatality(FatalityEnum.None, this, e);
            var fatality = FatalityFactory.Instance.GetFatality(this, e);
            if (fatality != null)
            {
                switch (fatality.Type)
                {
                    case (FatalityEnum.Crush): { cast = fatality as CrushFatality; success = true; } break;
                    case (FatalityEnum.Fighting): { cast = fatality as FightingFatality; success = true; } break;
                    case (FatalityEnum.Slash): { cast = fatality as SlashFatality; success = true; } break;
                }
            }

            if (success)
                fatality.Init();
            foreach (var hit in e.FatalityHits)
                hit.FXProcessed = true;
            return success;
        }

        private Vector3 GetRandomDodgePosition(DisplayHitStatsEvent e)
        {
            var random = ListUtil<TileController>.GetRandomListElement(e.Hit.Target.CurrentTile.Adjacent);
            var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, random.Model.Center, 0.35f);
            return position;
        }

        private void AssignDeadLayer(GameObject o, string layer)
        {
            var renderer = o.GetComponent<SpriteRenderer>();
            if (renderer != null)
                renderer.sortingLayerName = layer;
        }

        private void ProcessCharacterKilledHelper(GenericCharacterController c)
        {
            if (c.Model.Type == CharacterTypeEnum.Humanoid)
            {
                if (c.Model.Armor != null)
                    this.AssignDeadLayer(c.SpriteHandlerDict["CharArmor"], "DeadArmor");
                if (c.Model.Helm != null)
                    this.AssignDeadLayer(c.SpriteHandlerDict["CharHelm"], "DeadHelm");
                if (c.Model.LWeapon != null)
                {
                    this.RandomMoveKill(c.SpriteHandlerDict["CharLWeapon"]);
                    this.AssignDeadLayer(c.SpriteHandlerDict["CharLWeapon"], "DeadLWeapon");
                }
                if (c.Model.RWeapon != null)
                {
                    this.RandomMoveKill(c.SpriteHandlerDict["CharRWeapon"]);
                    this.AssignDeadLayer(c.SpriteHandlerDict["CharRWeapon"], "DeadRWeapon");
                }       
                var eyes = c.SpriteHandlerDict["CharFace"].GetComponent<SpriteRenderer>();
                if (eyes.sprite != null)
                    eyes.sprite = CharacterSpriteLoader.Instance.GetHumanoidDeadEyes(c.Model.Race);

                this.AssignDeadLayer(c.SpriteHandlerDict["CharFace"], "DeadFace");
                this.AssignDeadLayer(c.SpriteHandlerDict["CharDeco1"], "DeadDeco1");
                this.AssignDeadLayer(c.SpriteHandlerDict["CharDeco2"], "DeadDeco2");
                this.AssignDeadLayer(c.SpriteHandlerDict["CharDeco3"], "DeadDeco3");
                this.AssignDeadLayer(c.SpriteHandlerDict["CharDeco4"], "DeadDeco4");
                this.AssignDeadLayer(c.SpriteHandlerDict["CharHead"], "DeadHead");
                this.AssignDeadLayer(c.SpriteHandlerDict["CharTorso"], "DeadTorso");

                if (c.SpriteHandlerDict.ContainsKey("CharMount"))
                {
                    // TODO: Mounts spawning on kill
                    c.SpriteHandlerDict["CharMount"].transform.SetParent(null);
                    c.SpriteHandlerDict["CharMount"].transform.position = c.CurrentTile.Model.Center;
                }
            }
        }

        private bool IsFatality(DisplayActionEvent e)
        {
            bool sucess = false;
            foreach(var hit in e.EventController.Hits)
            {
                if (hit.Target != null && hit.Target.Model != null)
                {
                    if (hit.Target.Model.CurrentHP - hit.Dmg <= 0)
                    {
                        var roll = RNG.Instance.NextDouble();
                        if (roll < CMapGUIControllerParams.FATALITY_CHANCE)
                        {
                            sucess = true;
                            e.FatalityHits.Add(hit);
                        }
                    }
                }
            }
            return sucess;
        }

        private void ProcessBulletFXNonFatality(DisplayActionEvent e)
        {
            var attackerScript = e.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
            var position = Vector3.Lerp(e.EventController.Target.Model.Center, e.EventController.Source.CurrentTile.Model.Center, 0.85f);
            attackerScript.Init(e.EventController.Source, position, 8f);

            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(e.EventController.Action);
            var bullet = new GameObject();
            var script = bullet.AddComponent<RaycastWithDeleteScript>();
            bullet.transform.position = e.EventController.Source.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
            if (!e.EventController.Source.LParty)
                bullet.transform.localRotation = Quaternion.Euler(0, 180, 0);
            script.Init(bullet, e.EventController.Target.transform.position, 5f, e.AttackFXDone);
        }

        private void ProcessMeleeHitFXNonFatality(DisplayActionEvent e)
        {
            var attackerScript = e.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
            var position = Vector3.Lerp(e.EventController.Target.Model.Center, e.EventController.Source.CurrentTile.Model.Center, 0.85f);
            foreach (var hit in e.EventController.Hits)
            {
                var listener = new DefenderFXListener(this, hit);
                listener.ProcessDefenderGraphics();
            }
            attackerScript.Init(e.EventController.Source, position, 8f, e.AttackFXDone);
        }

        private void ProcessZoneFXNonFatality(DisplayActionEvent e)
        {
            var attackerScript = e.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
            var position = Vector3.Lerp(e.EventController.Target.Model.Center, e.EventController.Source.CurrentTile.Model.Center, 0.85f);
            attackerScript.Init(e.EventController.Source, position, 8f, e.AttackFXDone);

            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(e.EventController.Action);
            var action = e.EventController.Action;
            foreach(var tile in e.EventController.Target.Model.GetAoETiles((int)action.AoE))
            {
                var tileDeco = new GameObject();
                var renderer = tileDeco.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                this.RandomRotate(tileDeco);
                tileDeco.transform.position = tile.Center;
                tileDeco.transform.SetParent(tile.Parent.transform);
                renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
            }
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
            var x = RNG.Instance.Next(-75, 75) / 100;
            var y = RNG.Instance.Next(-75, 75) / 100;
            var position = o.transform.position;
            position.x += x;
            position.y += y;
            o.transform.position = position;
        }
    }
}
