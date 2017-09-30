using Assets.Controller.Character;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Ability;
using Assets.View.Character;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class WeenbowFatality : MFatality
    {
        private const string BONE = "Bone";
        private const string RIBS = "Ribs";
        private const string PATH = "Sprites/Bullet/Ween_Bullet";

        public WeenbowFatality(FatalityData data) : base(EFatality.Weenbow, data)
        {
            this._data.CustomPreFatalityBarks.Add("The thousand weeners of the " +
                "Dachschund Empire descend upon you...our weens will blot out the sun!");
        }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessFirstShot);
        }

        private void ProcessFirstShot(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_BULLET_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            foreach(var hit in this._data.FatalHits)
            {
                var bullet = AttackSpriteLoader.Instance.GetBullet(hit, this.ProcessWeens, FatalityParams.FATALITY_BULLET_SPEED);
                attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
            }
        }

        private void ProcessWeens(object o)
        {
            var sprite = AttackSpriteLoader.Instance.GetSprite(PATH);
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                {
                    var tgt = this._data.Target.Current as CChar;
                    var shakeData = new SXAxisShakeData();
                    shakeData.Duration = FatalityParams.DEFAULT_DUR;
                    shakeData.MaxDistance = FatalityParams.DEFAULT_X_SHAKE_DIST;
                    shakeData.Speed = FatalityParams.DEFAULT_X_SHAKE_SPEED;
                    shakeData.Target = tgt.Handle;
                    var shake = tgt.Handle.AddComponent<SXAxisShake>();
                    shake.Init(shakeData);
                    var swarmData = new SSwarmOnTargetData();
                    swarmData.Dur = FatalityParams.DEFAULT_DUR;
                    swarmData.Interval = FatalityParams.WEEN_INTERVAL;
                    swarmData.MaxOffset = FatalityParams.WEEN_OFFSET;
                    swarmData.Speed = FatalityParams.WEEN_SPEED;
                    swarmData.Sprite = sprite;
                    swarmData.Target = tgt.Handle.transform.position;
                    var weenSwarm = tgt.Handle.AddComponent<SSwarmOnTarget>();
                    weenSwarm.AddCallback(this.ProcessSwarm);
                    weenSwarm.Init(swarmData);
                }
            }
        }

        private void ProcessSwarm(object o)
        {
            if (o.GetType().Equals(typeof(SSwarmOnTarget)))
            {
                var swarm = o as SSwarmOnTarget;
                foreach(var item in swarm.GetSwarm())
                {
                    var shakeData = new SXAxisShakeData();
                    shakeData.Duration = FatalityParams.DEFAULT_DUR;
                    shakeData.MaxDistance = FatalityParams.DEFAULT_X_SHAKE_DIST;
                    shakeData.Speed = FatalityParams.DEFAULT_X_SHAKE_SPEED;
                    shakeData.Target = item;
                    var shake = item.AddComponent<SXAxisShake>();
                    shake.Init(shakeData);
                    shake.AddCallback(this.DeleteSwarm);
                }
            }
            this.ProcessTarget();
            this.CallbackStuff();
        }

        private void ProcessTarget()
        {
            var tgt = this._data.Target.Current as CChar;
            this.ProcessBlood(tgt);
            this.SetBodyComponentsNull(tgt);
            var ribs = CharSpriteLoader.Instance.GetEffectSprite(RIBS);
            this.LayFatalityDecoRandomPosition(ribs, tgt);
            var random = RNG.Instance.Next(5);
            for (int i = 0; i < random; i++)
            {
                var bone = CharSpriteLoader.Instance.GetEffectSprite(BONE);
                this.LayFatalityDecoRandomPosition(bone, tgt);
            }
            var geyser = this.AddBloodGeyser(tgt);
            var lifetime = geyser.AddComponent<SDestroyByLifetime>();
            lifetime.Init(geyser, FatalityParams.DEFAULT_DUR);
            lifetime.AddCallback(this.CallbackHandler);
            foreach(var hit in this._data.FatalHits)
                lifetime.AddCallback(hit.CallbackHandler);
            foreach (var hit in this._data.NonFatalHits)
                lifetime.AddCallback(hit.CallbackHandler);
        }

        private void CallbackStuff()
        {
            foreach (var hit in this._data.FatalHits)
                hit.DoCallbacks();
        }

        private void DeleteSwarm(object o)
        {
            if (o.GetType().Equals(typeof(SXAxisShake)))
            {
                var shake = o as SXAxisShake;
                var tgt = shake.gameObject;
                GameObject.Destroy(tgt);
            }
        }
    }
}
