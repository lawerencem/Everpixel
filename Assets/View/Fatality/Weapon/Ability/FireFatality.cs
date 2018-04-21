using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Ability;
using Assets.View.Character;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class FireFatality : MFatality
    {
        private const string PATH = "Sprites/Bullet/";

        public FireFatality(FatalityData data) : base(EFatality.Fire, data)
        {
            this.Data.CustomPostFatalityBarks.Add("Arrowed!");
        }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessFirstShot);
        }

        private void ProcessFirstShot(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.GameHandle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_BULLET_LERP);
            var attack = this._data.Source.GameHandle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            foreach (var hit in this._data.FatalHits)
            {
                AttackSpriteLoader.Instance.GetBullet(hit, this.ProcessArrows, FatalityParams.FATALITY_BULLET_SPEED);
                attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
            }
        }

        private void ProcessArrows(object o)
        {
            var missile = AttackSpriteLoader.Instance.GetSprite(PATH + this.Data.Action.Data.ParentWeapon.SpriteFXPath);
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
                    shakeData.Target = tgt.GameHandle;
                    var shake = tgt.GameHandle.AddComponent<SXAxisShake>();
                    shake.Init(shakeData);

                    var data = new SFireFatalityData();
                    data.Dur = 6f;
                    data.FireDur = 3f;
                    data.Hit = hit;
                    data.Interval = FatalityParams.FIRE_INTERVAL;
                    data.Speed = FatalityParams.FIRE_SPEED;
                    data.Sprite = missile;
                    data.Target = tgt.GameHandle.transform.position;
                    var script = tgt.GameHandle.AddComponent<SFireFatality>();
                    script.Init(data);
                    script.SetCallback(this.ProcessArrowed);
                }
            }
        }

        private void ProcessArrowed(object o)
        {
            VCharUtil.Instance.ProcessDeadChar(this._data.Target.Current as CChar);
            foreach (var hit in this.Data.FatalHits)
                hit.CallbackHandler(null);
            this.CallbackHandler(null);
        }
    }
}
