using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Equipment.Enum;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DamageCalculator : AAbilityCalculator
    {
        public void CalculateAbilityDmg(MHit hit)
        {
            var hitData = hit.Data;
            var abilityData = hitData.Ability.Data;
            var source = hit.Data.Source.Proxy;

            var dmg = hit.Data.ModData.BaseDamage;
            dmg += abilityData.FlatDamage;
            if (source.GetRWeapon() != null && !source.GetRWeapon().IsTypeOfShield())
                dmg += source.GetRWeapon().GetStat(EWeaponStat.Damage);
            if (source.GetLWeapon() != null && !source.GetLWeapon().IsTypeOfShield())
                dmg += source.GetLWeapon().GetStat(EWeaponStat.Damage);
            dmg += (abilityData.DmgPerPower * source.GetStat(ESecondaryStat.Power));
            dmg *= abilityData.DamageMod;
            hit.Data.Dmg = (int)dmg;
        }

        public void ModifyDmgViaDefender(MHit hit)
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

        public override void Predict(MHit hit)
        {
            this.CalculateAbilityDmg(hit);

            if (hit.Data.Target.Current != null && hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                var tgt = hit.Data.Target.Current as CharController;
                var dmgReduction = tgt.Proxy.GetStat(ESecondaryStat.Damage_Reduction);
                var bodyReduction = dmgReduction;
                var headReduction = dmgReduction;
                double flatDmgNegate = tgt.Proxy.GetStat(ESecondaryStat.Damage_Ignore);
                double bodyDmgNegate = 0;
                double headDmgNegate = 0;

                if (tgt.Proxy.GetArmor() != null)
                {
                    bodyDmgNegate += tgt.Proxy.GetArmor().GetStat(EArmorStat.Flat_Damage_Ignore);
                    bodyReduction *= tgt.Proxy.GetArmor().GetStat(EArmorStat.Damage_Mod);
                }
                if (tgt.Proxy.GetHelm() != null)
                {
                    headDmgNegate += tgt.Proxy.GetHelm().GetStat(EArmorStat.Flat_Damage_Ignore);
                    headReduction *= tgt.Proxy.GetHelm().GetStat(EArmorStat.Damage_Mod);
                }

                if (tgt.Proxy.GetLWeapon() != null && !tgt.Proxy.GetLWeapon().IsTypeOfShield())
                    flatDmgNegate *= tgt.Proxy.GetLWeapon().GetStat(EWeaponStat.Armor_Pierce);
                if (tgt.Proxy.GetRWeapon() != null && !tgt.Proxy.GetRWeapon().IsTypeOfShield())
                    flatDmgNegate *= tgt.Proxy.GetRWeapon().GetStat(EWeaponStat.Armor_Pierce);
                foreach (var perk in tgt.Proxy.GetPerks().GetWhenHitPerks())
                    perk.TryModHit(hit);

                var bodyWeight = ((hit.Data.Dmg - flatDmgNegate - bodyDmgNegate) * bodyReduction) * LogicParams.BASE_BODY_MULTIPLIER;
                var headWeight = ((hit.Data.Dmg - flatDmgNegate - headDmgNegate) * headReduction);
                hit.Data.Chances.Damage = (bodyWeight + headWeight) / (LogicParams.BASE_HIT_RATIO);
                if (hit.Data.Chances.Damage < 0) { hit.Data.Chances.Damage = 0; }
            }
        }

        public override void Process(MHit hit)
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
                    flatDmgNegate += target.GetHelm().GetStat(EArmorStat.Flat_Damage_Ignore);
            }
            else
            {
                if (target.GetArmor() != null)
                    flatDmgNegate += target.GetArmor().GetStat(EArmorStat.Flat_Damage_Ignore);
            }
            if (source.GetLWeapon() != null && !source.GetLWeapon().IsTypeOfShield())
                flatDmgNegate *= (source.GetLWeapon().GetStat(EWeaponStat.Armor_Pierce));
            if (source.GetRWeapon() != null && !source.GetRWeapon().IsTypeOfShield())
                flatDmgNegate *= (source.GetRWeapon().GetStat(EWeaponStat.Armor_Pierce));
            
            flatDmgNegate /= hit.Data.Ability.Data.ArmorPierceMod;
            flatDmgNegate *= hit.Data.ModData.SrcArmorPierceMod;

            dmgToApply -= flatDmgNegate;
            if (dmgToApply < 0)
                dmgToApply = 0;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Head))
            {
                if (target.GetHelm() != null)
                    dmgToApply *= 
                        (target.GetHelm().GetStat(EArmorStat.Damage_Mod) * 
                        dmgReduction * 
                        hit.Data.Ability.Data.ArmorIgnoreMod *
                        hit.Data.ModData.SrcArmorIgnoreMod);
            }
            else
            {
                if (target.GetArmor() != null)
                    dmgToApply *=
                        (target.GetArmor().GetStat(EArmorStat.Damage_Mod) *
                        dmgReduction *
                        hit.Data.Ability.Data.ArmorIgnoreMod *
                        hit.Data.ModData.SrcArmorIgnoreMod);
            }
            hit.Data.Dmg = (int)dmgToApply;
        }

        private void ProcessShieldBlock(MHit hit)
        {
            // TODO:
            hit.Data.Dmg = 0;
        }
    }
}
