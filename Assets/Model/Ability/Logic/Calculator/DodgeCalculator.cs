﻿using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Equipment.Enum;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DodgeCalculator : AAbilityCalculator
    {
        public override  void Predict(Hit hit)
        {
            var target = hit.Data.Target.Current as CharController;

            var acc = target.Proxy.GetStat(ESecondaryStat.Melee);
            var dodge = target.Proxy.GetStat(ESecondaryStat.Dodge);
            var dodgeChance = LogicParams.BASE_DODGE_CHANCE / hit.Data.Ability.Data.AccMod;

            if (target.Proxy.GetArmor() != null)
                dodgeChance *= target.Proxy.GetArmor().GetStat(EArmorStat.Dodge_Mod);
            if (target.Proxy.GetHelm() != null)
                dodgeChance *= target.Proxy.GetHelm().GetStat(EArmorStat.Dodge_Mod);

            acc *= hit.Data.Ability.Data.AccMod;

            hit.Data.Chances.Dodge = this.GetAttackVSDefenseSkillChance(acc, dodge, dodgeChance);
            hit.Data.Chances.Dodge *= hit.Data.Ability.Data.DodgeMod;
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Dodge > roll)
                FHit.SetDodgeTrue(hit.Data.Flags);
        }
    }
}
