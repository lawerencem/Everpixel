using System.Collections.Generic;
using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.AI.Agent.Combat;
using Assets.Model.Character.Enum;

namespace Assets.Model.AI.Agent.Role
{
    public class MTankZone : MAgentRole
    {
        private const double AGGRESSION_THROTTLE = 0.25;
        private const double SHIELD_WALL_PREFERENCE = 10000;
        private const double SHIELD_WALL_PENALTY = -10000;
        private const double SPEAR_WALL_PREFERENCE = 10000;
        private const double SPEAR_WALL_PENALTY = -10000;

        public MTankZone(EAgentRole role) : base(role)
        {

        }

        public override Dictionary<EAbility, double> GetAbilityWeights(CChar agent)
        {
            var weights = base.GetAbilityWeights(agent);

            if (weights.ContainsKey(EAbility.Shield_Wall))
            {
                if (FActionStatus.HasFlag(agent.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.ShieldWalling))
                    weights.Remove(EAbility.Shield_Wall);
                else
                    weights[EAbility.Shield_Wall] = SHIELD_WALL_PREFERENCE;
            }

            if (weights.ContainsKey(EAbility.Spear_Wall))
            {
                if (FActionStatus.HasFlag(agent.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Spearwalling))
                    weights.Remove(EAbility.Spear_Wall);
                else
                    weights[EAbility.Spear_Wall] = SPEAR_WALL_PREFERENCE;
            }

            return weights;
        }

        public override void ModifyParticleTilePoints(List<AgentMoveTileAndWeight> tileAndWeights, CChar agent)
        {
            base.ModifyParticleTilePoints(tileAndWeights, agent);
            if (FActionStatus.HasFlag(agent.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Spearwalling))
                this.HandleSpearwallingAggressionThrottle(tileAndWeights, agent);
        }

        private void HandleSpearwallingAggressionThrottle(List<AgentMoveTileAndWeight> tileAndWeights, CChar agent)
        {
            foreach (var tileAndWeight in tileAndWeights)
            {
                foreach (var tile in tileAndWeight.Tile.GetAdjacent())
                {
                    if (tile.GetCurrentOccupant() != null && tile.GetCurrentOccupant().Equals(typeof(CChar)))
                    {
                        var tgt = tile.GetCurrentOccupant() as CChar;
                        if (tgt.Proxy.LParty != agent.Proxy.LParty)
                            tileAndWeight.Weight *= AGGRESSION_THROTTLE;
                    }
                }
            }
        }
    }
}