using Generics;

namespace Model.Combat
{
    public class CombatReferee : AbstractSingleton<CombatReferee>
    {
        private const double BASE_CHANCE = 100;
        private const double BASE_SCALAR = 1000;

        public double GetAttackVSDefenseSkillChance(double attackSkill, double defenseSkill, double baseDefenseChance)
        {
            var diff = attackSkill - defenseSkill;
            if (diff < 0) { diff += BASE_SCALAR; }
            var scalar = diff / BASE_SCALAR;
            return (BASE_CHANCE - (baseDefenseChance * scalar));
        }
    }
}
