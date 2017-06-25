using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Abilities.Music;
using Model.Events.Combat;
using UnityEngine;

namespace Controller.Managers.Map
{
    public class CMapGUIControllerParticle
    { 
        public void ApplyInjuryParticle(ApplyInjuryEvent e)
        {
            if (e.Injury.IsTypeOfBleeding())
                this.HandleParticles("Bleed", e);
        }

        public void HandleSongParticle(DisplayHitStatsEvent e)
        {
            var tgt = e.Hit.Source;
            var song = e.Hit.Ability as GenericSong;
            var path = StringUtil.PathBuilder(CMapGUIControllerParams.EFFECTS_PATH, song.SongType.ToString(), CMapGUIControllerParams.PARTICLES_EXTENSION);
            var particles = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            particles.transform.SetParent(tgt.Handle.transform);
            particles.transform.position = tgt.Handle.transform.position;
            var script = particles.AddComponent<DestroyByLifetime>();
            script.lifetime = 5f;
            e.Done();
        }

        private void HandleParticles(string effect, ApplyInjuryEvent e)
        {
            var path = StringUtil.PathBuilder(CMapGUIControllerParams.EFFECTS_PATH, effect, CMapGUIControllerParams.PARTICLES_EXTENSION);
            var position = e.Target.transform.position;
            var particles = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            particles.transform.position = position;
            particles.transform.SetParent(e.Target.Handle.transform);
            particles.name = effect + " Particles";
            e.Target.Particles.Add(particles);
        }
    }
}
