using Controller.Managers;
using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;
using Model.Events.Combat;

namespace Model.Combat
{
    public class CombatReferee : AbstractSingleton<CombatReferee>
    {
        private const double BASE_BLOCK_CHANCE = 0.25;
        private const double BASE_BODY_RATIO = BASE_HIT_RATIO - 1;
        private const double BASE_CHANCE = 1.0;
        private const double BASE_CRIT_CHANCE = 0.15;
        private const double BASE_CRIT_SCALAR = 1.5;
        private const double BASE_DODGE_CHANCE = 0.20;
        private const double BASE_HEAD_CHANCE = 0.25;
        private const double BASE_HIT_RATIO = 1 / BASE_HEAD_CHANCE;
        private const double BASE_PARRY_CHANCE = 0.15;
        private const double BASE_RESIST = 0.15;
        private const double BASE_SCALAR = 1000;
        private const double BASE_SKILL_SCALAR = 0.75;

        public void PredictBullet(HitInfo hit)
        {
            this.PredictDodge(hit);
            this.PredictBlock(hit);
            this.PredictCrit(hit);
            this.PredictDmg(hit);
            this.PredictResist(hit);
        }

        public void PredictMelee(HitInfo hit)
        {
            this.PredictDodge(hit);
            this.PredictBlock(hit);
            this.PredictCrit(hit);
            this.PredictParry(hit);
            this.PredictDmg(hit);
            this.PredictResist(hit);
        }

        public void PredictRay(HitInfo hit)
        {
            this.PredictDodge(hit);
            this.PredictBlock(hit);
            this.PredictCrit(hit);
            this.PredictDmg(hit);
            this.PredictResist(hit);
        }

        public void ProcessBullet(HitInfo hit)
        {
            this.ProcessBulletFlags(hit);
            this.CalculateAbilityDmg(hit);
            this.ModifyDmgViaDefender(hit);
        }

        public void ProcessMelee(HitInfo hit)
        {
            this.ProcessMeleeFlags(hit);
            this.CalculateAbilityDmg(hit);
            this.ModifyDmgViaDefender(hit);
        }

        public void ProcessRay(HitInfo hit)
        {
            this.ProcessBulletFlags(hit);
            this.CalculateAbilityDmg(hit);
            this.ModifyDmgViaDefender(hit);
        }

        public void ProcessShapeshift(HitInfo hit)
        {
            var shapeshiftEvent = new ShapeshiftEvent(CombatEventManager.Instance, hit);
        }

        public void ProcessSong(HitInfo hit)
        {

        }

        public void ProcessSummon(HitInfo hit)
        {
            var summonEvent = new SummonEvent(CombatEventManager.Instance, hit);
        }

        private void CalculateAbilityDmg(HitInfo hit)
        {
            var dmg = hit.ModData.BaseDamage;
            dmg += hit.Ability.FlatDamage;
            if (hit.Ability.CastType == CastTypeEnum.Melee)
            {
                if (hit.Source.Model.RWeapon != null)
                    dmg += hit.Source.Model.RWeapon.Damage;
                if (hit.Source.Model.LWeapon != null)
                    dmg += hit.Source.Model.LWeapon.Damage;
            }
            dmg += (hit.Ability.DmgPerPower * hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Power));
            dmg *= hit.Ability.DamageMod;
            hit.Dmg = (int)dmg;
        }

        private double GetAttackVSDefenseSkillChance(double attackSkill, double defenseSkill, double baseDefenseChance)
        {
            double scalar = 1;
            double diff = attackSkill - defenseSkill;

            if (diff > 0)
                scalar = (1 - (diff / BASE_SCALAR));
            else
                scalar = (1 + ((diff *= -1) / BASE_SCALAR));

            return (baseDefenseChance * scalar);
        }

        private void ModifyDmgViaDefender(HitInfo hit)
        {
            if (!AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge) &&
                !AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
            {
                if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                    ProcessShieldBlock(hit);
                else
                    ProcessDamage(hit);
            }
            else
            {
                hit.Dmg = 0;
            }
        }

