using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Effect;
using UnityEngine;

namespace Assets.Template.Util
{
    public class DecoUtil
    {
        public void AttachEffectParticlesToChar(CChar tgt, GameObject particles, EEffect effect)
        {
            if (tgt != null)
            {
                this.DisplayText(tgt, effect);
                if (particles != null)
                {
                    var view = tgt.View;
                    if (!view.EffectParticlesDict.ContainsKey(effect))
                    {
                        view.EffectParticlesDict.Add(effect, particles);
                        particles.transform.SetParent(tgt.GameHandle.transform);
                        particles.transform.position = tgt.GameHandle.transform.position;
                    }
                }
            }
        }

        private void DisplayText(CChar tgt, EEffect effect)
        {
            var text = effect.ToString().Replace("_", " ").Replace("DoT", "");
            VCombatController.Instance.DisplayText(text, tgt);
        }
    }
}
