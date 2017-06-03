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
    public class CombatMapGUIControllerParticle
    {
        // TODO: Make this a static config item
        private const string MAP_GUI_LAYER = "BackgroundTileGUI";
        private const string IMG = "ActingBoxImgTag";
        private const string NAME = "ActingBoxNameTag";
        private const string UI_LAYER = "UI";

        private const string ARMOR = "ArmorTextTag";
        private const string AP = "APTextTag";
        private const string HELM = "HelmTextTag";
        private const string HP = "HPTextTag";
        private const string L_WEAP = "LWeaponTextTag";
        private const string MORALE = "MoraleTextTag";
        private const string R_WEAP = "RWeaponTextTag";
        private const string STAM = "StaminaTextTag";

        private const string EFFECTS_PATH = "Effects/";
        private const string PARTICLES_EXTENSION = "Particles";

        public void ApplyInjuryParticle(ApplyInjuryEvent e)
        {
            if (e.Injury.IsTypeOfBleeding())
                this.HandleParticles("Bleed", e);
        }

        private void HandleParticles(string effect, ApplyInjuryEvent e)
        {
            var path = StringUtil.PathBuilder(EFFECTS_PATH, effect, PARTICLES_EXTENSION);
            var position = e.Target.transform.position;
            var particles = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            particles.transform.position = position;
            particles.transform.SetParent(e.Target.Handle.transform);
            particles.name = effect + " Particles";
        }
    }
}
