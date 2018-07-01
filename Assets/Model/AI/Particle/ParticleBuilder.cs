using Assets.Controller.Character;
using Assets.Data.AI.Agent;
using Assets.Model.AI.Particle.Threat;
using Assets.Model.AI.Particle.Vuln;
using Assets.Model.Map.Tile;

namespace Assets.Model.AI.Particle
{
    public class ParticleBuilder
    {
        public void GenerateParticles(CChar agent, bool lTeam)
        {
            var pairs = agent.Proxy.GetModel().GetTile().GetAoETilesWithDistance(8);
            foreach (var pair in pairs)
            {
                this.GenerateThreatPoints(agent, pair.X, pair.Y, lTeam);
                this.GenerateVulnPoints(agent, pair.X, pair.Y, lTeam);
            }
        }

        private void GenerateThreatPoints(CChar agent, MTile tile, int dist, bool lTeam)
        {
            var builder = new ThreatPointBuilder();
            double scalar = 1;
            double degrade = AgentRoleDegradationTable.Instance.ThreatTable[agent.Proxy.GetAIRole()];
            for (int i = 0; i < dist; i++)
                scalar *= degrade;
            scalar *= this.GetHeightScalar(tile);
            var threats = builder.BuildThreats(agent);
            var particles = tile.GetParticles();
            foreach (var kvp in threats)
            {
                kvp.Value.ScaleValue(degrade);
                particles.AddThreatParticles(kvp.Key, kvp.Value, lTeam);
            }
        }

        private void GenerateVulnPoints(CChar agent, MTile tile, int dist, bool lTeam)
        {
            var builder = new VulnPointBuilder();
            double scalar = 1;
            double degrade = AgentRoleDegradationTable.Instance.VulnTable[agent.Proxy.GetAIRole()];
            var vulns = builder.BuildVulns(agent);
            for (int i = 0; i < dist; i++)
                scalar *= degrade;
            scalar *= this.GetHeightScalar(tile);
            var particles = tile.GetParticles();
            foreach (var kvp in vulns)
            {
                kvp.Value.ScaleValue(scalar);
                particles.AddVulnParticles(kvp.Key, kvp.Value, lTeam);
            }
        }

        private double GetHeightScalar(MTile tile)
        {
            double scalar = 1;
            foreach (var neighbor in tile.GetAdjacent())
            {
                int delta = neighbor.GetHeight() - tile.GetHeight();
                switch(delta)
                {
                    case (2): { scalar *= 0.8; } break;
                    case (1): { scalar *= 0.9; } break;
                    case (-1): { scalar *= 1.1; } break;
                    case (-2): { scalar *= 1.2; } break;
                }
            }
            return scalar;
        }
    }
}
