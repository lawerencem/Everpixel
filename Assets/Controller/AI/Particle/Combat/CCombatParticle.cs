using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.AI.Particle;
using System.Collections.Generic;

namespace Assets.Controller.AI.Particle.Combat
{
    public class CCombatParticle
    {
        private List<CTile> _mapTiles;

        public CCombatParticle(List<CTile> mapTiles)
        {
            this._mapTiles = mapTiles;
        }

        // TODO: Optimize
        public void RemoveAgentParticles(CChar agent, bool lTeam)
        {
            if (lTeam)
            {
                foreach (var tile in this._mapTiles)
                {
                    var particles = tile.Model.GetLTeamParticles();
                    particles.RemoveAgentParticles(agent.Proxy.GetGuid().ToString());
                }
            }
            else
            {
                foreach (var tile in this._mapTiles)
                {
                    var particles = tile.Model.GetRTeamParticles();
                    particles.RemoveAgentParticles(agent.Proxy.GetGuid().ToString());
                }
            }
        }

        public void SetAgentParticlePoints(CChar agent, bool lTeam)
        {
            this.RemoveAgentParticles(agent, lTeam);
            this.BuildAgentParticles(agent);
        }

        private void BuildAgentParticles(CChar agent)
        {
            var builder = new ParticleBuilder();
            builder.GenerateParticles(agent, agent.Proxy.LParty);
        }
    }
}
