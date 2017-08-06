using Controller.Characters;
using Controller.Managers;
using Model.OverTimeEffects;

namespace Model.Events.Combat
{
    public class HoTEvent : CombatEvent
    {
        public GenericHoT HoT { get; set; }
        public CharController ToHoT { get; set; }

        public HoTEvent(CombatEventManager parent, GenericHoT hot, CharController toHot) :
            base(ECombatEv.HoT, parent)
        {
            this.HoT = hot;
            this.ToHoT = toHot;
            this.ToHoT.Model.AddHoT(hot);
            this.RegisterEvent();
        }
    }
}
