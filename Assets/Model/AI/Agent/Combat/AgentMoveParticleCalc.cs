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
            var tiles = agent.Tile.Model.GetEmptyAoETiles(4);
            foreach (var tile in tiles)
                this.GenerateMovePoints(agent.Proxy.GetAIRole(), tile, agent.Proxy.LParty);
            this._tiles.Sort((x, y) => y.X.CompareTo(x.X));
        }

        private void GenerateMovePoints(EAgentRole role, MTile tile, bool lTeam)
        {
            var particles = tile.GetParticles();
            double movePts = 0;

            var enemyVulns = this._vulns.EnemyVulnTable[role];
            var enemyThreats = this._threats.EnemyThreatTable[role];
            var friendlyVulns = this._vulns.FriendlyVulnTable[role];
            var friendlyThreats = this._threats.FriendlyThreatTable[role];

            foreach (var kvp in enemyThreats)
                movePts -= (particles.GetThreatValue(kvp.Key, lTeam) * kvp.Value);
            foreach (var kvp in enemyVulns)
                movePts -= (particles.GetVulnValue(kvp.Key, lTeam) * kvp.Value);

            foreach (var kvp in enemyThreats)
                movePts += (particles.GetThreatValue(kvp.Key, lTeam) * kvp.Value);
            foreach (var kvp in friendlyVulns)
                movePts += (particles.GetVulnValue(kvp.Key, lTeam) * kvp.Value);

            this._tiles.Add(new Pair<double, MTile>(movePts, tile));
        }
    }
}
