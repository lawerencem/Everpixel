using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Equipment.Enum;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DamageCalculator : AAbilityCalculator
    {
        public void CalculateAbilityDmg(MHit hit)
        {
            if (hit.Data.Ability.Data.ProcessDamage)
            {
                var hitData = hit.Data;
                var abilityData = hitData.Ability.Data;
                var source = hit.Data.Source.Proxy;

                var dmg = hit.Data.ModData.BaseDamage;
                dmg += abilityData.FlatDamage;
                dmg += (abilityData.DmgPerPower * source.GetStat(ESecondaryStat.Power));

                if (source.GetLWeapon() != null && hit.Data.IsWeapon && hit.Data.IsLWeapon)
                {
                    dmg += source.GetLWeapon().GetStat(EWeaponStat.Damage);
                    dmg *= source.GetLWeapon().GetDurabilityPercentage();
                }
                else if (source.GetRWeapon() != null && hit.Data.IsWeapon)
                {
                    dmg += source.GetRWeapon().GetStat(EWeaponStat.Damage);
                    dmg *= source.GetRWeapon().GetDurabilityPercentage();
                }
                dmg *= abilityData.DamageMod;
                hit.Data.Dmg = (int)dmg;
            }
            else
                hit.Data.Dmg = 0;
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
            this.PredictWpnAndArmorValues(hit);
            this.PredictPerks(hit);
        }

        public override void Process(MHit hit)
        {
            if (hit.Data.Ability.Data.ProcessDamage)
            {
                var src = hit.Data.Source.Proxy;
                var targetController = hit.Data.Target.Current as CChar;
                var tgt = targetController.Proxy;

                double dmgToApply = this.GetCritDamage(hit, (double)hit.Data.Dmg);
                double dmgReduction = tgt.GetStat(ESecondaryStat.Damage_Reduction);
                double flatDmgNegate = src.GetStat(ESecondaryStat.Damage_Ignore);

                if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Head))
                {
                    flatDmgNegate += this.GetArmorNegation(hit, src, tgt, flatDmgNegate, true);
                    dmgReduction = this.GetDmgReduction(hit, src, tgt, dmgReduction, true);
                }
                else
                {
                    flatDmgNegate += this.GetArmorNegation(hit, src, tgt, flatDmgNegate, false);
                    dmgReduction = this.GetDmgReduction(hit, src, tgt, dmgReduction, false);
                }
                dmgToApply -= flatDmgNegate;
                dmgToApply *= dmgReduction;
                hit.Data.Dmg = (int)dmgToApply;
            }
        }

        private double GetCritDamage(MHit hit, double dmgIn)
        {
            double dmgOut = dmgIn;
            if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Critical))
                dmgOut *=
                    (LogicParams.BASE_CRIT_SCALAR +
                        (hit.Data.Source.Proxy.GetStat(ESecondaryStat.Critical_Multiplier) /
                        LogicParams.BASE_SCALAR));
            return dmgOut;
        }

        private void PredictWpnAndArmorValues(MHit hit)
        {
            if (hit.Data.Target.Current != null && hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var tgt = hit.Data.Target.Current as CChar;
                double dmgReduction = tgt.Proxy.GetStat(ESecondaryStat.Damage_Reduction);
                double bodyReduction = this.GetDmgReduction(hit, hit.Data.Source.Proxy, tgt.Proxy, dmgReduction, false);
                double headReduction = this.GetDmgReduction(hit, hit.Data.Source.Proxy, tgt.Proxy, dmgReduction, true); ;
                double baseDmgNegation = tgt.Proxy.GetStat(ESecondaryStat.Damage_Ignore);
                double bodyDmgNegate = this.GetArmorNegation(hit, hit.Data.Source.Proxy, tgt.Proxy, baseDmgNegation, false);
                double headDmgNegate = this.GetArmorNegation(hit, hit.Data.Source.Proxy, tgt.Proxy, baseDmgNegation, true);
                double bodyWeight = ((hit.Data.Dmg - baseDmgNegation - bodyDmgNegate) * bodyReduction) * LogicParams.BASE_BODY_MULTIPLIER;
                double headWeight = ((hit.Data.Dmg - baseDmgNegation - headDmgNegate) * headReduction);
                hit.Data.Chances.Damage = (bodyWeight + headWeight) / (LogicParams.BASE_HIT_RATIO);
                if (hit.Data.Chances.Damage < 0) { hit.Data.Chances.Damage = 0; }
            }
        }

        private void PredictPerks(MHit hit)
        {
            var tgt = hit.Data.Target.Current as CChar;
            foreach (var perk in tgt.Proxy.GetPerks().GetWhenHitPerks())
                perk.TryModHit(hit);
        }

        private double GetArmorNegation(MHit hit, PChar src, PChar tgt, double baseNegation, bool helm)
        {
            double negation = baseNegation;

            if (helm && tgt.GetHelm() != null)
            {
                negation += tgt.GetHelm().GetStat(EArmorStat.Flat_Damage_Ignore);
                negation *= tgt.GetHelm().GetDurabilityPercentage();
            }
            else if (!helm && tgt.GetArmor() != null)
            {
                negation += tgt.GetArmor().GetStat(EArmorStat.Flat_Damage_Ignore);
                negation *= tgt.GetArmor().GetDurabilityPercentage();
            }
            if (hit.Data.IsWeapon)
            {
                double modifier = 1;
                if (hit.Data.IsLWeapon)
                    modifier *= (src.GetLWeapon().GetStat(EWeaponStat.Armor_Pierce) / src.GetLWeapon().GetDurabilityPercentage());
                else
                    modifier *= (src.GetRWeapon().GetStat(EWeaponStat.Armor_Pierce) / src.GetRWeapon().GetDurabilityPercentage());
                if (modifier > 1)
                    modifier = 1;
                negation *= modifier;
            }
            negation /= hit.Data.Ability.Data.ArmorPierceMod;
            negation *= hit.Data.ModData.SrcArmorPierceMod;
            return negation;
        }

        private double GetDmgReduction(MHit hit, PChar src, PChar tgt, double baseReduce, bool helm)
        {
            double reduction = baseReduce;
            if (helm && tgt.GetHelm() != null)
                reduction *= tgt.GetHelm().GetStat(EArmorStat.Damage_Mod) / tgt.GetHelm().GetDurabilityPercentage();
            else if (!helm && tgt.GetArmor() != null)
                reduction *= tgt.GetArmor().GetStat(EArmorStat.Damage_Mod) / tgt.GetArmor().GetDurabilityPercentage();
            if (hit.Data.IsWeapon)
            {
                double modifier = 1;
                if (hit.Data.IsLWeapon)
                    modifier *= (src.GetLWeapon().GetStat(EWeaponStat.Armor_Ignore) / src.GetLWeapon().GetDurabilityPercentage());
                else
                    modifier *= (src.GetRWeapon().GetStat(EWeaponStat.Armor_Ignore) / src.GetRWeapon().GetDurabilityPercentage());
                if (modifier > 1)
                    modifier = 1;
                reduction /= modifier;
            }
            reduction *= hit.Data.Ability.Data.ArmorIgnoreMod;
            reduction *= hit.Data.ModData.SrcArmorIgnoreMod;
            if (reduction > 1)
                reduction = 1;
            return reduction;
        }

        private double GetHitDamageIgnore(MHit hit, PChar src, PChar tgt)
        {
            double flatDmgNegate = tgt.GetStat(ESecondaryStat.Damage_Ignore);
            if (hit.Data.IsWeapon)
            {
                double modifier = 1;
                if (hit.Data.IsLWeapon)
                    modifier *= (src.GetLWeapon().GetStat(EWeaponStat.Armor_Pierce) / src.GetLWeapon().GetDurabilityPercentage());
                else
                    modifier *= (src.GetRWeapon().GetStat(EWeaponStat.Armor_Pierce) / src.GetRWeapon().GetDurabilityPercentage());
                if (modifier > 1)
                    modifier = 1;
                flatDmgNegate *= modifier;
            }
            return flatDmgNegate;
        }

        private void ProcessShieldBlock(MHit hit)
        {
            // TODO:
            hit.Data.Dmg = 0;
        }
    }
}
