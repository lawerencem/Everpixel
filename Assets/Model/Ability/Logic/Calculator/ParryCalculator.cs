using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Template.Utility;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ParryCalculator : AAbilityCalculator
    {
        public override void Predict(Hit hit)
        {
            var stats = hit.Data.Source.Model.GetCurrentStats();
            var acc = stats.GetSecondaryStats().MeleeSkill;
            var parry = stats.GetSecondaryStats().ParrySkill;
            var parryChance = LogicParams.BASE_PARRY_CHANCE / hit.Data.Ability.Data.AccMod;

            var tgt = hit.Data.Target.Current as CharController;
            var equipment = tgt.Model.GetEquipment();

            if (equipment.GetArmor() != null)
                parryChance *= equipment.GetArmor().ParryReduce;
            if (equipment.GetHelm() != null)
                parryChance *= equipment.GetHelm().ParryReduce;
            if (equipment.GetLWeapon() != null)
                parryChance *= equipment.GetLWeapon().ParryMod;
            if (equipment.GetRWeapon() != null)
                parryChance *= equipment.GetRWeapon().ParryMod;

            if (hit.Data.Source.Model.Type == ECharType.Critter)
                parryChance = 0;

            acc *= hit.Data.Ability.Data.AccMod;
            parry *= hit.Data.Ability.Data.ParryModMod;

            if (hit.Data.Chances.Parry > 1)
                hit.Data.Chances.Parry = 1;
            if (hit.Data.Chances.Parry < 0)
                hit.Data.Chances.Parry = 0;

            hit.Data.Chances.Parry = this.GetAttackVSDefenseSkillChance(acc, parry, parryChance);
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Parry > roll)
                FHit.SetParryTrue(hit.Data.Flags);
        }
    }
}
