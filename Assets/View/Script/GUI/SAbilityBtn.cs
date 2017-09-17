using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Model.Ability.Enum;
using Assets.View.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Script.GUI
{
    public class SAbilityBtnData
    {
        public EAbility Ability { get; set; }
        public GameObject Handle { get; set; }
        public CChar Source { get; set; }
    }

    public class SAbilityBtn : SGuiButton
    {
        private SAbilityBtnData _data;

        public void Init(SAbilityBtnData data)
        {
            this._data = data;
            var btn = this._data.Handle.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
        }

        public override void OnClick()
        {
            base.OnClick();
            var data = new EvAbilitySelectedData();
            data.Ability = this._data.Ability;
            data.Source = CombatManager.Instance.GetCurrentlyActing();
            var e = new EvAbilitySelected(data);
            e.TryProcess();
        }
    }
}
