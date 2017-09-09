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
            if (source.GetRWeapon() != null && !source.GetRWeapon().IsTypeOfShield())
                dmg += source.GetRWeapon().Damage;
            if (source.GetLWeapon() != null && !source.GetLWeapon().IsTypeOfShield())
                dmg += source.GetLWeapon().Damage;
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
                    bodyDmgNegate += tgt.Proxy.GetArmor().FlatDamageIgnore;
                    bodyReduction *= tgt.Proxy.GetArmor().DamageMod;
                }
                if (tgt.Proxy.GetHelm() != null)
                {
                    headDmgNegate += tgt.Proxy.GetHelm().DamageIgnore;
                    headReduction *= tgt.Proxy.GetHelm().DamageMod;
                }

                if (tgt.Proxy.GetLWeapon() != null && !tgt.Proxy.GetLWeapon().IsTypeOfShield())
                    flatDmgNegate *= tgt.Proxy.GetLWeapon().ArmorPierce;
                if (tgt.Proxy.GetRWeapon() != null && !tgt.Proxy.GetRWeapon().IsTypeOfShield())
                    flatDmgNegate *= tgt.Proxy.GetRWeapon().ArmorPierce;
                foreach (var perk in tgt.Proxy.GetPerks().GetWhenHitPerks())
                    perk.TryModHit(hit);

                var bodyWeight = ((hit.Data.Dmg - flatDmgNegate - bodyDmgNegate) * bodyReduction) * LogicParams.BASE_BODY_MULTIPLIER;
                var headWeight = ((hit.Data.Dmg - flatDmgNegate - headDmgNegate) * headReduction);
                hit.Data.Chances.Damage = (bodyWeight + headWeight) / (LogicParams.BASE_HIT_RATIO);
                if (hit.Data.Chances.Damage < 0) { hit.Data.Chances.Damage = 0; }
            }
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
                    flatDmgNegate += target.GetArmor().FlatDamageIgnore;
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
                        (target.GetHelm().DamageMod * 
                        dmgReduction * 
                        hit.Data.Ability.Data.ArmorIgnoreMod);
            }
            else
            {
                if (target.GetArmor() != null)
                    dmgToApply *= 
                        (target.GetArmor().DamageMod * 
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
