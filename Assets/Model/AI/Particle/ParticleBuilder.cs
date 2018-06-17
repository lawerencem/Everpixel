﻿using Assets.Controller.Character;
using Assets.Model.AI.Particle.Threat;
using Assets.Model.AI.Particle.Vuln;
using Assets.Model.Map.Tile;

namespace Assets.Model.AI.Particle
{
    public class ParticleBuilder
    {
        public void GenerateParticles(CChar target, bool lTeam)
        {
            // TODO: Grab a dict of tiles that maintains their distance from center
            // TODO: Remove char's threat from all old tiles
            var tiles = target.Proxy.GetModel().GetTile().GetAoETiles(3);
            foreach (var tile in tiles)
            {
                tile.GetRTeamParticles().RemoveCharSwarmPoints(target.Proxy.GetId());
                this.GenerateThreatPoints(target, tile, 0, lTeam);
                this.GenerateVulnPoints(target, tile, 0, lTeam);
            }
        }

        private void GenerateThreatPoints(CChar target, MTile tile, int dist, bool lTeam)
        {
            var builder = new ThreatPointBuilder();
            var threats = builder.BuildThreats(target);
            // TODO: Modify threats based on distance
            target.Role.ModifyThreatPoints(threats);
            ParticleContainer points;
            if (lTeam)
                points = tile.GetLTeamParticles();
            else
                points = tile.GetRTeamParticles();
            foreach (var kvp in threats)
                points.AddThreatParticles(kvp.Key, kvp.Value);
        }

        private void GenerateVulnPoints(CChar target, MTile tile, int dist, bool lTeam)
        {
            var builder = new VulnPointBuilder();
            var vulns = builder.BuildVulns(target);
            // TODO: Modify threats based on distance
            target.Role.ModifyVulnPoints(vulns);
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
