using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat.Hit;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class HeadShotCalculator : AAbilityCalculator
    {
        public override void Predict(Hit hit)
        {

        }

        public override void Process(Hit hit)
        {
            var roll = RNG.Instance.NextDouble();
            if (roll > LogicParams.BASE_HEAD_CHANCE)
                FHit.SetHeadTrue(hit.Data.Flags);
        }
    }
}
