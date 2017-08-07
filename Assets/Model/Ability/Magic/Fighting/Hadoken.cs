using Assets.Model.Ability.Enum;
using Assets.Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Hadoken : MAbility
    {
        public Hadoken() : base(EAbility.Hadoken) {  }

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
