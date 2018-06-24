using Assets.Model.AI.Agent;
using Assets.Model.AI.Particle.Vuln;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Agent
{
    public class AgentRoleVulnModTable : ASingleton<AgentRoleVulnModTable>
    {
        public Dictionary<EAgentRole, Dictionary<EVuln, double>> Table;
        public AgentRoleVulnModTable()
        {
            this.Table = new Dictionary<EAgentRole, Dictionary<EVuln, double>>();
        }
    }
}
