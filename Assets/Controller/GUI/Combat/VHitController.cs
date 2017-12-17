using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Music;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View;
using Assets.View.Ability;
using Assets.View.Event;
using Assets.View.Particle;
using Assets.View.Script.FX;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.GUI.Combat
{
    public class VHitController : ICallback
    {
        private List<Callback> _callbacks;

        private static VHitController _instance;
        public static VHitController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VHitController();
                return _instance;
            }
        }

        public VHitController()
        {
            this._callbacks = new List<Callback>();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void ProcessBulletFX(MAction a)
        {
            VCombatController.Instance.DisplayActionEventName(a);
            if (VFatalityController.Instance.IsFatality(a))
            {
                if (!VFatalityController.Instance.FatalitySuccessful(a))
                    this.ProcessBulletFXNonFatality(a);
            }
            else
                this.ProcessBulletFXNonFatality(a);
        }

        public void ProcessCustomFX(MAction a)
        {
            VCombatController.Instance.DisplayActionEventName(a);
            a.ActiveAbility.DisplayFX(a);
        }

        public void ProcessDefenderHit(MHit hit)
        {
            if (hit.Data.Target.Current != null &&
                hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var target = hit.Data.Target.Current as CChar;
                this.ProcessDefenderHitsHelper(target, hit);
            }
        }

        public void ProcessMeleeHitFX(MAction a)
        {
            VCombatController.Instance.DisplayActionEventName(a);
            if (VFatalityController.Instance.IsFatality(a))
            {
                if (!VFatalityController.Instance.FatalitySuccessful(a))
                    this.ProcessMeleeFXNonFatality(a);
            }
            else
                this.ProcessMeleeFXNonFatality(a);
        }

        public void ProcessSingleHitFX(MAction a)
        {
            VCombatController.Instance.DisplayActionEventName(a);
            if (VFatalityController.Instance.IsFatality(a))
            {
                if (!VFatalityController.Instance.FatalitySuccessful(a))
                    this.ProcessSingleFXNonFatality(a);
            }
            else
                this.ProcessSingleFXNonFatality(a);
        }

        public void ProcessSongFX(MAction a)
        {
            var ability = a.ActiveAbility as MSong;
            VCombatController.Instance.DisplayActionEventName(a);
            var path = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                ability.SongType.ToString().Replace("_", ""),
                CombatGUIParams.PARTICLES_EXTENSION);
            var particles = ParticleController.Instance.CreateParticle(path);
            var script = particles.AddComponent<SDestroyByLifetime>();
            script.Init(particles, 5f);
            ParticleController.Instance.AttachParticle(a.Data.Source.Handle, particles);
            foreach (var hit in a.Data.Hits)
                hit.CallbackHandler(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        private void DisplayBlock(CChar target, MHit hit)
        {
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = hit;
            data.Priority = ViewParams.PARRY_PRIORITY;
            data.Target = target.Handle;
            data.Text = "Block";
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Hit.AddDataDisplay(data);

            if (target.Proxy.GetRWeapon() != null && target.Proxy.GetRWeapon().IsTypeOfShield())
            {
                var wpn = target.SubComponents[Layers.CHAR_R_WEAPON];
                this.DisplayParryAndBlockHelper(target, hit, wpn);
            }
            else if (target.Proxy.GetLWeapon() != null && target.Proxy.GetLWeapon().IsTypeOfShield())
            {
                var wpn = target.SubComponents[Layers.CHAR_L_WEAPON];
                this.DisplayParryAndBlockHelper(target, hit, wpn);
            }
        }

        private void DisplayDodge(CChar target, MHit hit)
        {
            var dodge = target.Handle.AddComponent<SBoomerang>();
            var dodgeTgt = ListUtil<CTile>.GetRandomElement(target.Tile.GetAdjacent());
            var position = Vector3.Lerp(target.Handle.transform.position, dodgeTgt.Model.Center, CombatGUIParams.DODGE_LERP);
            position = RandomPositionOffset.RandomOffset(position, CombatGUIParams.DEFAULT_OFFSET);
            dodge.AddCallback(hit.CallbackHandler);
            dodge.Init(target.Handle, position, CombatGUIParams.DODGE_SPEED);
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = hit;
            data.Priority = ViewParams.DODGE_PRIORITY;
            data.Text = "Dodge";
            data.Target = target.Handle;
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Hit.AddDataDisplay(data);
        }

        private void DisplayFlinch(CChar target, MHit hit)
        {
            var dmgData = new HitDisplayData();
            dmgData.Color = CombatGUIParams.RED;
            dmgData.Hit = hit;
            dmgData.Priority = ViewParams.DMG_PRIORITY;
            dmgData.Text = hit.Data.Dmg.ToString();
            dmgData.Target = target.Handle;
            dmgData.YOffset = CombatGUIParams.FLOAT_OFFSET;
            dmgData.Hit.AddDataDisplay(dmgData);

            if (hit.Data.Dmg < target.Proxy.GetPoints(ESecondaryStat.HP)) 
            {
                var flinch = target.Handle.AddComponent<SFlinch>();
                var flinchPos = target.Handle.transform.position;
                flinchPos.y -= CombatGUIParams.FLINCH_DIST;
                flinch.AddCallback(hit.CallbackHandler);
                flinch.Init(target, flinchPos, CombatGUIParams.FLINCH_SPEED);
                this.DisplaySplatter(hit);
            }
            else
            {
                var data = new EvCharDeathData();
                data.Target = target;
                var e = new EvCharDeath(data);
                e.AddCallback(hit.CallbackHandler);
                e.TryProcess();
            }
        }

        private void DisplayParry(CChar target, MHit hit)
        {
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = hit;
            data.Priority = ViewParams.PARRY_PRIORITY;
            data.Target = target.Handle;
            data.Text = "Parry";
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Hit.AddDataDisplay(data);
            
            if (target.Proxy.GetRWeapon() != null && !target.Proxy.GetRWeapon().IsTypeOfShield())
            {
                var wpn = target.SubComponents[Layers.CHAR_R_WEAPON];
                this.DisplayParryAndBlockHelper(target, hit, wpn);
            }
            else if (target.Proxy.GetLWeapon() != null && !target.Proxy.GetLWeapon().IsTypeOfShield())
            {
                var wpn = target.SubComponents[Layers.CHAR_L_WEAPON];
                this.DisplayParryAndBlockHelper(target, hit, wpn);
            }
        }

        private void DisplayParryAndBlockHelper(CChar target, MHit hit, GameObject wpn)
        {
            var pos = wpn.transform.position;
            if (target.Proxy.LParty)
                pos.x -= CombatGUIParams.PARRY_OFFSET;
            else
                pos.x += CombatGUIParams.PARRY_OFFSET;
            var script = wpn.AddComponent<SBoomerang>();
            script.Init(wpn, pos, CombatGUIParams.PARRY_SPEED);
            script.AddCallback(hit.CallbackHandler);
        }

        private void DisplaySplatter(MHit hit)
        {
            if (hit.Data.Target.Current != null && 
                hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var tgt = hit.Data.Target.Current as CChar;
                if (hit.Data.Dmg > 0 && !hit.Data.IsFatality)
                {
                    var data = new EvSplatterData();
                    data.DmgPercent =
                        (hit.Data.Dmg /
                        tgt.Proxy.GetStat(ESecondaryStat.HP));
                    data.Target = tgt.Tile.Handle;
                    var e = new EvSplatter(data);
                    e.TryProcess();
                }
            }
        }

        private void ProcessBulletFXNonFatality(MAction a)
        {
            var attack = a.Data.Source.Handle.AddComponent<SAttackerJolt>();
            var position = Vector3.Lerp(a.Data.Target.Model.Center, a.Data.Source.Tile.Model.Center, CombatGUIParams.ATTACK_LERP);
            attack.Action = a;
            attack.Init(a.Data.Source, position, CombatGUIParams.ATTACK_SPEED);
            foreach(var hit in a.Data.Hits)
            {
                AttackSpriteLoader.Instance.GetBullet(
                    hit, 
                    this.ProcessBulletHit, 
                    CombatGUIParams.BULLET_SPEED);
            }
        }

        private void ProcessBulletHit(object o)
        {
            if (o.GetType().Equals(typeof(SBulletThenDelete)))
                this.ProcessDefenderHitsBulletThenDelete(o);
            else if (o.GetType().Equals(typeof(SBulletThenEmbed)))
                this.ProcessDefenderHitsBulletThenEmbed(o);
        }

        private void ProcessDefenderHitsJolt(object o)
        {
            var a = o as SAttackerJolt;
            if (a.Action != null)
            {
                foreach (var hit in a.Action.Data.Hits)
                {
                    if (hit.Data.Target.Current != null &&
                        hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                    {
                        var target = hit.Data.Target.Current as CChar;
                        this.ProcessDefenderHitsHelper(target, hit);
                    }
                }
            }
        }

        private void ProcessDefenderHitsBulletThenDelete(object o)
        {
            var a = o as SBulletThenDelete;
            if (a.Action != null)
            {
                foreach (var hit in a.Action.Data.Hits)
                {
                    if (hit.Data.Target.Current != null &&
                        hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                    {
                        var target = hit.Data.Target.Current as CChar;
                        this.ProcessDefenderHitsHelper(target, hit);
                    }    
                }
            }
        }

        private void ProcessDefenderHitsBulletThenEmbed(object o)
        {
            var a = o as SBulletThenEmbed;
            if (a.Action != null)
            {
                foreach (var hit in a.Action.Data.Hits)
                {
                    if (hit.Data.Target.Current != null &&
                        hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                    {
                        var target = hit.Data.Target.Current as CChar;
                        this.ProcessDefenderHitsHelper(target, hit);
                    }
                }
            }
        }

        private void ProcessDefenderHits(object o)
        {
            if (o.GetType().Equals(typeof(SAttackerJolt)))
                this.ProcessDefenderHitsJolt(o);
        }

        private void ProcessDefenderHitsHelper(CChar target, MHit hit)
        {
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge))
                this.DisplayDodge(target, hit);
            else if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block))
                this.DisplayBlock(target, hit);
            else if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                this.DisplayParry(target, hit);
            else
                this.DisplayFlinch(target, hit);
        }

        private void ProcessMeleeFXNonFatality(MAction a)
        {
            var attack = a.Data.Source.Handle.AddComponent<SAttackerJolt>();
            var position = Vector3.Lerp(a.Data.Target.Model.Center, a.Data.Source.Tile.Model.Center, CombatGUIParams.ATTACK_LERP);
            attack.Action = a;
            attack.AddCallback(this.ProcessDefenderHits);
            attack.Init(a.Data.Source, position, CombatGUIParams.ATTACK_SPEED);
        }

        private void ProcessSingleFXNonFatality(MAction a)
        {
            var attack = a.Data.Source.Handle.AddComponent<SAttackerJolt>();
            var position = Vector3.Lerp(a.Data.Target.Model.Center, a.Data.Source.Tile.Model.Center, CombatGUIParams.ATTACK_LERP);
            attack.Action = a;
            attack.Init(a.Data.Source, position, CombatGUIParams.ATTACK_SPEED);
            foreach (var hit in a.Data.Hits)
            {
                AttackSpriteLoader.Instance.GetSingleFX(hit);
            }
        }
    }
}
