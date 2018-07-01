using Assets.Controller.Character;
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
            var tiles = agent.Proxy.GetModel().GetTile().GetAoETiles(5);
            foreach (var tile in tiles)
            {
                this.GenerateThreatPoints(agent, tile, 0, lTeam);
                this.GenerateVulnPoints(agent, tile, 0, lTeam);
            }
        }

        private void GenerateThreatPoints(CChar agent, MTile tile, int dist, bool lTeam)
        {
            var builder = new ThreatPointBuilder();
            var threats = builder.BuildThreats(agent);
            // TODO: Modify threats based on distance
            // TODO: agent.Role.ModifyThreatPoints(threats);
            var particles = tile.GetParticles();
            foreach (var kvp in threats)
                particles.AddThreatParticles(kvp.Key, kvp.Value, lTeam);
        }

        private void GenerateVulnPoints(CChar agent, MTile tile, int dist, bool lTeam)
        {
            var builder = new VulnPointBuilder();
            var vulns = builder.BuildVulns(agent);
            // TODO: Modify threats based on distance
            // TODO: agent.Role.ModifyVulnPoints(vulns);
            var particles = tile.GetParticles();
            foreach (var kvp in vulns)
                particles.AddVulnParticles(kvp.Key, kvp.Value, lTeam);
        }
    }
}
