using System;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle
{
    public class ParticleContainer
    {
        private Dictionary<EThreat, List<CharParticlePair>> _threatTable;
        private Dictionary<EVuln, List<CharParticlePair>> _vulnTable;

        public ParticleContainer()
        {
            this._threatTable = new Dictionary<EThreat, List<CharParticlePair>>();
            this._vulnTable = new Dictionary<EVuln, List<CharParticlePair>>();
            foreach(EThreat threat in Enum.GetValues(typeof(EThreat)))
                this._threatTable.Add(threat, new List<CharParticlePair>());
            foreach (EVuln vuln in Enum.GetValues(typeof(EVuln)))
                this._vulnTable.Add(vuln, new List<CharParticlePair>());
        }

        public void AddCharSwarmThreat(EThreat type, CharParticlePair threat)
        {
            this._threatTable[type].Add(threat);
        }

        public void AddCharSwarmVuln(EVuln type, CharParticlePair vuln)
        {
            this._vulnTable[type].Add(vuln);
        }

        public void RemoveCharSwarmPoints(Guid id)
        {
            foreach (var pair in this._threatTable)
                pair.Value.RemoveAll(x => x.Id == id);
            foreach (var pair in this._vulnTable)
                pair.Value.RemoveAll(x => x.Id == id);
        }

        public double GetThreatValue(EThreat type)
        {
            double threat = 0;
            foreach (var pair in this._threatTable[type])
                threat += pair.Value;
            return threat;
        }

        public double GetVulnValue(EVuln type)
        {
            double vuln = 0;
            foreach (var pair in this._vulnTable[type])
                vuln += pair.Value;
            return vuln;
        }

        public double GetTotalThreat()
        {
            double threat = 0;
            foreach (var swarmPair in this._threatTable)
            {
                foreach (var pair in swarmPair.Value)
                    threat += pair.Value;
            }
            return threat;
        }

        public double GetTotalVuln()
        {
            double vuln = 0;
            foreach (var swarmPair in this._vulnTable)
            {
                foreach (var pair in swarmPair.Value)
                    vuln += pair.Value;
            }
            return vuln;
        }
    }
}
