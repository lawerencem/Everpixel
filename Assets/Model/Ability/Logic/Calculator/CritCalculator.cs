using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class CritCalculator : AAbilityCalculator
    {
        public override void Predict(MHit hit)
        {
            //var melee = hit.Source.Model.GetCurrentStatValue(ESecondaryStat.Melee);
            //var crit = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Critical_Chance);
            //hit.Chances.Crit = this.GetAttackVSDefenseSkillChance(melee, crit, LogicParams.BASE_CRIT_CHANCE);
            //if (hit.Chances.Crit > 1)
            //    hit.Chances.Crit = 1;
            //if (hit.Chances.Crit < 0)
            //    hit.Chances.Crit = 0;
        }

        public override void Process(MHit hit)
        {
            //this.Predict(hit);
            //var roll = RNG.Instance.NextDouble();
            //if (hit.Chances.Crit > roll)
            //    FHit.SetCritTrue(hit.Flags);
        }
    }
}
