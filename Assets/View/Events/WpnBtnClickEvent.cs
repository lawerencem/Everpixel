using Controller.Managers;
using Model.Abilities;

namespace View.Events
{
    public class WpnBtnClickEvent : GUIEvent
    {
        public EAbility AbilityType { get; set; }
        public bool RWeapon { get; set; }

        public WpnBtnClickEvent(GUIEventManager parent, EAbility type, bool rWeapon) 
            : base(GUIEventEnum.WpnBtnClick, parent)
        {
            this.AbilityType = type;
            this.RWeapon = rWeapon;
            this._parent.RegisterEvent(this);
        }
    }
}
