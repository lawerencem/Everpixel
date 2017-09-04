using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Hadoken : MAbility
    {
        public Hadoken() : base(EAbility.Hadoken) {  }

        public override void Predict(Hit hit)
        {
            base.PredictBullet(hit);
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
