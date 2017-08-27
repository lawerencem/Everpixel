using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Hadoken : MAbility
    {
        public Hadoken() : base(EAbility.Hadoken) {  }

        public override List<Hit> Predict(AbilityArgs arg)
        {
            return base.PredictBullet(arg);
        }

        public override void Process(Hit hit)
        {
            base.ProcessHitBullet(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return base.IsValidEnemyTarget(arg);
        }
    }
}
