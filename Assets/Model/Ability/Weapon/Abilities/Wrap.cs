using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Weapon.Abilities
{
    public class Wrap : MAbility
    {
        public Wrap() : base(EAbility.Wrap) { }

        public override List<Hit> Predict(AbilityArgs arg)
        {
            return base.PredictMelee(arg);
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
