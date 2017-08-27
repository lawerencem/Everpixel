﻿using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Scripts;
using Assets.View.Fatality;
using Assets.View.Fatality.Magic;
using Assets.View.Fatality.Weapon;
using Assets.View.Script.FX;
using System.Collections.Generic;
using Template.CB;
using Template.Script;
using Template.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Controller.GUI
{
    public class VCombatController : ICallback
    {
        private List<Callback> _callbacks;

        private static VCombatController _instance;
        public static VCombatController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VCombatController();
                return _instance;
            }
        }

        public VCombatController()
        {
            this._callbacks = new List<Callback>();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DisplayNewAction(MAction a)
        {
            this._callbacks.Clear();
            switch(a.ActiveAbility.Data.CastType)
            {
                case (ECastType.Melee): { this.ProcessMeleeHitFX(a); } break;
            }
        }

        public void DisplayActionEventName(MAction a)
        {
            this.DisplayText(
                a.ActiveAbility.Type.ToString().Replace("_", " "),
                a.Data.Source.Handle,
                CombatGUIParams.WHITE, CombatGUIParams.ATTACK_TEXT_OFFSET);
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
            var script = display.AddComponent<SDestroyByLifetime>();
            script.lifetime = dur;
            var floating = display.AddComponent<SFloatingText>();
            floating.Init(display);
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        private bool FatalitySuccessful(MAction a)
        {
            var success = false;

            var data = new FatalityData();
            data.Target = a.Data.Target;
            var fatality = FatalityFactory.Instance.GetFatality(a);
            if (fatality != null)
            {
                switch (fatality.Type)
                {
                    case (EFatality.Crush): { fatality = fatality as CrushFatality; success = true; } break;
                    case (EFatality.Fighting): { fatality = fatality as FightingFatality; success = true; } break;
                    case (EFatality.Slash): { fatality = fatality as SlashFatality; success = true; } break;
                }
            }

            if (success)
                fatality.Init();
            return success;
        }

        private bool IsFatality(MAction a)
        {
            bool sucess = false;
            foreach (var hit in a.Data.Hits)
            {
                if (hit.Data.Target != null && hit.Data.Target.GetType().Equals(typeof(CharController)))
                {
                    var target = hit.Data.Target.Current as CharController;
                    if (target.Model.GetCurrentHP() - hit.Data.Dmg <= 0)
                    {
                        var roll = RNG.Instance.NextDouble();
                        if (roll < CombatGUIParams.FATALITY_CHANCE)
                        {
                            sucess = true;
                            //a.FatalityHits.Add(hit);
                        }
                    }
                }
            }
            return sucess;
        }

        private void ProcessDefenderFlinches(object o)
        {
            if (o.GetType().Equals(typeof(SAttackerJolt)))
            {
                var a = o as SAttackerJolt;
                if (a.Action != null)
                {
                    foreach (var hit in a.Action.Data.Hits)
                    {
                        if (hit.Data.Target.Current != null && 
                            hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                        {
                            var target = hit.Data.Target.Current as CharController;
                            var flinch = target.Handle.AddComponent<SFlinch>();
                            var flinchPos = target.Handle.transform.position;
                            flinchPos.y -= CombatGUIParams.FLINCH_DIST;
                            flinch.Init(target, flinchPos, CombatGUIParams.FLINCH_SPEED);
                        }
                    }
                }
            }
        }

        private void ProcessMeleeHitFX(MAction a)
        {
            this.DisplayActionEventName(a);
            if (this.IsFatality(a))
            {
                if (!this.FatalitySuccessful(a))
                    this.ProcessMeleeFXNonFatality(a);
            }
            else
                this.ProcessMeleeFXNonFatality(a);
        }

        private void ProcessMeleeFXNonFatality(MAction a)
        {
            var attack = a.Data.Source.Handle.AddComponent<SAttackerJolt>();
            var position = Vector3.Lerp(a.Data.Target.Model.Center, a.Data.Source.Tile.Model.Center, CombatGUIParams.ATTACK_LERP);
            attack.Action = a;
            attack.AddCallback(this.ProcessDefenderFlinches);
            attack.Init(a.Data.Source, position, CombatGUIParams.ATTACK_SPEED);
        }
    }

    //    public void ProcessBlock(DisplayHitStatsEvent e)
    //    {
    //        this.DisplayText("Block", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.BLOCK_TEXT_OFFSET);
    //        if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Critical))
    //            this.DisplayText("Critical!", e.Hit.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.CRIT_TEXT_OFFSET);
    //        if (e.Hit.Target.Model.LWeapon != null && e.Hit.Target.Model.LWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON];
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            var position = weapon.transform.position;
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //        if (e.Hit.Target.Model.RWeapon != null && e.Hit.Target.Model.RWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON];
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            var position = weapon.transform.position;
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //    }

    //    public void ProcessBulletFX(DisplayActionEvent e)
    //    {
    //        this.DisplayActionEventName(e);
    //        if (this.IsFatality(e))
    //        {
    //            if (!this.FatalitySuccessful(e))
    //                this.ProcessBulletFXNonFatality(e);
    //        }
    //        else
    //            this.ProcessBulletFXNonFatality(e);
    //    }


    //    public void ProcessCharacterKilled(CharController c)
    //    {
    //        if (!c.KillFXProcessed)
    //        {
    //            foreach (var particle in c.Particles)
    //                GameObject.Destroy(particle);
    //            this.ProcessCharacterKilledHelper(c);
    //            this.RandomRotate(c.Handle);
    //            this.ProcessSplatter(5, c.CurrentTile, 1);
    //        }
    //        c.KillFXProcessed = true;
    //    }

    //    public void ProcessDodge(DisplayHitStatsEvent e)
    //    {
    //        var defenderJolt = e.Hit.Target.Handle.AddComponent<BoomerangScript>();
    //        defenderJolt.Init(e.Hit.Target.Handle, this.GetRandomDodgePosition(e), 6f);
    //        this.DisplayText("Dodge", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
    //    }

    //    public void ProcessInjury(ApplyInjuryEvent e)
    //    {
    //        var text = e.Injury.Type.ToString().Replace("_", " ");
    //        this.DisplayText(text, e.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.INJURY_TEXT_OFFSET);
    //    }



    //    public void ProcessNormalHit(DisplayHitStatsEvent e)
    //    {
    //        if (e.Hit.Target.Model.GetCurrentHP() - e.Hit.Dmg > 0)
    //        {
    //            var position = e.Hit.Target.transform.position;
    //            position.y -= 0.08f;
    //            var defenderFlinch = e.Hit.Target.Handle.AddComponent<FlinchScript>();
    //            defenderFlinch.Init(e.Hit.Target, position, 8f);
    //        }
    //        if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Critical))
    //            this.DisplayText("Crit!", e.Hit.Target.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.DODGE_TEXT_OFFSET);
    //    }

    //    public void ProcessResist(DisplayHitStatsEvent e)
    //    {
    //        this.DisplayText("Resist", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.CRIT_TEXT_OFFSET);
    //    }

    //    public void ProcessSplatterOnHitEvent(DisplayHitStatsEvent e)
    //    {
    //        if (!FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Dodge) &&
    //            !FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Parry))
    //        {
    //            if (e.Hit.Target.Model.GetCurrentStatValue(ESecondaryStat.HP) > 0)
    //            {
    //                double dmg = (double)e.Hit.Dmg;
    //                double hp = (double)e.Hit.Target.Model.GetCurrentStatValue(ESecondaryStat.HP);
    //                double dmgPercentage = dmg / hp;
    //                if (dmgPercentage > 0.75 && !e.Hit.IsHeal)
    //                    this.ProcessSplatter(4, e.Hit.Target.CurrentTile);
    //                else if (dmgPercentage > 0.35 && !e.Hit.IsHeal)
    //                    this.ProcessSplatter(2, e.Hit.Target.CurrentTile);
    //                else if (dmgPercentage > 0.15 && !e.Hit.IsHeal)
    //                    this.ProcessSplatter(1, e.Hit.Target.CurrentTile);
    //            }
    //        }
    //    }

    //    public void ProcessSummon(DisplayHitStatsEvent e)
    //    {
    //        e.Done();
    //    }

    //    public void ProcessSplatter(int lvl, TileController t, float alpha = 1.0f)
    //    {
    //        var sprite = MapBridge.Instance.GetSplatterSprites(lvl);
    //        var splatter = new GameObject("Splatter");
    //        splatter.transform.SetParent(t.Handle.transform);
    //        var renderer = splatter.AddComponent<SpriteRenderer>();
    //        renderer.transform.position = t.Model.Center;
    //        renderer.sprite = sprite;
    //        this.RandomRotate(splatter);
    //        renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
    //        var color = renderer.color;
    //        color.a = alpha;
    //    }

    //    public void ProcessParry(DisplayHitStatsEvent e)
    //    {
    //        this.DisplayText("Parry", e.Hit.Target.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.PARRY_TEXT_OFFSET);
    //        if (e.Hit.Target.Model.LWeapon != null && !e.Hit.Target.Model.LWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON];
    //            var position = weapon.transform.position;
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //        if (e.Hit.Target.Model.RWeapon != null && !e.Hit.Target.Model.RWeapon.IsTypeOfShield())
    //        {
    //            var weapon = e.Hit.Target.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON];
    //            var position = weapon.transform.position;
    //            var boomerang = weapon.AddComponent<BoomerangScript>();
    //            if (e.Hit.Target.LParty)
    //                position.x -= CMapGUIControllerParams.WEAPON_OFFSET;
    //            else
    //                position.x += CMapGUIControllerParams.WEAPON_OFFSET;
    //            boomerang.Init(weapon, position, CMapGUIControllerParams.WEAPON_PARRY);
    //        }
    //    }

    //    public void ProcessZoneFX(DisplayActionEvent e)
    //    {
    //        this.DisplayActionEventName(e);
    //        if (this.IsFatality(e))
    //        {
    //            if (!this.FatalitySuccessful(e))
    //                this.ProcessZoneFXNonFatality(e);
    //        }
    //        else
    //            this.ProcessZoneFXNonFatality(e);
    //    }

    //    private Vector3 GetRandomDodgePosition(DisplayHitStatsEvent e)
    //    {
    //        var random = ListUtil<TileController>.GetRandomListElement(e.Hit.Target.CurrentTile.Adjacent);
    //        var position = Vector3.Lerp(e.Hit.Target.CurrentTile.Model.Center, random.Model.Center, 0.35f);
    //        return position;
    //    }

    //    private void AssignDeadLayer(GameObject o, string layer)
    //    {
    //        var renderer = o.GetComponent<SpriteRenderer>();
    //        if (renderer != null)
    //            renderer.sortingLayerName = layer;
    //    }

    //    private void ProcessCharacterKilledHelper(CharController c)
    //    {
    //        if (c.Model.Type == ECharacterType.Humanoid)
    //        {
    //            if (c.Model.Armor != null)
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_ARMOR], ViewParams.DEAD_ARMOR);
    //            if (c.Model.Helm != null)
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HELM], ViewParams.DEAD_HELM);
    //            if (c.Model.LWeapon != null)
    //            {
    //                this.RandomMoveKill(c.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON]);
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON], ViewParams.CHAR_L_WEAPON);
    //            }
    //            if (c.Model.RWeapon != null)
    //            {
    //                this.RandomMoveKill(c.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON]);
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON], ViewParams.DEAD_R_WEAPON);
    //            }
    //            var eyes = c.SpriteHandlerDict[ViewParams.CHAR_FACE].GetComponent<SpriteRenderer>();
    //            if (eyes.sprite != null)
    //                eyes.sprite = CharacterSpriteLoader.Instance.GetHumanoidDeadEyes(c.Model.Race);

    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_FACE))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_FACE], ViewParams.DEAD_FACE);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_HEAD_DECO_1))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_1], ViewParams.DEAD_HEAD_DECO_1);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_HEAD_DECO_2))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_2], ViewParams.DEAD_HEAD_DECO_2);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_TORSO_DECO_1))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_TORSO_DECO_1], ViewParams.DEAD_TORSO_DECO_1);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_TORSO_DECO_2))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_TORSO_DECO_2], ViewParams.DEAD_TORSO_DECO_2);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_HEAD))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_HEAD], ViewParams.DEAD_HEAD);
    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_TORSO))
    //                this.AssignDeadLayer(c.SpriteHandlerDict[ViewParams.CHAR_TORSO], ViewParams.DEAD_TORSO);

    //            if (c.SpriteHandlerDict.ContainsKey(ViewParams.CHAR_MOUNT))
    //            {
    //                // TODO: Mounts spawning on kill
    //                c.SpriteHandlerDict[ViewParams.CHAR_MOUNT].transform.SetParent(null);
    //                c.SpriteHandlerDict[ViewParams.CHAR_MOUNT].transform.position = c.CurrentTile.Model.Center;
    //            }
    //        }
    //    }



    //    private void ProcessBulletFXNonFatality(DisplayActionEvent e)
    //    {
    //        var attackerScript = e.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
    //        var position = Vector3.Lerp(e.EventController.Target.Model.Center, e.EventController.Source.CurrentTile.Model.Center, 0.85f);
    //        attackerScript.Init(e.EventController.Source, position, 8f);

    //        var sprite = AttackSpriteLoader.Instance.GetAttackSprite(e.EventController.Action);
    //        var bullet = new GameObject();
    //        var script = bullet.AddComponent<RaycastWithDeleteScript>();
    //        bullet.transform.position = e.EventController.Source.transform.position;
    //        var renderer = bullet.AddComponent<SpriteRenderer>();
    //        renderer.sprite = sprite;
    //        renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
    //        if (!e.EventController.Source.LParty)
    //            bullet.transform.localRotation = Quaternion.Euler(0, 180, 0);
    //        script.Init(bullet, e.EventController.Target.transform.position, 5f, e.AttackFXDone);
    //    }



    //    private void ProcessZoneFXNonFatality(DisplayActionEvent e)
    //    {
    //        var attackerScript = e.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
    //        var position = Vector3.Lerp(e.EventController.Target.Model.Center, e.EventController.Source.CurrentTile.Model.Center, 0.85f);
    //        attackerScript.Init(e.EventController.Source, position, 8f, e.AttackFXDone);

    //        var sprite = AttackSpriteLoader.Instance.GetAttackSprite(e.EventController.Action);
    //        var action = e.EventController.Action;
    //        foreach (var tile in e.EventController.Target.Model.GetAoETiles((int)action.AoE))
    //        {
    //            var tileDeco = new GameObject();
    //            var renderer = tileDeco.AddComponent<SpriteRenderer>();
    //            renderer.sprite = sprite;
    //            this.RandomRotate(tileDeco);
    //            tileDeco.transform.position = tile.Center;
    //            tileDeco.transform.SetParent(tile.Parent.transform);
    //            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
    //        }
    //    }

    //    private void RandomMoveKill(GameObject o)
    //    {
    //        this.RandomRotate(o);
    //        this.RandomTranslate(o);
    //    }

    //    private void RandomRotate(GameObject o)
    //    {
    //        var roll = RNG.Instance.NextDouble();
    //        o.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
    //    }

    //    private void RandomTranslate(GameObject o)
    //    {
    //        var x = RNG.Instance.Next(-75, 75) / 100;
    //        var y = RNG.Instance.Next(-75, 75) / 100;
    //        var position = o.transform.position;
    //        position.x += x;
    //        position.y += y;
    //        o.transform.position = position;
    //    }
    //}
}
