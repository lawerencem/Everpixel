using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Equipment.Enum;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class ParryCalculator : AAbilityCalculator
    {
        public override void Predict(MHit hit)
        {
            var acc = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Melee);
            var parry = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Parry);
            var parryChance = LogicParams.BASE_PARRY_CHANCE / hit.Data.Ability.Data.AccMod;

            var tgt = hit.Data.Target.Current as CharController;

            if (hit.Data.Source.Proxy.GetArmor() != null)
                parryChance *= hit.Data.Source.Proxy.GetArmor().GetStat(EArmorStat.Parry_Mod);
            if (hit.Data.Source.Proxy.GetHelm() != null)
                parryChance *= hit.Data.Source.Proxy.GetHelm().GetStat(EArmorStat.Parry_Mod);
            if (hit.Data.Source.Proxy.GetLWeapon() != null)
                parryChance *= hit.Data.Source.Proxy.GetLWeapon().GetStat(EWeaponStat.Parry_Mod);
            if (hit.Data.Source.Proxy.GetRWeapon() != null)
                parryChance *= hit.Data.Source.Proxy.GetRWeapon().GetStat(EWeaponStat.Parry_Mod);

            if (hit.Data.Source.Proxy.Type == ECharType.Critter)
                parryChance = 0;

            acc *= hit.Data.Ability.Data.AccMod;
            parry *= hit.Data.Ability.Data.ParryModMod;

            if (hit.Data.Chances.Parry > 1)
                hit.Data.Chances.Parry = 1;
            if (hit.Data.Chances.Parry < 0)
                hit.Data.Chances.Parry = 0;

            hit.Data.Chances.Parry = this.GetAttackVSDefenseSkillChance(acc, parry, parryChance);
        }

        public override void Process(MHit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Parry > roll)
                FHit.SetParryTrue(hit.Data.Flags);
        }
    }
}
