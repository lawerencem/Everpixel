using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;

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

        public void ApplyDamage(int dmg, AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            if (!AttackEventFlags.HasFlag(f.CurFlags, AttackEventFlags.Flags.Dodge) &&
                !AttackEventFlags.HasFlag(f.CurFlags, AttackEventFlags.Flags.Parry))
            {
                if (AttackEventFlags.HasFlag(f.CurFlags, AttackEventFlags.Flags.Block))
                {
                    var shieldDmg = (dmg * a.ShieldDamageMod);
                    if (s.LWeapon != null) { shieldDmg *= s.LWeapon.ShieldDamage; }
                    if (s.RWeapon != null) { shieldDmg *= s.RWeapon.ShieldDamage; }
                    if (t.LWeapon != null && t.LWeapon.IsTypeOfShield())
                    {
                        // TODO: event for shield damage
                    }
                }
                else
                {
                    if (AttackEventFlags.HasFlag(f.CurFlags, AttackEventFlags.Flags.Head))
                    {
                        var flatDmgNegate = t.Helm.DamageIgnore;
                        if (s.LWeapon != null)
                            flatDmgNegate *= s.LWeapon.ArmorPierce;
                        if (s.RWeapon != null)
                            flatDmgNegate *= s.RWeapon.ArmorPierce;

                        double dmg2 = (dmg - flatDmgNegate);
                        
                        
                    }
                }
            }
        }

        public void ProcessMeleeFlags(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            this.ProcessDodge(f, a, s, t);
            this.ProcessParry(f, a, s, t);
            this.ProcessBlock(f, a, s, t);
            this.ProcessCrit(f, a, s, t);
            this.ProcessHeadShot(f, a, s, t);
        }

        public int ProcessDamage(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            double dmg = 0;

            if (s.RWeapon != null)
                dmg += s.RWeapon.Damage;
            if (s.LWeapon != null)
                dmg += s.LWeapon.Damage;
            if (AttackEventFlags.HasFlag(f.CurFlags, AttackEventFlags.Flags.Critical))
                dmg *= (BASE_CRIT_SCALAR + (s.SecondaryStats.CriticalMultiplier / BASE_SCALAR));

            dmg *= (BASE_SKILL_SCALAR + (s.SecondaryStats.Power / BASE_SCALAR));

            return (int)dmg;
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

        private void ProcessDodge(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var dodge = t.GetCurrentStatValue(SecondaryStatsEnum.Dodge);
            var dodgeChance = BASE_DODGE_CHANCE;

            if (t.Armor != null)
                dodgeChance *= t.Armor.DodgeMod;
            if (t.Helm != null)
                dodgeChance *= t.Helm.DodgeMod;

            var chance = this.GetAttackVSDefenseSkillChance(melee, dodge, dodgeChance);
            chance *= a.DodgeMod;
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetDodgeTrue(f);
        }

        private void ProcessParry(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var parry = t.GetCurrentStatValue(SecondaryStatsEnum.Parry);
            var parryChance = BASE_PARRY_CHANCE;

            if (t.Armor != null)
                parryChance *= t.Armor.ParryReduce;
            if (t.Helm != null)
                parryChance *= t.Helm.ParryReduce;
            if (t.LWeapon != null)
                parryChance *= t.LWeapon.ParryMod;
            if (t.RWeapon != null)
                parryChance *= t.RWeapon.ParryMod;

            var chance = this.GetAttackVSDefenseSkillChance(melee, parry, parryChance);
            chance *= a.ParryModMod;
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetParryTrue(f);
        }

        private void ProcessBlock(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var block = t.GetCurrentStatValue(SecondaryStatsEnum.Block);
            var blockChance = BASE_BLOCK_CHANCE;

            bool hasShield = false;

            if (t.Armor != null)
                blockChance *= t.Armor.BlockReduce;
            if (t.Helm != null)
                blockChance *= t.Helm.BlockReduce;
            if (t.LWeapon != null && t.LWeapon.IsTypeOfShield())
                blockChance *= (BASE_SKILL_SCALAR + (t.LWeapon.MeleeBlockChance / BASE_SCALAR));
            if (t.RWeapon != null && t.RWeapon.IsTypeOfShield())
                blockChance *= (BASE_SKILL_SCALAR + (t.RWeapon.MeleeBlockChance / BASE_SCALAR));

            if (!hasShield) { blockChance = 0; }
            var chance = this.GetAttackVSDefenseSkillChance(melee, block, BASE_BLOCK_CHANCE);
            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetBlockTrue(f);
        }

        private void ProcessCrit(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var crit = t.GetCurrentStatValue(SecondaryStatsEnum.Critical_Chance);

            var chance = this.GetAttackVSDefenseSkillChance(melee, crit, BASE_CRIT_CHANCE);

            var roll = RNG.Instance.NextDouble();
            if (chance < roll)
                AttackEventFlags.SetCritTrue(f);
        }

        private void ProcessHeadShot(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var roll = RNG.Instance.NextDouble();
            if (roll > BASE_HEAD_CHANCE)
                AttackEventFlags.SetHeadTrue(f);
        }
    }
}
