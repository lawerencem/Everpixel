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
            ParticleContainer points;
            if (lTeam)
                points = tile.GetLTeamParticles();
            else
                points = tile.GetRTeamParticles();
            foreach (var kvp in threats)
                points.AddThreatParticles(kvp.Key, kvp.Value);
        }

        private void GenerateVulnPoints(CChar agent, MTile tile, int dist, bool lTeam)
        {
            var builder = new VulnPointBuilder();
            var vulns = builder.BuildVulns(agent);
            // TODO: Modify threats based on distance
            // TODO: agent.Role.ModifyVulnPoints(vulns);
            ParticleContainer points;
            if (lTeam)
                points = tile.GetLTeamParticles();
            else
                points = tile.GetRTeamParticles();
            foreach (var kvp in vulns)
                points.AddVulnParticles(kvp.Key, kvp.Value);
        }
    }
}
