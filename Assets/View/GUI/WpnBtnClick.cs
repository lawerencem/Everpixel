﻿using Controller.Managers;
using Model.Abilities;
using UnityEngine;
using UnityEngine.UI;
using View.Events;

namespace View.GUI
{
    public class WpnBtnClick : MonoBehaviour
    {
        private WeaponAbilitiesEnum _ability;
        private bool _rWeapon;
        private GameObject _imgHandler;

        public void Init(string tag)
        {
            var btnContainer = GameObject.FindGameObjectWithTag(tag);
            var btn = btnContainer.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
            this._imgHandler = new GameObject();
            var img = this._imgHandler.AddComponent<Image>();
        }

        public void SetAbility(WeaponAbilitiesEnum a, bool rWeapon)
        {
            this._rWeapon = rWeapon;
            this._ability = a;
            var btn = GameObject.FindGameObjectWithTag(this.tag);
            var text = btn.GetComponentInChildren<Text>();
            text.text = a.ToString().Replace("_", " ");
            this._imgHandler.transform.SetParent(btn.transform);
            var img = this._imgHandler.GetComponent<Image>();
            img.sprite = GUISpriteLoader.Instance.GetWpnAbilityBtnImg(a);
            img.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            img.transform.position = btn.transform.position;
        }

        private void OnClick()
        {
            var e = new WpnBtnClickEvent(GUIEventManager.Instance, this._ability, this._rWeapon);
        }
    }
}
