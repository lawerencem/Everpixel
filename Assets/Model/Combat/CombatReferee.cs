using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;

namespace Model.Combat
{
    public class CombatReferee : AbstractSingleton<CombatReferee>
    {
        private const double BASE_CHANCE = 100;
        private const double BASE_DODGE_CHANCE = 0.05;
        private const double BASE_SCALAR = 1000;

        public void ProcesMeleeAttack(AttackEventFlags f, WeaponAbility a, GenericCharacter s, GenericCharacter t)
        {
            this.ProcessDodge(f, a, s, t);
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
            var chance = this.GetAttackVSDefenseSkillChance(s.SecondaryStats.MeleeSkill, s.SecondaryStats.DodgeSkill, BASE_DODGE_CHANCE);
            chance *= a.DodgeReduceMod;
            var roll = RNG.Instance.Next();
            if (chance > roll)
                AttackEventFlags.SetDodgeTrue(f);       
        }
    }
}
