using Model.Combat;
using Generics.Utilities;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;

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
                AttackEventFlags.SetHeadTrue(hit.Flags);
        }
    }
}
