using Assets.Model.Abiltiy.Logic;
using Model.Combat;

namespace Assets.Model.Ability.Logic.Calculator
{
    public abstract class AAbilityCalculator
    {
        public abstract void Predict(HitInfo hit);
        public abstract void Process(HitInfo hit);

        public double GetAttackVSDefenseSkillChance(double attackSkill, double defenseSkill, double baseDefenseChance)
        {
            double scalar = 1;
            double diff = attackSkill - defenseSkill;

            if (diff > 0)
                scalar = (1 - (diff / LogicParams.BASE_SCALAR));
            else
                scalar = (1 + ((diff *= -1) / LogicParams.BASE_SCALAR));

            return (baseDefenseChance * scalar);
        }
    }
}
