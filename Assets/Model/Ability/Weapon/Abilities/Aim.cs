using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Weapon.Abilities
{
    public class Aim : MAbility
    {
        public Aim() : base(EAbility.Aim) { }

        public override List<Hit> Predict(AbilityArgContainer arg)
        {
            return base.PredictBullet(arg);
        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            var hits = base.Process(arg);
            foreach (var hit in hits)
            {
                base.ProcessHitBullet(hit);
            }
            return hits;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return base.IsValidEnemyTarget(arg);
        }
    }
}
