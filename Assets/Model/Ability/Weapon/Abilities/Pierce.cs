using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class Pierce : MAbility
    {
        public Pierce() : base(EAbility.Pierce) { }

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
