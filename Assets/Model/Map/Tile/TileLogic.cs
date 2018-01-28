using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Particle;

namespace Assets.Model.Map.Tile
{
    public class TileLogic
    {
        public void ProcessEnterTile(CChar c, MTile t)
        {
            switch(t.Type)
            {
                case (ETile.Water): { this.CreateWaterParticles(c, t); } break;
            }
        }

        private void CreateWaterParticles(CChar c, MTile t)
        {
            var path = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "Water",
                CombatGUIParams.PARTICLES_EXTENSION);
            var particles = ParticleController.Instance.CreateParticle(path);
            if (particles != null)
                DecoUtil.AttachParticles(particles, t.Controller.Handle);
            var script = particles.AddComponent<SDestroyByLifetime>();
            script.Init(particles, 5f);
        }
    }
}
