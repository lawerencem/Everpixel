using Assets.Model.AI.Particle.Threat;
using Assets.Model.AI.Particle.Vuln;
using System;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle
{
    public class ParticleContainer
    {
        private Dictionary<EThreat, List<CharParticlePair>> _lTeamThreatTable;
        private Dictionary<EVuln, List<CharParticlePair>> _lTeamVulnTable;
        private Dictionary<EThreat, List<CharParticlePair>> _rTeamThreatTable;
        private Dictionary<EVuln, List<CharParticlePair>> _rTeamVulnTable;

        public ParticleContainer()
        {
            this._lTeamThreatTable = new Dictionary<EThreat, List<CharParticlePair>>();
            this._lTeamVulnTable = new Dictionary<EVuln, List<CharParticlePair>>();
            this._rTeamThreatTable = new Dictionary<EThreat, List<CharParticlePair>>();
            this._rTeamVulnTable = new Dictionary<EVuln, List<CharParticlePair>>();
            foreach (EThreat threat in Enum.GetValues(typeof(EThreat)))
            {
                this._lTeamThreatTable.Add(threat, new List<CharParticlePair>());
                this._rTeamThreatTable.Add(threat, new List<CharParticlePair>());
            }
            foreach (EVuln vuln in Enum.GetValues(typeof(EVuln)))
            {
                this._lTeamVulnTable.Add(vuln, new List<CharParticlePair>());
                this._rTeamVulnTable.Add(vuln, new List<CharParticlePair>());
            }
        }

        public void AddThreatParticles(EThreat type, CharParticlePair threat, bool lTeam)
        {
            if (lTeam)
                this._lTeamThreatTable[type].Add(threat);
            else
                this._rTeamThreatTable[type].Add(threat);
        }

        public void AddVulnParticles(EVuln type, CharParticlePair vuln, bool lTeam)
        {
            if (lTeam)
                this._lTeamVulnTable[type].Add(vuln);
            else
                this._rTeamVulnTable[type].Add(vuln);
        }

        public void RemoveAgentParticles(string id, bool lTeam)
        {
            if (lTeam)
            {
                foreach (var pair in this._lTeamThreatTable)
                    pair.Value.RemoveAll(x => x.Id == id);
                foreach (var pair in this._lTeamVulnTable)
                    pair.Value.RemoveAll(x => x.Id == id);
            }
            else
            {
                foreach (var pair in this._rTeamThreatTable)
                    pair.Value.RemoveAll(x => x.Id == id);
                foreach (var pair in this._rTeamVulnTable)
                    pair.Value.RemoveAll(x => x.Id == id);
            }
        }

        public double GetThreatValue(EThreat type, bool lTeam)
        {
            double threat = 0;
            if (lTeam)
            {
                foreach (var pair in this._lTeamThreatTable[type])
                    threat += pair.Value;
            }
            else
            {
                foreach (var pair in this._rTeamThreatTable[type])
                    threat += pair.Value;
            }
            return threat;
        }

        public double GetVulnValue(EVuln type, bool lTeam)
        {
            double vuln = 0;
            if (lTeam)
            {
                foreach (var pair in this._lTeamVulnTable[type])
                    vuln += pair.Value;
            }
            else
            {
                foreach (var pair in this._rTeamVulnTable[type])
                    vuln += pair.Value;
            }
            return vuln;
        }
    }
}
