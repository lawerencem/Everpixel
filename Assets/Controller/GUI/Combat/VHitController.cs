using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.View.Script.FX;
using System.Collections.Generic;
using Template.CB;
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

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
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
            attack.AddCallback(this.ProcessDefenderFlinches);
            attack.Init(a.Data.Source, position, CombatGUIParams.ATTACK_SPEED);
        }
    }
}
