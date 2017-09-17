using Assets.Controller.Manager.Combat;
using Assets.Model.Ability.Enum;
using Assets.View.Event;
using Assets.View.GUI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Script.GUI
{
    public class SWpnBtn : SGuiButton
    {
        private EAbility _ability;
        private GameObject _btnHandle;
        private GameObject _imgHandle;
        private bool _lWeapon;

        public void Init(GameObject o)
        {
            this._btnHandle = o;
            var btn = this._btnHandle.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
            this._imgHandle = new GameObject();
            this._imgHandle.transform.SetParent(btn.transform);
            this._imgHandle.transform.position = btn.transform.position;
            this._imgHandle.AddComponent<Image>();
        }

        public override void OnClick()
        {
            base.OnClick();
            var data = new EvAbilitySelectedData();
            data.Ability = this._ability;
            data.LWeapon = this._lWeapon;
            data.Source = CombatManager.Instance.GetCurrentlyActing();
            data.WpnAbility = true;
            var e = new EvAbilitySelected(data);
            e.TryProcess();
        }

        public void SetAbility(EAbility a, bool lWeapon)
        {
            this._ability = a;
            this._lWeapon = lWeapon;
            var img = this._imgHandle.GetComponent<Image>();
            img.sprite = GUISpriteLoader.Instance.GetWpnAbilityBtnImg(a);
            img.transform.localScale = ViewParams.WPN_IMG_SCALE;
        }
    }
}
