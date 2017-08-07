using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;
using Generics.Utilities;

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
                FHit.SetHeadTrue(hit.Flags);
        }
    }
}
