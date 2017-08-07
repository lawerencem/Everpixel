//using System;
//using Controller.Managers;
//using Model.Abilities;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using View.Events;
//using View.GUI;

//namespace View.Scripts
//{
//    public class WpnBtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//    {
//        private EAbility _ability;
//        private bool _rWeapon;
//        private GameObject _imgHandler;

//        private void OnClick()
//        {
//            if (this._ability != EAbility.None)
//            {
//                var e = new WpnBtnClickEvent(GUIEventManager.Instance, this._ability, this._rWeapon);
//            }
//        }

//        public void Init(string tag)
//        {
//            var btnContainer = GameObject.FindGameObjectWithTag(tag);
//            var btn = btnContainer.GetComponent<Button>();
//            btn.onClick.AddListener(this.OnClick);
//            this._imgHandler = new GameObject();
//            var img = this._imgHandler.AddComponent<Image>();
//        }

//        public void OnPointerEnter(PointerEventData eventData)
//        {
//            CombatEventManager.Instance.LockGUI();
//        }

//        public void OnPointerExit(PointerEventData eventData)
//        {
//            CombatEventManager.Instance.UnlockGUI();
//        }

//        public void SetAbility(EAbility a, bool rWeapon)
//        {
//            this._rWeapon = rWeapon;
//            this._ability = a;
//            var btn = GameObject.FindGameObjectWithTag(this.tag);
//            this._imgHandler.transform.SetParent(btn.transform);
//            var img = this._imgHandler.GetComponent<Image>();
//            img.sprite = GUISpriteLoader.Instance.GetWpnAbilityBtnImg(a);
//            img.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
//            img.transform.position = btn.transform.position;
//        }
//    }
//}
