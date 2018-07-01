using Assets.Model.AI.Agent;
using Assets.Model.AI.Particle.Threat;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Agent
{
    public class AgentRoleThreatModTable : ASingleton<AgentRoleThreatModTable>
    {
        public Dictionary<EAgentRole, Dictionary<EThreat, double>> EnemyThreatTable;
        public Dictionary<EAgentRole, Dictionary<EThreat, double>> FriendlyThreatTable;
        public AgentRoleThreatModTable()
        {
            this.EnemyThreatTable = new Dictionary<EAgentRole, Dictionary<EThreat, double>>();
            this.FriendlyThreatTable = new Dictionary<EAgentRole, Dictionary<EThreat, double>>();
        }
    }
}
