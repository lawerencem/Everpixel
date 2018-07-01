using Assets.Model.AI.Agent;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Agent
{
    public class AgentRoleDegradationTable : ASingleton<AgentRoleDegradationTable>
    {
        public Dictionary<EAgentRole, double> ThreatTable;
        public Dictionary<EAgentRole, double> VulnTable;
        public AgentRoleDegradationTable()
        {
            this.ThreatTable = new Dictionary<EAgentRole, double>();
            this.VulnTable = new Dictionary<EAgentRole, double>();
        }
    }
}
