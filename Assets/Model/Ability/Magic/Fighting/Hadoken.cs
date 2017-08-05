using Assets.Model.Ability.Enum;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Hadoken : Ability
    {
        public Hadoken() : base(EnumAbility.Hadoken) {  }

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
