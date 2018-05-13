using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class GreatStrikeFatality : MFatality
    {
        public GreatStrikeFatality(FatalityData data) : base(EFatality.Great_Strike, data)
        {
            
        }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.GameHandle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.GameHandle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            attack.AddCallback(this.ProcessGreatStrikeFatalities);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessGreatStrikeFatalities(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                var tgt = hit.Data.Target.Current as CChar;
                VCharUtil.Instance.ProcessDeadChar(tgt);
                VCharUtil.Instance.SetBodyViewComponentsNull(tgt);

                var boneParticlePath = StringUtil.PathBuilder(
                    CombatGUIParams.EFFECTS_PATH,
                    CombatGUIParams.GREAT_STRIKE_BONE_FATALITY,
                    CombatGUIParams.PARTICLES_EXTENSION);
                var boneParticlesPrefab = Resources.Load(boneParticlePath);
                var boneParticles = GameObject.Instantiate(boneParticlesPrefab) as GameObject;
                boneParticles.transform.position = tgt.GameHandle.transform.position;
                boneParticles.name = "GreatStrike Bone Particle";
                var destoyBoneParticles = boneParticles.AddComponent<SDestroyByLifetime>();
                destoyBoneParticles.AddCallback(this.CallbackHandler);
                destoyBoneParticles.AddCallback(hit.CallbackHandler);
                destoyBoneParticles.Init(boneParticles, 5f);

                var bloodParticlePath = StringUtil.PathBuilder(
                    CombatGUIParams.EFFECTS_PATH,
                    CombatGUIParams.GREAT_STRIKE_BLOOD_FATALITY,
                    CombatGUIParams.PARTICLES_EXTENSION);
                var bloodParticlesPrefab = Resources.Load(bloodParticlePath);
                var bloodParticles = GameObject.Instantiate(bloodParticlesPrefab) as GameObject;
                bloodParticles.transform.position = tgt.GameHandle.transform.position;
                bloodParticles.name = "Greatstrike Blood Particle";
                var destroyExplosion = bloodParticles.AddComponent<SDestroyByLifetime>();
                destroyExplosion.Init(bloodParticles, 5f);
            }
            foreach (var hit in this.Data.NonFatalHits)
            {
                var handle = new GameObject();
                var destroy = handle.AddComponent<SDestroyByLifetime>();
                destroy.AddCallback(this.CallbackHandler);
                destroy.AddCallback(hit.CallbackHandler);
                destroy.Init(handle, FatalityParams.DEFAULT_DUR);
            }
        }
    }
}
