using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class Bite : MAbility
    {
        public Bite() : base(EAbility.Bite) { }

        public override void Predict(Hit hit)
        {
            base.PredictMelee(hit);
        }

        public override void Process(Hit hit)
        {
            base.ProcessHitMelee(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return base.IsValidEnemyTarget(arg);
        }
    }
}
