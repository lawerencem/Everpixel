using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DamageCalculator : AAbilityCalculator
    {
        public void CalculateAbilityDmg(Hit hit)
        {
            var hitData = hit.Data;
            var abilityData = hitData.Ability.Data;
            var equipment = hit.Data.Source.Model.GetEquipment();
            var source = hit.Data.Source.Model;

            var dmg = hit.Data.ModData.BaseDamage;
            dmg += abilityData.FlatDamage;
            if (abilityData.CastType == ECastType.Melee)
            {
                if (equipment.GetRWeapon() != null && !equipment.GetRWeapon().IsTypeOfShield())
                    dmg += equipment.GetRWeapon().Damage;
                if (equipment.GetLWeapon() != null && !equipment.GetLWeapon().IsTypeOfShield())
                    dmg += equipment.GetLWeapon().Damage;
            }
            dmg += (abilityData.DmgPerPower * source.GetCurrentStats().GetSecondaryStats().Power);
            dmg *= abilityData.DamageMod;
            hit.Data.Dmg = (int)dmg;
        }

        public void ModifyDmgViaDefender(Hit hit)
        {
            if (!FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
            {
                if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block))
                    this.ProcessShieldBlock(hit);
                else
                    this.Process(hit);
            }
            else
            {
                hit.Data.Dmg = 0;
            }
        }

        public override void Predict(Hit hit)
        {
            //this.CalculateAbilityDmg(hit);
            //var dmgReduction = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Damage_Reduction);
            //var bodyReduction = dmgReduction;
            //var headReduction = dmgReduction;
            //double flatDmgNegate = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Damage_Ignore);
            //double bodyDmgNegate = 0;
            //double headDmgNegate = 0;

            //if (hit.Target.Model.Armor != null)
            //{
            //    bodyDmgNegate += hit.Target.Model.Armor.DamageIgnore;
            //    bodyReduction *= hit.Target.Model.Armor.DamageReduction;
            //}
            //if (hit.Target.Model.Helm != null)
            //{
            //    headDmgNegate += hit.Target.Model.Helm.DamageIgnore;
            //    headReduction *= hit.Target.Model.Helm.DamageReduction;
            //}

            //if (hit.Source.Model.LWeapon != null && !hit.Source.Model.LWeapon.IsTypeOfShield())
            //    flatDmgNegate *= hit.Source.Model.LWeapon.ArmorPierce;
            //if (hit.Source.Model.RWeapon != null && !hit.Source.Model.RWeapon.IsTypeOfShield())
            //    flatDmgNegate *= hit.Source.Model.RWeapon.ArmorPierce;
            //foreach (var perk in hit.Target.Model.Perks.WhenHitPerks)
            //    perk.TryModHit(hit);

            //var bodyWeight = ((hit.Dmg - flatDmgNegate - bodyDmgNegate) * bodyReduction) * LogicParams.BASE_BODY_RATIO;
            //var headWeight = ((hit.Dmg - flatDmgNegate - headDmgNegate) * headReduction);
            //hit.Chances.Damage = (bodyWeight + headWeight) / (LogicParams.BASE_HIT_RATIO + 1);
        }

        public override void Process(Hit hit)
        {
            var source = hit.Data.Source.Model;
            var targetController = hit.Data.Target.Current as CharController;
            var target = targetController.Model;
            var sourceEquipment = source.GetEquipment();
            var targetEquipment = target.GetEquipment();
            var sourceStats = source.GetCurrentStats();
            var targetStats = target.GetCurrentStats();

            var dmgToApply = (double)hit.Data.Dmg;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Critical))
                dmgToApply *= (LogicParams.BASE_CRIT_SCALAR + (sourceStats.GetSecondaryStats().CriticalMultiplier / LogicParams.BASE_SCALAR));
            var dmgReduction = targetStats.GetSecondaryStats().DamageReduce;
            double flatDmgNegate = sourceStats.GetSecondaryStats().DamageIgnore;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Head))
            {
                if (targetEquipment.GetHelm() != null)
                    flatDmgNegate += targetEquipment.GetHelm().DamageIgnore;
            }
            else
            {
                if (targetEquipment.GetArmor() != null)
                    flatDmgNegate += targetEquipment.GetArmor().DamageIgnore;
            }
            if (sourceEquipment.GetLWeapon() != null && !sourceEquipment.GetLWeapon().IsTypeOfShield())
                flatDmgNegate *= (sourceEquipment.GetLWeapon().ArmorPierce);
            if (sourceEquipment.GetRWeapon() != null && !sourceEquipment.GetRWeapon().IsTypeOfShield())
                flatDmgNegate *= (sourceEquipment.GetRWeapon().ArmorPierce);
            
            flatDmgNegate /= hit.Data.Ability.Data.ArmorPierceMod;
            dmgToApply -= flatDmgNegate;
            if (dmgToApply < 0)
                dmgToApply = 0;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Head))
            {
                if (targetEquipment.GetHelm() != null)
                    dmgToApply *= (targetEquipment.GetHelm().DamageReduction * dmgReduction * hit.Data.Ability.Data.ArmorIgnoreMod);
            }
            else
            {
                if (targetEquipment.GetArmor() != null)
                    dmgToApply *= (targetEquipment.GetArmor().DamageReduction * dmgReduction * hit.Data.Ability.Data.ArmorIgnoreMod);
            }
            hit.Data.Dmg = (int)dmgToApply;
        }

        private void ProcessShieldBlock(Hit hit)
        {
            // TODO:
            hit.Data.Dmg = 0;
        }
    }
}
