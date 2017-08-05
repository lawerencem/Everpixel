using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DodgeCalculator : AAbilityCalculator
    {
        public override  void Predict(Hit hit)
        {
            var acc = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var dodge = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Dodge);
            var dodgeChance = LogicParams.BASE_DODGE_CHANCE / hit.Ability.AccMod;

            if (hit.Target.Model.Armor != null)
                dodgeChance *= hit.Target.Model.Armor.DodgeMod;
            if (hit.Target.Model.Helm != null)
                dodgeChance *= hit.Target.Model.Helm.DodgeMod;

            if (hit.Target.Model.Type == CharacterTypeEnum.Critter)
                dodgeChance *= 1.75;

            acc *= hit.Ability.AccMod;

            hit.Chances.Dodge = this.GetAttackVSDefenseSkillChance(acc, dodge, dodgeChance);
            hit.Chances.Dodge *= hit.Ability.DodgeMod;
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Dodge > roll)
                AttackEventFlags.SetDodgeTrue(hit.Flags);
        }
    }
}
