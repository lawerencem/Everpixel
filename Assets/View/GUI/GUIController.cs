using Generics;
using Model.Events.Combat;
using UnityEngine;
using UnityEngine.UI;
using View.Builders;

namespace View.GUI
{
    public class GUIController : AbstractSingleton<GUIController>
    {
        private const string ACTING = "ActingSprite";

        public void SetActingCharacter(TakingActionEvent e)
        {
            var box = GameObject.FindGameObjectWithTag(ACTING);
            var sprite = box.GetComponent<Image>();
            var builder = new CharacterViewBuilder();
        }
    }
}
