using Assets.Model.Ability.Enum;
using Assets.Model.AI.Agent;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Orient.Agent
{
    public class AgentRoleOrientAbilitiesTable : ASingleton<AgentRoleOrientAbilitiesTable>
    {
        public Dictionary<EAgentRole, Dictionary<EAbility, double>> Table;
        public AgentRoleOrientAbilitiesTable()
        {
            this.Table = new Dictionary<EAgentRole, Dictionary<EAbility, double>>();
        }
    }
}
