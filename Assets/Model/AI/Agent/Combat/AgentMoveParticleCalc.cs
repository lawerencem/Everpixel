﻿using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Data.AI.Observe.Agent;
using Assets.Model.Map.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent.Combat
{
    public class AgentMoveParticleCalc
    {
        private List<AgentMoveTileAndWeight> _tiles;
        private AgentRoleThreatModTable _threats;
        private AgentRoleVulnModTable _vulns;

        public AgentMoveParticleCalc()
        {
            this._tiles = new List<AgentMoveTileAndWeight>();
            this._threats = AgentRoleThreatModTable.Instance;
            this._vulns = AgentRoleVulnModTable.Instance;
        }

        public Pair<CTile, double> GetMoveTile(CChar agent)
        {
            this.CalculateParticlePoints(agent);
            this._tiles.RemoveAll(x => x.Tile.Controller.Current != null);
            if (this._tiles.Count > 0)
                return new Pair<CTile, double>(this._tiles[0].Tile.Controller, this._tiles[0].Weight);
            else
                return new Pair<CTile, double>(null, 0);
        }

        private void CalculateParticlePoints(CChar agent)
        {
            var tiles = agent.Tile.Model.GetEmptyAoETiles(4);
            foreach (var tile in tiles)
                this.GenerateParticlePoints(agent.Proxy.GetAIRole(), tile, agent.Proxy.LParty);
            var agentRole = new AgentRoleFactory().GetAgentRole(agent.Proxy.GetAIRole());
            agentRole.ModifyParticleTilePoints(this._tiles, agent);
            this._tiles.Sort((x, y) => y.Weight.CompareTo(x.Weight));
        }

        private void GenerateParticlePoints(EAgentRole role, MTile tile, bool lTeam)
        {
            var particles = tile.GetParticles();
            double particlePoints = 0;

            var enemyVulns = this._vulns.EnemyVulnTable[role];
            var enemyThreats = this._threats.EnemyThreatTable[role];
            var friendlyVulns = this._vulns.FriendlyVulnTable[role];
            var friendlyThreats = this._threats.FriendlyThreatTable[role];

            foreach (var kvp in enemyThreats)
            {
                particlePoints -= (particles.GetThreatValue(kvp.Key, lTeam) * kvp.Value);
            }
            foreach (var kvp in enemyVulns)
            {
                double points = (particles.GetVulnValue(kvp.Key, lTeam) * kvp.Value);
                points *= tile.GetVulnMod();
                particlePoints += points;
            }
            foreach (var kvp in friendlyThreats)
            {
                double points = (particles.GetThreatValue(kvp.Key, lTeam) * kvp.Value);
                points *= tile.GetThreatMod();
                particlePoints += points;
            }
            foreach (var kvp in friendlyVulns)
            {
                particlePoints += (particles.GetVulnValue(kvp.Key, lTeam) * kvp.Value);
            }
            var tileAndWeight = new AgentMoveTileAndWeight();
            tileAndWeight.Tile = tile;
            tileAndWeight.Weight = particlePoints;
            this._tiles.Add(tileAndWeight);
        }
    }
}
