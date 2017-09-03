using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View;
using Assets.View.Event;
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

        public void ProcessDefenderHit(Hit hit)
        {
            if (hit.Data.Target.Current != null &&
                hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                var target = hit.Data.Target.Current as CharController;
                this.ProcessDefenderHitsHelper(target, hit);
            }
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        private void DisplayDodge(CharController target, Hit hit)
        {
            var dodge = target.Handle.AddComponent<SBoomerang>();
            var dodgeTgt = ListUtil<TileController>.GetRandomListElement(target.Tile.GetAdjacent());
            var position = Vector3.Lerp(target.Handle.transform.position, dodgeTgt.Model.Center, CombatGUIParams.DODGE_LERP);
            dodge.AddCallback(hit.CallbackHandler);
            dodge.Init(target.Handle, position, CombatGUIParams.DODGE_SPEED);
            VCombatController.Instance.DisplayText("Dodge", target.Handle, CombatGUIParams.WHITE);
        }

        private void DisplayFlinch(CharController target, Hit hit)
        {
            if (hit.Data.Dmg < target.Model.GetCurrentPoints().CurrentHP) 
            {
                var flinch = target.Handle.AddComponent<SFlinch>();
                var flinchPos = target.Handle.transform.position;
                flinchPos.y -= CombatGUIParams.FLINCH_DIST;
                flinch.AddCallback(hit.CallbackHandler);
                flinch.Init(target, flinchPos, CombatGUIParams.FLINCH_SPEED);
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

        private void DisplayParry(CharController target, Hit hit)
        {
            VCombatController.Instance.DisplayText("Parry", target.Handle, CombatGUIParams.WHITE);
            var equipment = target.Model.GetEquipment();
            if (equipment.GetRWeapon() != null && !equipment.GetRWeapon().IsTypeOfShield())
            {
                var wpn = target.SubComponents[Layers.CHAR_R_WEAPON];
                this.DisplayParryHelper(target, hit, wpn);
            }
            else if (equipment.GetLWeapon() != null && !equipment.GetLWeapon().IsTypeOfShield())
            {
                var wpn = target.SubComponents[Layers.CHAR_L_WEAPON];
                this.DisplayParryHelper(target, hit, wpn);
            }
        }

        private void DisplayParryHelper(CharController target, Hit hit, GameObject wpn)
        {
            var pos = wpn.transform.position;
            if (target.Model.LParty)
                pos.x -= CombatGUIParams.WEAPON_OFFSET;
            else
                pos.x += CombatGUIParams.WEAPON_OFFSET;
            var script = wpn.AddComponent<SBoomerang>();
            script.Init(wpn, pos, CombatGUIParams.WEAPON_PARRY);
            script.AddCallback(hit.CallbackHandler);
        }

        private void ProcessDefenderHits(object o)
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
                            this.ProcessDefenderHitsHelper(target, hit);
                        }
                    }
                }
            }
        }

        private void ProcessDefenderHitsHelper(CharController target, Hit hit)
        {
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge))
                this.DisplayDodge(target, hit);
            else if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                this.DisplayParry(target, hit);
            else
                this.DisplayFlinch(target, hit);
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

        private void ProcessMeleeFXNonFatality(MAction a)
        {
            var attack = a.Data.Source.Handle.AddComponent<SAttackerJolt>();
            var position = Vector3.Lerp(a.Data.Target.Model.Center, a.Data.Source.Tile.Model.Center, CombatGUIParams.ATTACK_LERP);
            attack.Action = a;
            attack.AddCallback(this.ProcessDefenderHits);
            attack.Init(a.Data.Source, position, CombatGUIParams.ATTACK_SPEED);
        }
    }
}
