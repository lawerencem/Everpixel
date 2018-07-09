using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.AI.Agent.Combat;
using Assets.Model.Character.Enum;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent.Role
{
    public class MAssassinMelee : MAgentRole
    {
        private const double SHIELD_WALL_PREFERENCE = 10000;
        private const double RIPOSTE_PREFERENCE = 10000;

        public MAssassinMelee(EAgentRole role) : base(role)
        {

        }

        public override Dictionary<EAbility, double> GetAbilityWeights(CChar agent)
        {
            var weights = base.GetAbilityWeights(agent);

            if (weights.ContainsKey(EAbility.Shield_Wall))
            {
                if (FActionStatus.HasFlag(agent.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.ShieldWalling))
                    weights.Remove(EAbility.Riposte);
                else
                    weights[EAbility.Shield_Wall] = SHIELD_WALL_PREFERENCE;
            }

            if (weights.ContainsKey(EAbility.Riposte))
            {
                if (FActionStatus.HasFlag(agent.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Riposting))
                    weights.Remove(EAbility.Riposte);
                else
                    weights[EAbility.Spear_Wall] = RIPOSTE_PREFERENCE;
            }

            return weights;
        }

        public override void ModifyParticleTilePoints(List<AgentMoveTileAndWeight> tileAndWeights, CChar agent)
        {
            base.ModifyParticleTilePoints(tileAndWeights, agent);   
        }
    }
}
