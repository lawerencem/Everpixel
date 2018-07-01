using Assets.Model.AI.Agent;
using Assets.Model.AI.Particle.Vuln;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Agent
{
    public class AgentRoleVulnModTable : ASingleton<AgentRoleVulnModTable>
    {
        public Dictionary<EAgentRole, Dictionary<EVuln, double>> EnemyVulnTable;
        public Dictionary<EAgentRole, Dictionary<EVuln, double>> FriendlyVulnTable;
        public AgentRoleVulnModTable()
        {
            this.EnemyVulnTable = new Dictionary<EAgentRole, Dictionary<EVuln, double>>();
            this.FriendlyVulnTable = new Dictionary<EAgentRole, Dictionary<EVuln, double>>();
        }
    }
}
