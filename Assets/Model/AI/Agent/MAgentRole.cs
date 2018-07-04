using Assets.Controller.Character;
using Assets.Data.AI.Orient.Agent;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Map.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent
{
    public class MAgentRole
    {
        private EAgentRole _type;

        public EAgentRole Type { get { return this._type; } }

        public MAgentRole(EAgentRole type)
        {
            this._type = type;
        }

        public virtual void ModifyParticleTilePoints(List<Pair<double, MTile>> tiles)
        {

        }

        public virtual Dictionary<EAbility, double> GetAbilityWeights(CChar agent)
        {
            var abilityWeights = new Dictionary<EAbility, double>();

            foreach (var ability in agent.Proxy.GetActiveAbilities())
                abilityWeights.Add(ability.Type, 1);

            if (agent.Proxy.Type == ECharType.Humanoid)
            {
                if (agent.Proxy.GetLWeapon() != null)
                {
                    foreach (var ability in agent.Proxy.GetLWeapon().Model.Data.Abilities)
                        abilityWeights.Add(ability.Type, 1);
                }
                if (agent.Proxy.GetRWeapon() != null)
                {
                    foreach (var ability in agent.Proxy.GetRWeapon().Model.Data.Abilities)
                        if (!abilityWeights.ContainsKey(ability.Type))
                            abilityWeights.Add(ability.Type, 1);
                }
            }

            var weights = AgentRoleOrientAbilitiesTable.Instance.Table;
            foreach (var kvp in weights[agent.Proxy.GetAIRole()])
            {
                if (abilityWeights.ContainsKey(kvp.Key))
                    abilityWeights[kvp.Key] = kvp.Value;
            }

            return abilityWeights;
        }
    }
}
