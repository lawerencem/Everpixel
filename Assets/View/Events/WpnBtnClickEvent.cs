using Controller.Managers;
using Model.Abilities;

namespace View.Events
{
    public class WpnBtnClickEvent : GUIEvent
    {
        public WeaponAbilitiesEnum Type { get; set; }

        public WpnBtnClickEvent(GUIEventManager parent, WeaponAbilitiesEnum type) : base(GUIEventEnum.WpnBtnClick, parent)
        {
            this.Type = type;
            this._parent.RegisterEvent(this);
        }
    }
}
