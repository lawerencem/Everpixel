using Assets.Scripts;
using Controller.Characters;
using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;

namespace Controller.Managers.Map
{
    public class CMapGUIControllerParticle
    { 
        public void ApplyInjuryParticle(ApplyInjuryEvent e)
        {
            if (e.Injury.IsTypeOfBleeding())
                this.HandleParticles("Bleed", e);
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
