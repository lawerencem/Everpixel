using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Combat;
using Generics.Utilities;
using Assets.Model.Combat;
using Model.Characters;
using Assets.Model.Abiltiy.Logic;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ParryCalculator : AAbilityCalculator
    {
        public override void Predict(Hit hit)
        {
            var acc = hit.Source.Model.GetCurrentStatValue(ESecondaryStat.Melee);
            var parry = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Parry);
            var parryChance = LogicParams.BASE_PARRY_CHANCE / hit.Ability.AccMod;

            if (hit.Target.Model.Armor != null)
                parryChance *= hit.Target.Model.Armor.ParryReduce;
            if (hit.Target.Model.Helm != null)
                parryChance *= hit.Target.Model.Helm.ParryReduce;
            if (hit.Target.Model.LWeapon != null)
                parryChance *= hit.Target.Model.LWeapon.ParryMod;
            if (hit.Target.Model.RWeapon != null)
                parryChance *= hit.Target.Model.RWeapon.ParryMod;

            if (hit.Target.Model.Type == ECharacterType.Critter)
                parryChance = 0;

            acc *= hit.Ability.AccMod;
            parry *= hit.Ability.ParryModMod;

            if (hit.Chances.Parry > 1)
                hit.Chances.Parry = 1;
            if (hit.Chances.Parry < 0)
                hit.Chances.Parry = 0;

            hit.Chances.Parry = this.GetAttackVSDefenseSkillChance(acc, parry, parryChance);
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Parry > roll)
                FHit.SetParryTrue(hit.Flags);
        }
    }
}
