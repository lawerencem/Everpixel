using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Physical
{
    public class AttackOfOpportunity : MAbility
    {
        public AttackOfOpportunity() : base(EAbility.Attack_Of_Opportunity) { this._wpnAbility = false; }

        public override void Predict(MHit hit)
        {
            base.PredictMelee(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessHitMelee(hit);
        }
    }
}
