using Assets.Controller.Character;
using System;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle.Vuln
{
    public class VulnPointBuilder
    {
        public Dictionary<EVuln, CharParticlePair> BuildVulns(CChar target)
        {
            var vulns = new Dictionary<EVuln, CharParticlePair>();
            foreach (EVuln vuln in Enum.GetValues(typeof(EVuln)))
                vulns.Add(vuln, this.GetSwarmPoints(vuln, target));
            return vulns;
        }

        private CharParticlePair GetSwarmPoints(EVuln type, CChar target)
        {
            var value = 10; // TODO
            return new CharParticlePair(target.Proxy.GetId(), value);
        }
    }
}
