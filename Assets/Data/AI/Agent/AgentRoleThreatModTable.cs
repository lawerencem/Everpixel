using Assets.Model.AI.Agent;
using Assets.Model.AI.Particle.Threat;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Agent
{
    public class AgentRoleThreatModTable : ASingleton<AgentRoleThreatModTable>
    {
        public Dictionary<EAgentRole, Dictionary<EThreat, double>> Table;
        public AgentRoleThreatModTable()
        {
            this.Table = new Dictionary<EAgentRole, Dictionary<EThreat, double>>();
        }
    }
}
