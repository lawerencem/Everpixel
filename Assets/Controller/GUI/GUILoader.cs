using Assets.Controller.Manager.GUI;
using Assets.View.GUI;
using Assets.View.Script.GUI;
using UnityEngine;

namespace Assets.Controller.GUI
{
    public class GUILoader
    {
        public GUILoader()
        {

        }

        public void InitCombatGUI()
        {
            this.InitAbilityModal();
            this.InitWpnBtns();
        }

        private void InitAbilityModal()
        {
            var modal = GameObject.FindGameObjectWithTag(GameObjectTags.ACTIVE_MODAL);
            GUIManager.Instance.AddComponent(GameObjectTags.ACTIVE_MODAL, modal);
            var script = modal.AddComponent<AbilityModalManager>();
            script.Init();
        }

        private void InitWpnBtns()
        {
            for(int i = 0; i < 7; i++)
            {
                var tag = "WpnBtnTag" + i;
                var btn = GameObject.FindGameObjectWithTag(tag);
                GUIManager.Instance.AddComponent(tag, btn);
                var script = btn.AddComponent<SWpnBtn>();
                script.Init(btn);
                GUIManager.Instance.SetComponentActive(tag, false);
            }
        }
    }
}
