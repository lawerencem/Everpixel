using Assets.Model.AI.Agent;
using Assets.Model.AI.Particle.Threat;
using Assets.Model.AI.Particle.Vuln;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Observe.Agent
{
    public class AgentRoleSelfModTable : ASingleton<AgentRoleSelfModTable>
    {
        public Dictionary<EAgentRole, Dictionary<EThreat, double>> ThreatTable;
        public Dictionary<EAgentRole, Dictionary<EVuln, double>> VulnTable;
        public AgentRoleSelfModTable()
        {
            this.ThreatTable = new Dictionary<EAgentRole, Dictionary<EThreat, double>>();
            this.VulnTable = new Dictionary<EAgentRole, Dictionary<EVuln, double>>();
        }
    }
}
