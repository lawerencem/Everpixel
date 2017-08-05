using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class BlockCalculator : AAbilityCalculator
    {
        public override void Predict(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var block = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Block);

            melee *= hit.Ability.AccMod;

            bool hasShield = false;

            if (hit.Target.Model.Armor != null)
                hit.Chances.Block *= hit.Target.Model.Armor.BlockReduce;
            if (hit.Target.Model.Helm != null)
                hit.Chances.Block *= hit.Target.Model.Helm.BlockReduce;
            if (hit.Target.Model.LWeapon != null && hit.Target.Model.LWeapon.IsTypeOfShield())
            {
                hit.Chances.Block *= (+(hit.Target.Model.LWeapon.MeleeBlockChance / LogicParams.BASE_SCALAR));
                hasShield = true;
            }
            if (hit.Target.Model.RWeapon != null && hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                hit.Chances.Block *= (LogicParams.BASE_SKILL_SCALAR + (hit.Target.Model.RWeapon.MeleeBlockChance / LogicParams.BASE_SCALAR));
                hasShield = true;
            }
            hit.Chances.Block = this.GetAttackVSDefenseSkillChance(melee, block, hit.Chances.Block);
            hit.Chances.Block *= hit.ModData.BlockMod;
            if (hit.Chances.Block > 1)
                hit.Chances.Block = 1;
            if (hit.Chances.Block < 0)
                hit.Chances.Block = 0;

            if (!hasShield) { hit.Chances.Block = 0; }
        }

        public override void Process(HitInfo hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Block > roll)
                AttackEventFlags.SetBlockTrue(hit.Flags);
        }
    }
}
