using Assets.Model.AI.Agent.Role;

namespace Assets.Model.AI.Agent
{
    public class AgentRoleFactory
    {
        public MAgentRole GetAgentRole(EAgentRole role)
        {
            MAgentRole agentRole;
            switch (role)
            {
                case (EAgentRole.Assassin_Melee): { agentRole = new MAssassinMelee(EAgentRole.Assassin_Melee); } break;
                case (EAgentRole.Assassin_Ranged): { agentRole = new MAssassinRanged(EAgentRole.Assassin_Ranged); } break;
                case (EAgentRole.Brawler_Initiater): { agentRole = new MBrawlerInitiater(EAgentRole.Brawler_Initiater); } break;
                case (EAgentRole.Support_Summoner): { agentRole = new MSupportSummoner(EAgentRole.Support_Summoner); } break;
                case (EAgentRole.Tank_Zone): { agentRole = new MTankZone(EAgentRole.Tank_Zone); } break;
                default: { agentRole = null; } break;
            }
            return agentRole;
        }
    }
}
