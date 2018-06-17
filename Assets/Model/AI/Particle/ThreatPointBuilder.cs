using Assets.Controller.Character;
using System;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle
{
    public class ThreatPointBuilder
    {
        public Dictionary<EThreat, CharParticlePair> BuildThreats(CChar target)
        {
            var threats = new Dictionary<EThreat, CharParticlePair>();
            foreach (EThreat threat in Enum.GetValues(typeof(EThreat)))
                threats.Add(threat, this.GetThreatPoints(threat, target));
            return threats;
        }

        private CharParticlePair GetThreatPoints(EThreat type, CChar target)
        {
            var value = 10; // TODO
            return new CharParticlePair(target.Proxy.GetId(), value);
        }
    }
}
