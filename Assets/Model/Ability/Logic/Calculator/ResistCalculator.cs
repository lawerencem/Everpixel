using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;
using Model.Combat;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ResistCalculator : AAbilityCalculator
    {
        public override void Predict(HitInfo hit)
        {
            if (hit.Ability.Resist != ResistTypeEnum.None)
            {
                var attack = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Spell_Penetration);
                switch (hit.Ability.Resist)
                {
                    case (ResistTypeEnum.Fortitude):
                        {
                            var defense = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Fortitude);
                            hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
                        }
                        break;
                    case (ResistTypeEnum.Reflex):
                        {
                            var defense = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Reflex);
                            hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
                        }
                        break;
                    case (ResistTypeEnum.Will):
                        {
                            var defense = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Will);
                            hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, LogicParams.BASE_RESIST);
                        }
                        break;
                }
                if (hit.Chances.Resist > 1)
                    hit.Chances.Resist = 1;
                if (hit.Chances.Resist < 0)
                    hit.Chances.Resist = 0;
            }
        }

        public override void Process(HitInfo hit)
        {
            this.Predict(hit);
            if (hit.Ability.Resist != ResistTypeEnum.None)
            {
                var roll = RNG.Instance.NextDouble();
                if (roll < hit.Chances.Resist)
                    AttackEventFlags.SetResistTrue(hit.Flags);
            }
        }
    }
}
