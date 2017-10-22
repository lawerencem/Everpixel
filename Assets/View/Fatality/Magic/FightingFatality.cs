using Assets.View.Ability;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Magic
{
    public class FightingFatality : MFatality
    {
        public FightingFatality(FatalityData data) : base(EFatality.Fighting, data)
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            foreach(var hit in this.Data.FatalHits)
            {
                AttackSpriteLoader.Instance.GetBullet(hit, this.ProcessExplosion, FatalityParams.FATALITY_BULLET_SPEED);
                attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
            }
        }
    }
}
