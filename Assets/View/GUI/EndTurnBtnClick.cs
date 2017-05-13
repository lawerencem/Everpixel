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

        private void OnClick()
        {
            var e = new GUIEndTurnEvent(GUIEventManager.Instance);
        }
    }
}
