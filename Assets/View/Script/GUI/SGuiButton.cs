using Assets.Controller.Manager.GUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.View.Script.GUI
{
    public class SGuiButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        protected virtual bool IsValid()
        {
            if (GUIManager.Instance.GetInteractionLocked())
                return false;
            else
                return true;
        }

        public virtual void OnClick()
        {
            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            GUIManager.Instance.SetGUILocked(true);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            GUIManager.Instance.SetGUILocked(false);
        }
    }
}
