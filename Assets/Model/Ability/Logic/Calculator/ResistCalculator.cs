using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Effect;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ResistCalculator : AAbilityCalculator
    {
        public bool DidResist(CChar target, MEffect effect, double resistBase)
        {
            double resist = 0;
            if (effect.Data.Resist == EResistType.Fortitude)
                resist = target.Proxy.GetStat(ESecondaryStat.Fortitude);
            else if (effect.Data.Resist == EResistType.Reflex)
                resist = target.Proxy.GetStat(ESecondaryStat.Reflex);
            else if (effect.Data.Resist == EResistType.Will)
                resist = target.Proxy.GetStat(ESecondaryStat.Will);

            var chance = this.GetAttackVSDefenseSkillChance(resistBase, resist, LogicParams.BASE_RESIST);
            var roll = RNG.Instance.NextDouble();
            if (roll < chance)
                return true;
            else
                return false;
        }

        public bool DidResist(CChar target, EResistType resist, double resistBase)
        {
            double resistValue = 0;
            if (resist == EResistType.Fortitude)
                resistValue = target.Proxy.GetStat(ESecondaryStat.Fortitude);
            else if (resist == EResistType.Reflex)
                resistValue = target.Proxy.GetStat(ESecondaryStat.Reflex);
            else if (resist == EResistType.Will)
                resistValue = target.Proxy.GetStat(ESecondaryStat.Will);

            var chance = this.GetAttackVSDefenseSkillChance(resistBase, resistValue, LogicParams.BASE_RESIST);
            var roll = RNG.Instance.NextDouble();
            if (roll < chance)
                return true;
            else
                return false;
        }

        public override void Predict(MHit hit)
        {
            if (hit.Data.Ability.Data.Resist != Enum.EResistType.None)
            {
                var attack = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Spell_Penetration);
                var proxy = hit.Data.Target.Current as CChar;
                double resist = 0;
                switch (hit.Data.Ability.Data.Resist)
                {
                    case (EResistType.Fortitude):
                        {
                            var defense = proxy.Proxy.GetStat(ESecondaryStat.Fortitude);
                            resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
                        }
                        break;
                    case (EResistType.Reflex):
                        {
                            var defense = proxy.Proxy.GetStat(ESecondaryStat.Reflex);
                            resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
                        }
                        break;
                    case (EResistType.Will):
                        {
                            var defense = proxy.Proxy.GetStat(ESecondaryStat.Will);
                            resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
                        }
                        break;
                }
            }

            if (hit.Data.Chances.Resist > 1)
                hit.Data.Chances.Resist = 1;
            if (hit.Data.Chances.Resist < 0)
                hit.Data.Chances.Resist = 0;
        }

        public override void Process(MHit hit)
        {
            this.Predict(hit);
            if (hit.Data.Ability.Data.Resist != EResistType.None)
            {
                var roll = RNG.Instance.NextDouble();
                if (roll < hit.Data.Chances.Resist)
                    FHit.SetResistTrue(hit.Data.Flags);
            }
        }
    }
}
