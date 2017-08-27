using Assets.Controller.Manager.GUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.View.Script.GUI
{
    public class SGui : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
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
