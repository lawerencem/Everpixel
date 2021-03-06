﻿using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class CrushFatality : MFatality
    {
        public CrushFatality(FatalityData data) : base(EFatality.Crush, data) {}

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessCrush(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                {
                    var tgt = this._data.Target.Current as CChar;
                    var path = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        CombatGUIParams.CRUSH_FATALITY,
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var position = tgt.GameHandle.transform.position;
                    var prefab = Resources.Load(path);
                    var particles = GameObject.Instantiate(prefab) as GameObject;
                    particles.transform.position = position;
                    particles.name = CombatGUIParams.CRUSH_FATALITY + " Particles";
                    var lifetime = particles.AddComponent<SDestroyByLifetime>();
                    lifetime.Init(particles, FatalityParams.DEFAULT_DUR);
                    lifetime.AddCallback(this.CallbackHandler);
                    lifetime.AddCallback(hit.CallbackHandler);
                    VCharUtil.Instance.ProcessDeadChar(tgt);
                }
                else
                {
                    VHitController.Instance.ProcessDefenderHit(hit);
                }
            }
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.GameHandle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.GameHandle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            attack.AddCallback(this.ProcessCrush);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }
    }
}
