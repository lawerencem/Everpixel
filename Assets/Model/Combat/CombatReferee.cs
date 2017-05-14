using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;

namespace Model.Combat
{
    public class CombatReferee : AbstractSingleton<CombatReferee>
    {
        private const double BASE_CHANCE = 100;
        private const double BASE_DODGE_CHANCE = 0.02;
        private const double BASE_PARRY_CHANCE = 0.03;
        private const double BASE_BLOCK_CHANCE = 0.05;
        private const double BASE_CRIT_CHANCE = 0.05;
        private const double BASE_SCALAR = 1000;

        public void ProcesMeleeAttack(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            this.ProcessDodge(f, a, s, t);
            this.ProcessParry(f, a, s, t);
            this.ProcessBlock(f, a, s, t);
            this.ProcessCrit(f, a, s, t);
        }

        private double GetAttackVSDefenseSkillChance(double attackSkill, double defenseSkill, double baseDefenseChance)
        {
            var diff = attackSkill - defenseSkill;
            if (diff < 0) { diff += BASE_SCALAR; }
            var scalar = diff / BASE_SCALAR;
            return (BASE_CHANCE - (baseDefenseChance * scalar));
        }

        private void ProcessDodge(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var dodge = t.GetCurrentStatValue(SecondaryStatsEnum.Dodge);

            var chance = this.GetAttackVSDefenseSkillChance(melee, dodge, BASE_DODGE_CHANCE);
            chance *= a.DodgeMod;
            var roll = RNG.Instance.Next();
            if (chance > roll)
                AttackEventFlags.SetDodgeTrue(f);       
        }

        private void ProcessParry(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var parry = t.GetCurrentStatValue(SecondaryStatsEnum.Parry);

            var chance = this.GetAttackVSDefenseSkillChance(melee, parry, BASE_PARRY_CHANCE);
            chance *= a.ParryModMod;
            var roll = RNG.Instance.Next();
            if (chance > roll)
                AttackEventFlags.SetParryTrue(f);
        }

        private void ProcessBlock(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var block = t.GetCurrentStatValue(SecondaryStatsEnum.Block);

            var chance = this.GetAttackVSDefenseSkillChance(melee, block, BASE_BLOCK_CHANCE);

            var roll = RNG.Instance.Next();
            if (chance > roll)
                AttackEventFlags.SetBlockTrue(f);
        }

        private void ProcessCrit(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            var melee = s.GetCurrentStatValue(SecondaryStatsEnum.Melee);
            var crit = t.GetCurrentStatValue(SecondaryStatsEnum.Critical_Chance);

            var chance = this.GetAttackVSDefenseSkillChance(melee, crit, BASE_CRIT_CHANCE);

            var roll = RNG.Instance.Next();
            if (chance > roll)
                AttackEventFlags.SetCritTrue(f);
        }
    }
}
