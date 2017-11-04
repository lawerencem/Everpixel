using Assets.Controller.Character;
using Assets.Model.Combat.Hit;
using Assets.Model.Effect;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ResistCalculator : AAbilityCalculator
    {
        public bool DidResist(CChar target, MEffect effect)
        {
            return false;
        }

        public override void Predict(MHit hit)
        {
            //if (hit.Data.Ability.Data.Resist != EResistType.None)
            //{
            //    var attack = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Spell_Penetration);
            //    switch (hit.Data.Ability.Data.Resist)
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
            //    if (hit.Data.Chances.Resist > 1)
            //        hit.Data.Chances.Resist = 1;
            //    if (hit.Data.Chances.Resist < 0)
            //        hit.Data.Chances.Resist = 0;
        }

        public override void Process(MHit hit)
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
