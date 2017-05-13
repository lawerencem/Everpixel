using Controller.Managers;
using Model.Abilities;
using System.Collections.Generic;

namespace View.Events
{
    public class PopulateWpnBtnsEvent : GUIEvent
    {
        public List<WeaponAbility> Abilities;

        public PopulateWpnBtnsEvent(List<WeaponAbility> abs, GUIEventManager parent) : 
            base(GUIEventEnum.PopulateWpnBtns, parent)
        {
            this.Abilities = abs;
            this._parent.RegisterEvent(this);
        }
    }
}
