//using Controller.Managers;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using View.Events;

//namespace View.Scripts
//{
//    public class EndTurnBtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//    {
//        private void OnClick()
//        {
//            if (!CombatEventManager.Instance.GetInteractionLock())
//            {
//                var e = new GUIEndTurnEvent(GUIEventManager.Instance);
//            }
//        }

//        public void Start()
//        {
//            var btnContainer = GameObject.FindGameObjectWithTag("EndTurnBtnTag");
//            var btn = btnContainer.GetComponent<Button>();
//            btn.onClick.AddListener(this.OnClick);
//        }

//        public void OnPointerEnter(PointerEventData eventData)
//        {
//            CombatEventManager.Instance.LockGUI();
//        }

//        public void OnPointerExit(PointerEventData eventData)
//        {
//            CombatEventManager.Instance.UnlockGUI();
//        }
//    }
//}
