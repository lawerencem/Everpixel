using Assets.Model.Ability.Enum;
using Assets.Model.Map.Tile;
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
                this._data.Source.GameHandle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.GameHandle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            if (this._data.Action.ActiveAbility.Data.CastType == ECastType.Raycast)
            {
                var tgt = attack.Action.Data.Source.Tile.Model.GetRaycastTiles(attack.Action.Data.Target.Model, attack.Action.ActiveAbility.Data.Range);
                if (tgt.Count > 0)
                {
                    var finalTile = tgt[tgt.Count - 1] as MTile;
                    AttackSpriteLoader.Instance.GetRaycast(attack.Action, finalTile.Controller, this.ProcessExplosion, FatalityParams.FATALITY_BULLET_SPEED);
                    attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
                }
                else
                    throw new System.Exception("Attempting raycast FX with empty target");
            }
            else
            {
                foreach (var hit in this.Data.FatalHits)
                {
                    AttackSpriteLoader.Instance.GetBullet(hit, this.ProcessExplosion, FatalityParams.FATALITY_BULLET_SPEED);
                    attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
                }
            }
        }
    }
}
