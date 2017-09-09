using Assets.Controller.Character;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Ability;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class WeenbowFatality : MFatality
    {
        private string PATH = "Sprites/Bullet/Ween_Bullet";

        public WeenbowFatality(FatalityData data) : base(EFatality.Weenbow, data) { }

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
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            var bullet = AttackSpriteLoader.Instance.GetBullet(this._data.Action, this.ProcessWeens, FatalityParams.FATALITY_BULLET_SPEED);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessWeens(object o)
        {
            var sprite = AttackSpriteLoader.Instance.GetSprite(PATH);
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = this._data.Target.Current as CharController;
                    var shakeData = new SXAxisShakeData();
                    shakeData.Duration = FatalityParams.DEFAULT_DUR;
                    shakeData.MaxDistance = FatalityParams.DEFAULT_X_SHAKE_DIST;
                    shakeData.Speed = FatalityParams.DEFAULT_X_SHAKE_SPEED;
                    shakeData.Target = tgt.Handle;
                    var shake = tgt.Handle.AddComponent<SXAxisShake>();
                    shake.Init(shakeData);
                    var swarmData = new SSwarmOnTargetData();
                    swarmData.Dur = 5f;
                    swarmData.Interval = 0.00125f;
                    swarmData.MaxOffset = 2f;
                    swarmData.Speed = 8f;
                    swarmData.Sprite = sprite;
                    swarmData.Target = tgt.Handle.transform.position;
                    var weenSwarm = tgt.Handle.AddComponent<SSwarmOnTarget>();
                    weenSwarm.Init(swarmData);
                }
            }
        }
    }
}
