using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class CritCalculator : AAbilityCalculator
    {
        public override void Predict(MHit hit)
        {
            var melee = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Melee);
            var tgtController = hit.Data.Target.Current as CChar;
            var target = tgtController.Proxy;

            var crit = target.GetStat(ESecondaryStat.Critical_Chance);
            hit.Data.Chances.Crit = this.GetAttackVSDefenseSkillChance(melee, crit, LogicParams.BASE_CRIT_CHANCE);
            if (hit.Data.Chances.Crit > 1)
                hit.Data.Chances.Crit = 1;
            if (hit.Data.Chances.Crit < 0)
                hit.Data.Chances.Crit = 0;
        }

        public override void Process(MHit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Crit > roll)
                FHit.SetCritTrue(hit.Data.Flags);
        }
    }
}
