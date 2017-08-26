using Assets.Model.Combat;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ResistCalculator : AAbilityCalculator
    {
        public override void Predict(Hit hit)
        {
            //if (hit.Ability.Resist != EResistType.None)
            //{
            //    var attack = hit.Source.Model.GetCurrentStatValue(ESecondaryStat.Spell_Penetration);
            //    switch (hit.Ability.Resist)
            //    {
            //        case (EResistType.Fortitude):
            //            {
            //                var defense = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Fortitude);
            //                hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
            //            }
            //            break;
            //        case (EResistType.Reflex):
            //            {
            //                var defense = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Reflex);
            //                hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
            //            }
            //            break;
            //        case (EResistType.Will):
            //            {
            //                var defense = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Will);
            //                hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
            //            }
            //            break;
            //    }
            //    if (hit.Chances.Resist > 1)
            //        hit.Chances.Resist = 1;
            //    if (hit.Chances.Resist < 0)
            //        hit.Chances.Resist = 0;
        }

        public override void Process(Hit hit)
        {
            //this.Predict(hit);
            //if (hit.Ability.Resist != EResistType.None)
            //{
            //    var roll = RNG.Instance.NextDouble();
            //    if (roll < hit.Chances.Resist)
            //        FHit.SetResistTrue(hit.Flags);
            //}
            //       }
        }
    }
}
