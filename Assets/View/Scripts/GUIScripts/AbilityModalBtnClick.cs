using Controller.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Controller.Managers.Map;

namespace View.Scripts
{
    public class AbilityModalBtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    { 
        private bool _activeModal = false;

        private void OnClick()
        {
            if (!this._activeModal)
            {
                CMapGUIController.Instance.SetAbilityModalActive();
                this._activeModal = true;
            }
            else
            {
                CMapGUIController.Instance.SetAbilityModalInactive();
                this._activeModal = false;
            }
        }

        public void Init(string tag)
        {
            var btnContainer = GameObject.FindGameObjectWithTag(tag);
            var btn = btnContainer.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CombatEventManager.Instance.LockGUI();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CombatEventManager.Instance.UnlockGUI();
        }
    }
}
