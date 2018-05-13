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
        public override void Predict(MHit hit)
        {
            var tgt = hit.Data.Target.Current as CChar;

            if (tgt != null)
            {
                var acc = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Melee);
                var block = tgt.Proxy.GetStat(ESecondaryStat.Block);
                var blockChance = LogicParams.BASE_BLOCK_CHANCE;
                blockChance *= this.GetHeightDeltaMod(hit);
                blockChance *= this.GetSurroundedDeltaMod(hit);
                acc *= hit.Data.Ability.Data.AccMod;

                bool hasShield = false;

                if (tgt.Proxy.GetArmor() != null)
                    blockChance *= tgt.Proxy.GetArmor().GetStat(EArmorStat.Block_Mod);
                if (tgt.Proxy.GetHelm() != null)
                    blockChance *= tgt.Proxy.GetHelm().GetStat(EArmorStat.Block_Mod);
                if (tgt.Proxy.GetLWeapon() != null && tgt.Proxy.GetLWeapon().IsTypeOfShield())
                {
                    blockChance *= (tgt.Proxy.GetLWeapon().GetStat(EWeaponStat.Melee_Block_Chance));
                    hasShield = true;
                }
                if (tgt.Proxy.GetRWeapon() != null && tgt.Proxy.GetRWeapon().IsTypeOfShield())
                {
                    blockChance *= (tgt.Proxy.GetRWeapon().GetStat(EWeaponStat.Melee_Block_Chance));
                    hasShield = true;
                }
                if (hit.Data.Chances.Block > 1)
                    hit.Data.Chances.Block = 1;
                if (hit.Data.Chances.Block < 0)
                    hit.Data.Chances.Block = 0;

                hit.Data.Chances.Block = this.GetAttackVSDefenseSkillChance(acc, block, blockChance);

                if (!hasShield) { hit.Data.Chances.Block = 0; }
            }
        }

        public override void Process(MHit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Block > roll)
                FHit.SetBlockTrue(hit.Data.Flags);
        }
    }
}
