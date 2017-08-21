using Assets.Model.Ability.Enum;
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

//private EAbility _ability;
//private bool _rWeapon;
//private GameObject _imgHandler;

//private void OnClick()
//{
//    if (this._ability != EAbility.None)
//    {
//        var e = new WpnBtnClickEvent(GUIEventManager.Instance, this._ability, this._rWeapon);
//    }
//}

//public void Init(string tag)
//{
//    var btnContainer = GameObject.FindGameObjectWithTag(tag);
//    var btn = btnContainer.GetComponent<Button>();
//    btn.onClick.AddListener(this.OnClick);
//    this._imgHandler = new GameObject();
//    var img = this._imgHandler.AddComponent<Image>();
//}

//public void OnPointerEnter(PointerEventData eventData)
//{
//    CombatEventManager.Instance.LockGUI();
//}

//public void OnPointerExit(PointerEventData eventData)
//{
//    CombatEventManager.Instance.UnlockGUI();
//}

//public void SetAbility(EAbility a, bool rWeapon)
//{
//    this._rWeapon = rWeapon;
//    this._ability = a;
//    var btn = GameObject.FindGameObjectWithTag(this.tag);
//    this._imgHandler.transform.SetParent(btn.transform);
//    var img = this._imgHandler.GetComponent<Image>();
//    img.sprite = GUISpriteLoader.Instance.GetWpnAbilityBtnImg(a);
//    img.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
//    img.transform.position = btn.transform.position;
//}
