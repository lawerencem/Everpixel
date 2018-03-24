using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Logic.Calculator
{
    public abstract class AAbilityCalculator
    {
        private const double DELTA_BONUS = 0.12;

        public abstract void Predict(MHit hit);
        public abstract void Process(MHit hit);

        public double GetAttackVSDefenseSkillChance(double attackSkill, double defenseSkill, double baseDefenseChance)
        {
            double scalar = 1;
            double diff = attackSkill - defenseSkill;

            if (diff > 0)
                scalar = (1 - (diff / LogicParams.BASE_SCALAR));
            else
                scalar = (1 + ((diff *= -1) / LogicParams.BASE_SCALAR));

            return (baseDefenseChance / scalar);
        }

        public double GetHeightDeltaMod(MHit hit)
        {
            double mod = 1;
            if (hit.Data.Ability.Data.CastType == Enum.ECastType.Melee)
            {
                var tgt = hit.Data.Target.Current as CChar;
                double delta = tgt.Tile.Model.GetHeight() - hit.Data.Source.Tile.Model.GetHeight();
                if (delta > 0)
                    mod += (delta * DELTA_BONUS);
                else if (delta < 0)
                    mod -= (delta * DELTA_BONUS);
            }
            return mod;
        }

        public double GetSurroundedDeltaMod(MHit hit)
        {
            double mod = 1;
            if (hit.Data.Ability.Data.CastType == Enum.ECastType.Melee)
            {
                int count = 0;
                bool attackerTeam = hit.Data.Source.Proxy.LParty;
                var tgt = hit.Data.Target.Current as CChar;
                bool defenderTeam = tgt.Proxy.LParty;
                if (attackerTeam != defenderTeam)
                {
                    foreach (var tile in hit.Data.Target.GetAdjacent())
                    {
                        if (tile.Current != null && tile.Current.GetType().Equals(typeof(CChar)))
                        {
                            var tileChar = tile.Current as CChar;
                            if (tileChar.Proxy.LParty == attackerTeam)
                                count++;
                        }
                    }
                }
                if (count > 1)
                    mod -= (count * DELTA_BONUS);
            }
            return mod;
        }
    }
}
