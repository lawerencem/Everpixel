using Assets.Model.Combat;
using Template.Utility;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DodgeCalculator : AAbilityCalculator
    {
        public override  void Predict(Hit hit)
        {
            //var acc = hit.Source.Model.GetCurrentStatValue(ESecondaryStat.Melee);
            //var dodge = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Dodge);
            //var dodgeChance = LogicParams.BASE_DODGE_CHANCE / hit.Ability.AccMod;

            //if (hit.Target.Model.Armor != null)
            //    dodgeChance *= hit.Target.Model.Armor.DodgeMod;
            //if (hit.Target.Model.Helm != null)
            //    dodgeChance *= hit.Target.Model.Helm.DodgeMod;

            //if (hit.Target.Model.Type == ECharacterType.Critter)
            //    dodgeChance *= 1.75;

            //acc *= hit.Ability.AccMod;

            //hit.Chances.Dodge = this.GetAttackVSDefenseSkillChance(acc, dodge, dodgeChance);
            //hit.Chances.Dodge *= hit.Ability.DodgeMod;
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Dodge > roll)
                FHit.SetDodgeTrue(hit.GetFlags());
        }
    }
}
