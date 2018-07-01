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
            // TODO: Grab a dict of tiles that maintains their distance from center
            var pairs = agent.Proxy.GetModel().GetTile().GetAoETilesWithDistance(5);
            foreach (var pair in pairs)
            {
                this.GenerateThreatPoints(agent, pair.X, pair.Y, lTeam);
                this.GenerateVulnPoints(agent, pair.X, pair.Y, lTeam);
            }
        }

        private void GenerateThreatPoints(CChar agent, MTile tile, int dist, bool lTeam)
        {
            var builder = new ThreatPointBuilder();
            double degrade = AgentRoleDegradationTable.Instance.ThreatTable[agent.Proxy.GetAIRole()];
            for (int i = 0; i < dist; i++)
                degrade *= degrade;
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
            double degrade = AgentRoleDegradationTable.Instance.VulnTable[agent.Proxy.GetAIRole()];
            var vulns = builder.BuildVulns(agent);
            for (int i = 0; i < dist; i++)
                degrade *= degrade;
            var particles = tile.GetParticles();
            foreach (var kvp in vulns)
            {
                kvp.Value.ScaleValue(degrade);
                particles.AddVulnParticles(kvp.Key, kvp.Value, lTeam);
            }
        }
    }
}
