using Assets.Controller.Character;
using Assets.Model.AI.Agent.Combat;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent.Role
{
    public class MAssassinMelee : MAgentRole
    {
        public MAssassinMelee(EAgentRole role) : base(role)
        {

        }

        public override void ModifyParticleTilePoints(List<AgentMoveTileAndWeight> tileAndWeights, CChar agent)
        {
            base.ModifyParticleTilePoints(tileAndWeights, agent);   
        }
    }
}
