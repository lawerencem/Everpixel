using Controller.Managers;
using Model.Abilities;

namespace View.Events
{
    public class WpnBtnClickEvent : GUIEvent
    {
        public bool RWeapon { get; set; }
        public WeaponAbilitiesEnum Type { get; set; }

        public WpnBtnClickEvent(GUIEventManager parent, WeaponAbilitiesEnum type, bool rWeapon) 
            : base(GUIEventEnum.WpnBtnClick, parent)
        {
            this.RWeapon = rWeapon;
            this.Type = type;
            this._parent.RegisterEvent(this);
        }
    }
}
