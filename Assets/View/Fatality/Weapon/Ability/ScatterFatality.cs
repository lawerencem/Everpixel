using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class ScatterFatality : MFatality
    {
        public ScatterFatality(FatalityData data) : base(EFatality.Scatter, data)
        {
            this._data.CustomPostFatalityBarks.Add("Boom.");
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
            attack.AddCallback(this.ProcessScatterFatalities);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessScatterFatalities(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                var tgt = hit.Data.Target.Current as CChar;
                VCharUtil.Instance.ProcessDeadChar(tgt);
                VCharUtil.Instance.SetBodyViewComponentsNull(tgt);
                var boneParticlePath = StringUtil.PathBuilder(
                    CombatGUIParams.EFFECTS_PATH,
                    CombatGUIParams.SCATTER_BONE_FATALITY,
                    CombatGUIParams.PARTICLES_EXTENSION);
                var boneParticlesPrefab = Resources.Load(boneParticlePath);
                var boneParticles = GameObject.Instantiate(boneParticlesPrefab) as GameObject;
                boneParticles.transform.position = tgt.GameHandle.transform.position;
                boneParticles.name = "Scatter Fatality Particle";
                this.RotateParticles(this.Data.Source, tgt, boneParticles);
                var destoyBoneParticles = boneParticles.AddComponent<SDestroyByLifetime>();
                destoyBoneParticles.AddCallback(this.CallbackHandler);
                destoyBoneParticles.AddCallback(hit.CallbackHandler);
                destoyBoneParticles.Init(boneParticles, 5f);

                var explosionPath = StringUtil.PathBuilder(
                    CombatGUIParams.EFFECTS_PATH,
                    CombatGUIParams.SCATTER_EXPLOSION_FATALITY,
                    CombatGUIParams.PARTICLES_EXTENSION);
                var explosionPrefab = Resources.Load(explosionPath);
                var explosionParticles = GameObject.Instantiate(explosionPrefab) as GameObject;
                explosionParticles.transform.position = tgt.GameHandle.transform.position;
                explosionParticles.name = "Scatter Explosion Particle";
                var destroyExplosion = explosionParticles.AddComponent<SDestroyByLifetime>();
                destroyExplosion.Init(explosionParticles, 5f);
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

        private void RotateParticles(CChar src, CChar tgt, GameObject particles)
        {
            if (src.Tile.Model.IsTileNE(tgt.Tile.Model, 1))
                particles.transform.Rotate(0, 60, 0);
            else if (src.Tile.Model.IsTileSE(tgt.Tile.Model, 1))
                particles.transform.Rotate(0, 120, 0);
            else if (src.Tile.Model.IsTileS(tgt.Tile.Model, 1))
                particles.transform.Rotate(0, 180, 0);
            else if (src.Tile.Model.IsTileSW(tgt.Tile.Model, 1))
                particles.transform.Rotate(0, 240, 0);
            else if (src.Tile.Model.IsTileNW(tgt.Tile.Model, 1))
                particles.transform.Rotate(0, 300, 0);
        }
    }
}
