using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class SuppressArea : MAbility
    {
        public SuppressArea() : base(EAbility.Suppress_Area) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {
            base.PredictTile(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessTile(hit);
            FActionStatus.SetSuppressingAreaTrue(hit.Data.Action.Data.Source.Proxy.GetActionFlags());
        }
    }
}
