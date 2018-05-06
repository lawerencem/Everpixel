using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Combat.Hit;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Ability;
using Assets.View.Fatality;
using Assets.View.Particle;
using UnityEngine;

namespace Assets.View.Script.FX
{
    public class SFireFatalityData
    {
        public float Dur { get; set; }
        public float FireDur { get; set; }
        public MHit Hit { get; set; }
        public float Interval { get; set; }
        public float Speed { get; set; }
        public Sprite Sprite { get; set; }
        public Vector3 Target { get; set; }

        public SFireFatalityData()
        {

        }
    }

    public class SFireFatality : AScript
    {
        private float _curInterval;
        private float _curTime;
        private SFireFatalityData _data;
        private GameObject _targetHandle;

        public void Init(SFireFatalityData data)
        {
            this._data = data;
            var character = this._data.Hit.Data.Target.Current as CChar;
            this._targetHandle = character.GameHandle;
        }

        public void Update()
        {
            this._curInterval += Time.deltaTime;
            this._curTime += Time.deltaTime;

            if (this._curTime >= this._data.Dur)
            {
                this.DoCallbacks();
                GameObject.Destroy(this);
            }
            else if (this._curTime <= this._data.FireDur)
            {
                if (this._curInterval >= this._data.Interval)
                {
                    this._curInterval = 0;
                    AttackSpriteLoader.Instance.GetBullet(this._data.Hit, this.AttachBloodParticles, FatalityParams.FATALITY_BULLET_SPEED);
                }
            }
        }

        private void AttachBloodParticles(object o)
        {
            var controller = new ParticleController();
            var path = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "Bleed",
                CombatGUIParams.PARTICLES_EXTENSION);
            var particles = controller.CreateParticle(path);
            if (particles != null)
                controller.AttachParticle(this._targetHandle, particles);
            var script = particles.AddComponent<SDestroyByLifetime>();
            script.Init(particles, 1f);
        }
    }
}
