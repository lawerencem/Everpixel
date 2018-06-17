using Assets.Controller.Character;
using Assets.Model.Map.Tile;

namespace Assets.Model.AI.Particle
{
    public class ParticleBuilder
    {
        public void GenerateParticles(CChar target)
        {
            // TODO: Grab a dict of tiles that maintains their distance from center
            var tiles = target.Proxy.GetModel().GetTile().GetAoETiles(3);
            foreach (var tile in tiles)
            {
                tile.GetRTeamParticles().RemoveCharSwarmPoints(target.Proxy.GetId());
                this.GenerateThreatPoints(target, tile, 0);
                this.GenerateVulnPoints(target, tile, 0);
            }
        }

        private void GenerateThreatPoints(CChar target, MTile tile, int dist)
        {
            var builder = new ThreatPointBuilder();
            var threats = builder.BuildThreats(target);
            // TODO: Modify threats based on distance
            target.Role.ModifyThreatPoints(threats);
            var points = tile.GetRTeamParticles();
            foreach (var kvp in threats)
                points.AddCharSwarmThreat(kvp.Key, kvp.Value);
        }

        private void GenerateVulnPoints(CChar target, MTile tile, int dist)
        {
            var builder = new VulnPointBuilder();
            var vulns = builder.BuildVulns(target);
            // TODO: Modify threats based on distance
            target.Role.ModifyVulnPoints(vulns);
            var points = tile.GetRTeamParticles();
            foreach (var kvp in vulns)
                points.AddCharSwarmVuln(kvp.Key, kvp.Value);
        }
    }
}
