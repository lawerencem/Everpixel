using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Equipment.Enum;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class BlockCalculator : AAbilityCalculator
    {
        public override void Predict(Hit hit)
        {
            var melee = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Melee);
            var tgt = hit.Data.Target.Current as CharController;
            var block = tgt.Proxy.GetStat(ESecondaryStat.Block);
            melee *= hit.Data.Ability.Data.AccMod;

            bool hasShield = false;

            if (tgt.Proxy.GetArmor() != null)
                hit.Data.Chances.Block *= tgt.Proxy.GetArmor().GetStat(EArmorStat.Block_Mod);
            if (tgt.Proxy.GetHelm() != null)
                hit.Data.Chances.Block *= tgt.Proxy.GetHelm().GetStat(EArmorStat.Block_Mod);
            if (tgt.Proxy.GetLWeapon() != null && tgt.Proxy.GetLWeapon().IsTypeOfShield())
            {
                hit.Data.Chances.Block *= (tgt.Proxy.GetLWeapon().GetStat(EWeaponStat.Melee_Block_Chance) / LogicParams.BASE_SCALAR);
                hasShield = true;
            }
            if (tgt.Proxy.GetRWeapon() != null && tgt.Proxy.GetRWeapon().IsTypeOfShield())
            {
                hit.Data.Chances.Block *= (tgt.Proxy.GetRWeapon().GetStat(EWeaponStat.Melee_Block_Chance) / LogicParams.BASE_SCALAR);
                hasShield = true;
            }
            hit.Data.Chances.Block = this.GetAttackVSDefenseSkillChance(melee, block, hit.Data.Chances.Block);
            hit.Data.Chances.Block *= hit.Data.ModData.BlockMod;
            if (hit.Data.Chances.Block > 1)
                hit.Data.Chances.Block = 1;
            if (hit.Data.Chances.Block < 0)
                hit.Data.Chances.Block = 0;

            if (!hasShield) { hit.Data.Chances.Block = 0; }
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Block > roll)
                FHit.SetBlockTrue(hit.Data.Flags);
        }
    }
}
