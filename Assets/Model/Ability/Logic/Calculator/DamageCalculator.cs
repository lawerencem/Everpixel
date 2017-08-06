using Assets.Model.Ability.Enum;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat;
using Model.Abilities;
using Model.Characters;
using Model.Combat;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DamageCalculator : AAbilityCalculator
    {
        public void CalculateAbilityDmg(Hit hit)
        {
            var dmg = hit.ModData.BaseDamage;
            dmg += hit.Ability.FlatDamage;
            if (hit.Ability.CastType == ECastType.Melee)
            {
                if (hit.Source.Model.RWeapon != null)
                    dmg += hit.Source.Model.RWeapon.Damage;
                if (hit.Source.Model.LWeapon != null)
                    dmg += hit.Source.Model.LWeapon.Damage;
            }
            dmg += (hit.Ability.DmgPerPower * hit.Source.Model.GetCurrentStatValue(ESecondaryStat.Power));
            dmg *= hit.Ability.DamageMod;
            hit.Dmg = (int)dmg;
        }

        public void ModifyDmgViaDefender(Hit hit)
        {
            if (!FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Dodge) &&
                !FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Parry))
            {
                // TODO:
                //if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Block))
                    //ProcessShieldBlock(hit);
                //else
                    //ProcessDamage(hit);
            }
            else
            {
                hit.Dmg = 0;
            }
        }

        public override void Predict(Hit hit)
        {
            this.CalculateAbilityDmg(hit);
            var dmgReduction = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Damage_Reduction);
            var bodyReduction = dmgReduction;
            var headReduction = dmgReduction;
            double flatDmgNegate = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Damage_Ignore);
            double bodyDmgNegate = 0;
            double headDmgNegate = 0;

            if (hit.Target.Model.Armor != null)
            {
                bodyDmgNegate += hit.Target.Model.Armor.DamageIgnore;
                bodyReduction *= hit.Target.Model.Armor.DamageReduction;
            }
            if (hit.Target.Model.Helm != null)
            {
                headDmgNegate += hit.Target.Model.Helm.DamageIgnore;
                headReduction *= hit.Target.Model.Helm.DamageReduction;
            }

            if (hit.Source.Model.LWeapon != null && !hit.Source.Model.LWeapon.IsTypeOfShield())
                flatDmgNegate *= hit.Source.Model.LWeapon.ArmorPierce;
            if (hit.Source.Model.RWeapon != null && !hit.Source.Model.RWeapon.IsTypeOfShield())
                flatDmgNegate *= hit.Source.Model.RWeapon.ArmorPierce;
            foreach (var perk in hit.Target.Model.Perks.WhenHitPerks)
                perk.TryModHit(hit);

            var bodyWeight = ((hit.Dmg - flatDmgNegate - bodyDmgNegate) * bodyReduction) * LogicParams.BASE_BODY_RATIO;
            var headWeight = ((hit.Dmg - flatDmgNegate - headDmgNegate) * headReduction);
            hit.Chances.Damage = (bodyWeight + headWeight) / (LogicParams.BASE_HIT_RATIO + 1);
        }

        public override void Process(Hit hit)
        {
            var dmgToApply = (double)hit.Dmg;
            if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Critical))
                dmgToApply *= (LogicParams.BASE_CRIT_SCALAR + (hit.Source.Model.GetCurrentStatValue(ESecondaryStat.Critical_Multiplier) / LogicParams.BASE_SCALAR));
            var dmgReduction = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Damage_Reduction);
            double flatDmgNegate = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.Damage_Ignore);
            if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Head))
            {
                if (hit.Target.Model.Helm != null)
                    flatDmgNegate += hit.Target.Model.Helm.DamageIgnore;
            }
            else
            {
                if (hit.Target.Model.Armor != null)
                    flatDmgNegate += hit.Target.Model.Armor.DamageIgnore;
            }
            if (hit.Source.Model.LWeapon != null && !hit.Source.Model.LWeapon.IsTypeOfShield())
                flatDmgNegate *= (hit.Source.Model.LWeapon.ArmorPierce);
            if (hit.Source.Model.RWeapon != null && !hit.Source.Model.RWeapon.IsTypeOfShield())
                flatDmgNegate *= (hit.Source.Model.RWeapon.ArmorPierce);
            flatDmgNegate /= hit.Ability.ArmorPierceMod;
            dmgToApply -= flatDmgNegate;
            if (dmgToApply < 0)
                dmgToApply = 0;
            if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Head))
            {
                if (hit.Target.Model.Helm != null)
                    dmgToApply *= (hit.Target.Model.Helm.DamageReduction * dmgReduction * hit.Ability.ArmorIgnoreMod);
            }
            else
            {
                if (hit.Target.Model.Armor != null)
                    dmgToApply *= (hit.Target.Model.Armor.DamageReduction * dmgReduction * hit.Ability.ArmorIgnoreMod);
                hit.Dmg = (int)dmgToApply;
            }
            foreach (var perk in hit.Target.Model.Perks.WhenHitPerks)
                perk.TryModHit(hit);
            hit.Dmg = (int)dmgToApply;
        }
    }
}
