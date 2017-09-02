using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat.Hit;
using Assets.Template.Util;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class BlockCalculator : AAbilityCalculator
    {
        public override void Predict(Hit hit)
        {
            var melee = hit.Data.Source.Model.GetCurrentStats().GetSecondaryStats().MeleeSkill;

            var tgt = hit.Data.Target.Current as CharController;

            var block = tgt.Model.GetCurrentStats().GetSecondaryStats().Block;
            var tgtEquipment = tgt.Model.GetEquipment();

            melee *= hit.Data.Ability.Data.AccMod;

            bool hasShield = false;

            if (tgtEquipment.GetArmor() != null)
                hit.Data.Chances.Block *= tgtEquipment.GetArmor().BlockReduce;
            if (tgtEquipment.GetHelm() != null)
                hit.Data.Chances.Block *= tgtEquipment.GetHelm().BlockReduce;
            if (tgtEquipment.GetLWeapon() != null && tgtEquipment.GetLWeapon().IsTypeOfShield())
            {
                hit.Data.Chances.Block *= (tgtEquipment.GetLWeapon().MeleeBlockChance / LogicParams.BASE_SCALAR);
                hasShield = true;
            }
            if (tgtEquipment.GetRWeapon() != null && tgtEquipment.GetRWeapon().IsTypeOfShield())
            {
                hit.Data.Chances.Block *= (tgtEquipment.GetRWeapon().MeleeBlockChance / LogicParams.BASE_SCALAR);
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
