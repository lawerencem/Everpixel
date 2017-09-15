using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class TripleStrike : MAbility
    {
        public TripleStrike() : base(EAbility.Triple_Strike) { }

        public override void Predict(MHit hit)
        {
            base.PredictMelee(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessHitMelee(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return base.IsValidEnemyTarget(arg);
        }
    }
}
