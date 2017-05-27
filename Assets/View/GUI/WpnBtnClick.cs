using Controller.Managers;
using Model.Abilities;
using Model.Equipment;
using UnityEngine;
using UnityEngine.UI;
using View.Events;

namespace View.GUI
{
    public class WpnBtnClick : MonoBehaviour
    {
        private bool _rWeapon;
        private WeaponAbilitiesEnum _ability;

        public void Init(string tag)
        {
            var btnContainer = GameObject.FindGameObjectWithTag(tag);
            var btn = btnContainer.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
        }

        public void SetAbility(WeaponAbilitiesEnum a, bool rWeapon)
        {
            this._rWeapon = rWeapon;
            this._ability = a;
            var btn = GameObject.FindGameObjectWithTag(this.tag);
            var text = btn.GetComponentInChildren<Text>();
            text.text = a.ToString().Replace("_", " ");
        }

        private void OnClick()
        {
            var e = new WpnBtnClickEvent(GUIEventManager.Instance, this._ability, this._rWeapon);
        }
    }
}