        private void PredictBlock(HitInfo hit)
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
                hit.Chances.Block *= (BASE_SKILL_SCALAR + (hit.Target.Model.LWeapon.MeleeBlockChance / BASE_SCALAR));
                hasShield = true;
            }
            if (hit.Target.Model.RWeapon != null && hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                hit.Chances.Block *= (BASE_SKILL_SCALAR + (hit.Target.Model.RWeapon.MeleeBlockChance / BASE_SCALAR));
                hasShield = true;
            }
            hit.Chances.Block = this.GetAttackVSDefenseSkillChance(melee, block, hit.Chances.Block);
            hit.Chances.Block *= hit.ModData.BlockMod;

            if (!hasShield) { hit.Chances.Block = 0; }
        }

        private void PredictCrit(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var crit = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Critical_Chance);
            hit.Chances.Crit = this.GetAttackVSDefenseSkillChance(melee, crit, BASE_CRIT_CHANCE);
        }

        private void PredictDmg(HitInfo hit)
        {
            this.CalculateAbilityDmg(hit);
            var dmgReduction = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Damage_Reduction);
            var bodyReduction = dmgReduction;
            var headReduction = dmgReduction;
            double flatDmgNegate = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Damage_Ignore);
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

            var bodyWeight = ((hit.Dmg - flatDmgNegate - bodyDmgNegate) * bodyReduction) * BASE_BODY_RATIO;
            var headWeight = ((hit.Dmg - flatDmgNegate - headDmgNegate) * headReduction);
            hit.Chances.Damage = (bodyWeight + headWeight) / (BASE_HIT_RATIO + 1);
        }

        private void PredictDodge(HitInfo hit)
        {
            var acc = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var dodge = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Dodge);
            var dodgeChance = BASE_DODGE_CHANCE / hit.Ability.AccMod;

            if (hit.Target.Model.Armor != null)
                dodgeChance *= hit.Target.Model.Armor.DodgeMod;
            if (hit.Target.Model.Helm != null)
                dodgeChance *= hit.Target.Model.Helm.DodgeMod;

            if (hit.Target.Model.Type == CharacterTypeEnum.Critter)
                dodgeChance *= 1.75;

            acc *= hit.Ability.AccMod;

            hit.Chances.Dodge = this.GetAttackVSDefenseSkillChance(acc, dodge, dodgeChance);
            hit.Chances.Dodge *= hit.Ability.DodgeMod;
        }

        private void PredictParry(HitInfo hit)
        {
            var acc = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var parry = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Parry);
            var parryChance = BASE_PARRY_CHANCE / hit.Ability.AccMod;

            if (hit.Target.Model.Armor != null)
                parryChance *= hit.Target.Model.Armor.ParryReduce;
            if (hit.Target.Model.Helm != null)
                parryChance *= hit.Target.Model.Helm.ParryReduce;
            if (hit.Target.Model.LWeapon != null)
                parryChance *= hit.Target.Model.LWeapon.ParryMod;
            if (hit.Target.Model.RWeapon != null)
                parryChance *= hit.Target.Model.RWeapon.ParryMod;

            if (hit.Target.Model.Type == CharacterTypeEnum.Critter)
                parryChance = 0;

            acc *= hit.Ability.AccMod;
            parry *= hit.Ability.ParryModMod;

            hit.Chances.Parry = this.GetAttackVSDefenseSkillChance(acc, parry, parryChance);
        }

        private void PredictResist(HitInfo hit)
        {
            if (hit.Ability.Resist != ResistTypeEnum.None)
            {
                var attack = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Spell_Penetration);
                switch (hit.Ability.Resist)
                {
                    case (ResistTypeEnum.Fortitude):
                        {
                            var defense = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Fortitude);
                            hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, BASE_RESIST);
                        }
                        break;
                    case (ResistTypeEnum.Reflex):
                        {
                            var defense = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Reflex);
                            hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, BASE_RESIST);
                        }
                        break;
                    case (ResistTypeEnum.Will):
                        {
                            var defense = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Will);
                            hit.Chances.Resist = this.GetAttackVSDefenseSkillChance(attack, defense, BASE_RESIST);
                        }
                        break;
                }
            }
        }

        private void ProcessBulletFlags(HitInfo hit)
        {
            this.ProcessDodge(hit);
            this.ProcessBlock(hit);
            this.ProcessCrit(hit);
            this.ProcessHeadShot(hit);
            this.ProcessResist(hit);
        }

        private void ProcessMeleeFlags(HitInfo hit)
        {
            this.ProcessDodge(hit);
            this.ProcessParry(hit);
            this.ProcessBlock(hit);
            this.ProcessCrit(hit);
            this.ProcessHeadShot(hit);
            this.ProcessResist(hit);
        }

        private void ProcessBlock(HitInfo hit)
        {
            this.PredictBlock(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Block > roll)
                AttackEventFlags.SetBlockTrue(hit.Flags);
        }

        private void ProcessCrit(HitInfo hit)
        {
            this.PredictCrit(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Crit > roll)
                AttackEventFlags.SetCritTrue(hit.Flags);
        }

        private void ProcessDamage(HitInfo hit)
        {
            var dmgToApply = (double)hit.Dmg;
            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                dmgToApply *= (BASE_CRIT_SCALAR + (hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Critical_Multiplier) / BASE_SCALAR));
            var dmgReduction = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Damage_Reduction);
            double flatDmgNegate = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Damage_Ignore);
            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Head))
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
            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Head))
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

        private void ProcessDodge(HitInfo hit)
        {
            this.PredictDodge(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Dodge > roll)
                AttackEventFlags.SetDodgeTrue(hit.Flags);
        }

        private void ProcessParry(HitInfo hit)
        {
            this.PredictParry(hit);   
            var roll = RNG.Instance.NextDouble();
            if (hit.Chances.Parry > roll)
                AttackEventFlags.SetParryTrue(hit.Flags);
        }

        private void ProcessShieldBlock(HitInfo hit)
        {
            double ignoreDamageScalar = 1;
            bool hasShield = false;
            var shieldDmg = (hit.Dmg * hit.Ability.ShieldDamageMod);
            if (hit.Source.Model.LWeapon != null && !hit.Source.Model.LWeapon.IsTypeOfShield())
            {
                shieldDmg *= hit.Source.Model.LWeapon.ShieldDamage;
                ignoreDamageScalar *= hit.Source.Model.LWeapon.BlockIgnore;
            }
            if (hit.Source.Model.RWeapon != null && !hit.Source.Model.RWeapon.IsTypeOfShield())
            {
                shieldDmg *= hit.Source.Model.RWeapon.ShieldDamage;
                if (ignoreDamageScalar == 0)
                    ignoreDamageScalar += hit.Source.Model.RWeapon.BlockIgnore;
                else
                    ignoreDamageScalar *= hit.Source.Model.RWeapon.BlockIgnore;
            }
            if (hit.Target.Model.LWeapon != null && hit.Target.Model.LWeapon.IsTypeOfShield())
            {
                // TODO: event for shield damage
                hasShield = true;
            }
            if (hit.Target.Model.RWeapon != null && hit.Target.Model.RWeapon.IsTypeOfShield())
            {
                // TODO: event for shield damage
                hasShield = true;
            }
            if (hasShield)
            {
                hit.Dmg = (int)(ignoreDamageScalar * hit.Dmg);
            }
            else
                AttackEventFlags.SetBlockFalse(hit.Flags);
            this.ProcessDamage(hit);
        }

        private void ProcessHeadShot(HitInfo hit)
        {
            var roll = RNG.Instance.NextDouble();
            if (roll > BASE_HEAD_CHANCE)
                AttackEventFlags.SetHeadTrue(hit.Flags);
        }

        private void ProcessResist(HitInfo hit)
        {
            this.PredictResist(hit);
            if (hit.Ability.Resist != ResistTypeEnum.None)
            {
                var roll = RNG.Instance.NextDouble();
                if (roll < hit.Chances.Resist)
                    AttackEventFlags.SetResistTrue(hit.Flags);
            }
        }
    }
}
