using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Map;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class MapDoneLoadingEvent : CombatEvent
    {
        public List<GenericCharacterController> Controllers { get; set; }
        public CombatMap Map { get; set; }

        public MapDoneLoadingEvent(CombatEventManager p, List<GenericCharacterController> c, CombatMap m) : 
            base(CombatEventEnum.MapDoneLoading, p)
        {
            this.Controllers = c;
            this.Map = m;
            this.RegisterEvent();
        }
    }
}
