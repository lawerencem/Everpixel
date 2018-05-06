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

        // TODO: Move particle creation to load from .xml files
        private void CreateWaterParticles(CChar c, MTile t)
        {
            var controller = new ParticleController();
            var path = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "Water",
                CombatGUIParams.PARTICLES_EXTENSION);
            var particles = controller.CreateParticle(path);
            if (particles != null)
                controller.AttachParticle(t.Controller.Handle, particles);
            var script = particles.AddComponent<SDestroyByLifetime>();
            script.Init(particles, 5f);
        }
    }
}
