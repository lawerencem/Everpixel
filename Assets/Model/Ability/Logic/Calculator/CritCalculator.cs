using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class CritCalculator : AAbilityCalculator
    {
        public override void Predict(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var crit = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Critical_Chance);
            hit.Chances.Crit = this.GetAttackVSDefenseSkillChance(melee, crit, LogicParams.BASE_CRIT_CHANCE);
            if (hit.Chances.Crit > 1)
                hit.Chances.Crit = 1;
            if (hit.Chances.Crit < 0)
                hit.Chances.Crit = 0;
        }

        public override void Process(HitInfo hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Crit > roll)
                AttackEventFlags.SetCritTrue(hit.Flags);
        }
    }
}
