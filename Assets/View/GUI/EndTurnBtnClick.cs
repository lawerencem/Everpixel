using Controller.Managers;
using UnityEngine;
using UnityEngine.UI;
using View.Events;

namespace View.GUI
{
    public class EndTurnBtnClick : MonoBehaviour
    {
        public void Start()
        {
            var btnContainer = GameObject.FindGameObjectWithTag("EndTurnBtnTag");
            var btn = btnContainer.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
        }

        public void OnMouseOver()
        {
            CombatEventManager.Instance.LockGUI();
        }

        public void OnMouseExit()
        {
            CombatEventManager.Instance.UnlockGUI();
        }

        private void OnClick()
        {
            var e = new GUIEndTurnEvent(GUIEventManager.Instance);
        }
    }
}
