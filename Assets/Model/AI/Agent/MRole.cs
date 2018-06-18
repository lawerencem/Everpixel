using Assets.Model.AI.Particle;
using Assets.Model.AI.Particle.Threat;
using Assets.Model.AI.Particle.Vuln;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent
{
    public class MAgentRole
    {
        private EAgentRole _type;

        public MAgentRole(EAgentRole type)
        {
            this._type = type;
        }

        public void ModifyThreatPoints(Dictionary<EThreat, CharParticlePair> threats)
        {
            // TODO: Modify threats based on AI roles
        }

        public void ModifyVulnPoints(Dictionary<EVuln, CharParticlePair> vulns)
        {
            // TODO: Modify vulns based on AI roles
        }
    }
}
