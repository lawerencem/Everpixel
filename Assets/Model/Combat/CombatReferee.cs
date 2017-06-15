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
        private const double BASE_CHANCE = 1.0;
        private const double BASE_BLOCK_CHANCE = 0.25;
        private const double BASE_CRIT_CHANCE = 0.15;
        private const double BASE_CRIT_SCALAR = 1.5;
        private const double BASE_DODGE_CHANCE = 0.20;
        private const double BASE_HEAD_CHANCE = 0.25;
        private const double BASE_PARRY_CHANCE = 0.15;
        private const double BASE_SKILL_SCALAR = 0.75;
        private const double BASE_SCALAR = 1000;

        private Callback _callBack;
        public delegate void Callback();

        private HitInfo _hit;

        public void ProcessBullet(HitInfo hit)
        {
            ProcessBulletFlags(hit);
            CalculateDamage(hit);
            ProcessHitEventView(hit);
        }

        public void ProcessMelee(HitInfo hit)
        {
            ProcessMeleeFlags(hit);
            CalculateDamage(hit);
            ProcessHitEventView(hit);
        }

        private void CalculateDamage(HitInfo hit)
        {
            var dmg = hit.Ability.ModData.BaseDamage;

            if (hit.Source.Model.RWeapon != null)
                dmg += hit.Source.Model.RWeapon.Damage;
            if (hit.Source.Model.LWeapon != null)
                dmg += hit.Source.Model.LWeapon.Damage;
            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
                dmg *= (BASE_CRIT_SCALAR + (hit.Source.Model.SecondaryStats.CriticalMultiplier / BASE_SCALAR));

            dmg *= (BASE_SKILL_SCALAR + (hit.Source.Model.SecondaryStats.Power / BASE_SCALAR));

            hit.Dmg = (int)dmg;
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
        }

        private void ProcessHitEventView(HitInfo hit)
        {
            var guiEvent = new DisplayHitStatsEvent(CombatEventManager.Instance, hit, hit.Done);
        }

        private void ProcessBulletFlags(HitInfo hit)
        {
            this.ProcessDodge(hit);
            this.ProcessBlock(hit);
            this.ProcessCrit(hit);
            this.ProcessHeadShot(hit);
        }

        private void ProcessMeleeFlags(HitInfo hit)
        {
            this.ProcessDodge(hit);
            this.ProcessParry(hit);
            this.ProcessBlock(hit);
            this.ProcessCrit(hit);
            this.ProcessHeadShot(hit);
        }

        private void ProcessDamage(HitInfo hit)
        {
            var dmgReduction = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Damage_Reduction);

            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Head))
            {
                double flatDmgNegate = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Damage_Ignore);
                if (hit.Target.Model.Helm != null)
                    flatDmgNegate = hit.Target.Model.Helm.DamageIgnore;
                if (hit.Source.Model.LWeapon != null)
                    flatDmgNegate *= hit.Source.Model.LWeapon.ArmorPierce;
                if (hit.Source.Model.RWeapon != null)
                    flatDmgNegate *= hit.Source.Model.RWeapon.ArmorPierce;

                double dmgToApply = (hit.Dmg - flatDmgNegate);
                if (dmgToApply < 0)
                    dmgToApply = 0;

                if (hit.Target.Model.Helm != null)
                    dmgToApply *= (hit.Target.Model.Helm.DamageReduction * dmgReduction);
                hit.Dmg = (int)dmgToApply;
            }
            else
            {
                double flatDmgNegate = 0;
                if (hit.Target.Model.Armor != null)
                    flatDmgNegate = hit.Target.Model.Armor.DamageIgnore;
                if (hit.Source.Model.LWeapon != null)
                    flatDmgNegate *= hit.Source.Model.LWeapon.ArmorPierce;
                if (hit.Source.Model.RWeapon != null)
                    flatDmgNegate *= hit.Source.Model.RWeapon.ArmorPierce;

                double dmgToApply = (hit.Dmg - flatDmgNegate);

                if (hit.Target.Model.Armor != null)
                    dmgToApply *= (hit.Target.Model.Helm.DamageReduction * dmgReduction);
                hit.Dmg = (int)dmgToApply;
            }
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
                this.ProcessDamage(hit);
            }
            else
                AttackEventFlags.SetBlockFalse(hit.Flags);
        }

        private double GetAttackVSDefenseSkillChance(double attackSkill, double defenseSkill, double baseDefenseChance)
        {
            double scalar = 1;
            double diff = attackSkill - defenseSkill;

            if (diff > 0)
                scalar = (1 - (diff / BASE_SCALAR));
            else
                scalar = (1 + ((diff *= -1) / BASE_SCALAR));

            return (BASE_CHANCE - (baseDefenseChance * scalar));
        }

        private void ProcessDodge(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var dodge = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Dodge);
            var dodgeChance = BASE_DODGE_CHANCE / hit.Ability.AccMod;

            if (hit.Target.Model.Armor != null)
                dodgeChance *= hit.Target.Model.Armor.DodgeMod;
            if (hit.Target.Model.Helm != null)
                dodgeChance *= hit.Target.Model.Helm.DodgeMod;

            var chance = this.GetAttackVSDefenseSkillChance(melee, dodge, dodgeChance);
            
            chance *= hit.Ability.DodgeMod;
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetDodgeTrue(hit.Flags);
        }

        private void ProcessParry(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
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

            var chance = this.GetAttackVSDefenseSkillChance(melee, parry, parryChance);
            
            chance *= hit.Ability.ParryModMod;
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetParryTrue(hit.Flags);
        }

        private void ProcessBlock(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var block = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Block);
            var blockChance = BASE_BLOCK_CHANCE / hit.Ability.AccMod;

            bool hasShield = false;

            if (hit.Target.Model.Armor != null)
                blockChance *= hit.Target.Model.Armor.BlockReduce;
            if (hit.Target.Model.Helm != null)
                blockChance *= hit.Target.Model.Helm.BlockReduce;
            if (hit.Target.Model.LWeapon != null && hit.Target.Model.LWeapon.IsTypeOfShield())
                blockChance *= (BASE_SKILL_SCALAR + (hit.Target.Model.LWeapon.MeleeBlockChance / BASE_SCALAR));
            if (hit.Target.Model.RWeapon != null && hit.Target.Model.RWeapon.IsTypeOfShield())
                blockChance *= (BASE_SKILL_SCALAR + (hit.Target.Model.RWeapon.MeleeBlockChance / BASE_SCALAR));

            if (!hasShield) { blockChance = 0; }
            var chance = this.GetAttackVSDefenseSkillChance(melee, block, BASE_BLOCK_CHANCE);
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetBlockTrue(hit.Flags);
        }

        private void ProcessCrit(HitInfo hit)
        {
            var melee = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var crit = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Critical_Chance);

            var chance = this.GetAttackVSDefenseSkillChance(melee, crit, BASE_CRIT_CHANCE);

            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetCritTrue(hit.Flags);
        }

        private void ProcessHeadShot(HitInfo hit)
        {
            var roll = RNG.Instance.NextDouble();
            if (roll > BASE_HEAD_CHANCE)
                AttackEventFlags.SetHeadTrue(hit.Flags);
        }

        private void ProcessRangedBlock(HitInfo hit)
        {
            var ranged = hit.Source.Model.GetCurrentStatValue(SecondaryStatsEnum.Ranged);
            var block = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.Block);
            var blockChance = BASE_BLOCK_CHANCE / hit.Ability.AccMod;

            bool hasShield = false;

            if (hit.Target.Model.Armor != null)
                blockChance *= hit.Target.Model.Armor.BlockReduce;
            if (hit.Target.Model.Helm != null)
                blockChance *= hit.Target.Model.Helm.BlockReduce;
            if (hit.Target.Model.LWeapon != null && hit.Target.Model.LWeapon.IsTypeOfShield())
                blockChance *= (BASE_SKILL_SCALAR + (hit.Target.Model.LWeapon.MeleeBlockChance / BASE_SCALAR));
            if (hit.Target.Model.RWeapon != null && hit.Target.Model.RWeapon.IsTypeOfShield())
                blockChance *= (BASE_SKILL_SCALAR + (hit.Target.Model.RWeapon.MeleeBlockChance / BASE_SCALAR));

            if (!hasShield) { blockChance = 0; }
            var chance = this.GetAttackVSDefenseSkillChance(ranged, block, BASE_BLOCK_CHANCE);
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetBlockTrue(hit.Flags);
        }
    }
}
