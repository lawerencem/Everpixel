using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DamageCalculator : AAbilityCalculator
    {
        public void CalculateAbilityDmg(Hit hit)
        {
            var hitData = hit.Data;
            var abilityData = hitData.Ability.Data;
            var source = hit.Data.Source.Proxy;

            var dmg = hit.Data.ModData.BaseDamage;
            dmg += abilityData.FlatDamage;
            if (abilityData.CastType == ECastType.Melee)
            {
                if (source.GetRWeapon() != null && !source.GetRWeapon().IsTypeOfShield())
                    dmg += source.GetRWeapon().Damage;
                if (source.GetLWeapon() != null && !source.GetLWeapon().IsTypeOfShield())
                    dmg += source.GetLWeapon().Damage;
            }
            dmg += (abilityData.DmgPerPower * source.GetStat(ESecondaryStat.Power));
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
            var source = hit.Data.Source.Proxy;
            var targetController = hit.Data.Target.Current as CharController;
            var target = targetController.Proxy;

            var dmgToApply = (double)hit.Data.Dmg;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Critical))
                dmgToApply *= 
                    (LogicParams.BASE_CRIT_SCALAR + 
                        (source.GetStat(ESecondaryStat.Critical_Multiplier) / 
                        LogicParams.BASE_SCALAR));
            var dmgReduction = target.GetStat(ESecondaryStat.Damage_Reduction);
            double flatDmgNegate = source.GetStat(ESecondaryStat.Damage_Ignore);
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Head))
            {
                if (target.GetHelm() != null)
                    flatDmgNegate += target.GetHelm().DamageIgnore;
            }
            else
            {
                if (target.GetArmor() != null)
                    flatDmgNegate += target.GetArmor().DamageIgnore;
            }
            if (source.GetLWeapon() != null && !source.GetLWeapon().IsTypeOfShield())
                flatDmgNegate *= (source.GetLWeapon().ArmorPierce);
            if (source.GetRWeapon() != null && !source.GetRWeapon().IsTypeOfShield())
                flatDmgNegate *= (source.GetRWeapon().ArmorPierce);
            
            flatDmgNegate /= hit.Data.Ability.Data.ArmorPierceMod;
            dmgToApply -= flatDmgNegate;
            if (dmgToApply < 0)
                dmgToApply = 0;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Head))
            {
                if (target.GetHelm() != null)
                    dmgToApply *= 
                        (target.GetHelm().DamageReduction * 
                        dmgReduction * 
                        hit.Data.Ability.Data.ArmorIgnoreMod);
            }
            else
            {
                if (target.GetArmor() != null)
                    dmgToApply *= 
                        (target.GetArmor().DamageReduction * 
                        dmgReduction * 
                        hit.Data.Ability.Data.ArmorIgnoreMod);
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
