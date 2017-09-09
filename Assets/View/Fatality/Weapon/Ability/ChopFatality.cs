using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class ChopFatality : MFatality
    {
        public ChopFatality(FatalityData data) : base(EFatality.Chop, data) { }

        public override void Init()
        {
            base.Init();
            this.AddCallback(this.AssignDeadLayers);
            base.Start(this.ProcessJolt);
        }

        private void ProcessChop(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = this._data.Target.Current as CharController;
                    var position = tgt.Handle.transform.position;

                    var bonePath = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        "ChopBoneFatality",
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var bloodPath = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        "ChopBloodFatality",
                        CombatGUIParams.PARTICLES_EXTENSION);

                    
                    var bonePrefab = Resources.Load(bonePath);
                    var boneParticles = GameObject.Instantiate(bonePrefab) as GameObject;
                    boneParticles.transform.position = position;
                    boneParticles.name = CombatGUIParams.CHOP_FATALITY + " Bone Particles";

                    var bloodPrefab = Resources.Load(bloodPath);
                    var bloodParticles = GameObject.Instantiate(bloodPrefab) as GameObject;
                    bloodParticles.transform.position = position;
                    bloodParticles.name = CombatGUIParams.CHOP_FATALITY + " Blood Particles";

                    var boneLifetime = boneParticles.AddComponent<SDestroyByLifetime>();
                    boneLifetime.Init(boneParticles, 5f);
                    var bloodLifetime = bloodParticles.AddComponent<SDestroyByLifetime>();
                    bloodLifetime.Init(bloodParticles, 5f);

                    var data = new SXAxisShakeData();
                    data.Duration = FatalityParams.DEFAULT_DUR;
                    data.MaxDistance = FatalityParams.DEFAULT_X_SHAKE_DIST;
                    data.Speed = FatalityParams.DEFAULT_X_SHAKE_SPEED;
                    data.Target = tgt.Handle;
                    var shake = tgt.Handle.AddComponent<SXAxisShake>();
                    shake.AddCallback(this.ProcessExplosion);
                    shake.Init(data);
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
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            attack.AddCallback(this.ProcessChop);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void AssignDeadLayers(object o)
        {
            foreach(var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null && hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = hit.Data.Target.Current as CharController;
                    VCharUtil.Instance.ProcessDeadChar(tgt);
                }
            }
        }
    }
}
