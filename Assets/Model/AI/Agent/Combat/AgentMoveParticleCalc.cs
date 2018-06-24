using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Data.AI.Agent;
using Assets.Model.AI.Particle;
using Assets.Model.Map.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent.Combat
{
    public class AgentMoveParticleCalc
    {
        private List<Pair<double, MTile>> _tiles;
        private AgentRoleThreatModTable _threats;
        private AgentRoleVulnModTable _vulns;

        public AgentMoveParticleCalc()
        {
            this._tiles = new List<Pair<double, MTile>>();
            this._threats = AgentRoleThreatModTable.Instance;
            this._vulns = AgentRoleVulnModTable.Instance;
        }

        public CTile GetMoveTile(CChar agent)
        {
            this.CalculateParticlePoints(agent);
            this._tiles.RemoveAll(x => x.Y.Controller.Current != null);
            if (this._tiles.Count > 0)
                return this._tiles[0].Y.Controller;
            else
                return null;
        }

        private void CalculateParticlePoints(CChar agent)
        {
            var tiles = agent.Tile.Model.GetAoETiles(5);
            foreach (var tile in tiles)
                this.GenerateMovePoints(agent.Proxy.GetAIRole(), tile, agent.Proxy.LParty);
            this._tiles.Sort((x, y) => y.X.CompareTo(x.X));
        }

        private void GenerateMovePoints(EAgentRole role, MTile tile, bool lTeam)
        {
            ParticleContainer particles;
            if (lTeam)
                particles = tile.GetLTeamParticles();
            else
                particles = tile.GetRTeamParticles();
            double movePts = 0;
            var threats = this._threats.Table[role];
            var vulns = this._vulns.Table[role];
            foreach (var kvp in threats)
                movePts -= (particles.GetThreatValue(kvp.Key) * kvp.Value);
            foreach (var kvp in vulns)
                movePts += (particles.GetVulnValue(kvp.Key) * kvp.Value);
            this._tiles.Add(new Pair<double, MTile>(movePts, tile));
        }
    }
}
