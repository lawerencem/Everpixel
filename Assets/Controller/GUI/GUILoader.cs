using Assets.Controller.Manager;
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
            this.InitWpnBtns();
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
