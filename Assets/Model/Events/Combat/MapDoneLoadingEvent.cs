using Controller.Characters;
using Controller.Managers;
using Controller.Managers.Map;
using Model.Map;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class MapDoneLoadingEvent : CombatEvent
    {
        public List<GenericCharacterController> LParty { get; set; }
        public List<GenericCharacterController> RParty { get; set; }
        public CombatMap Map { get; set; }
        public CombatMapLoader MapLoader { get; set; }

        public MapDoneLoadingEvent(CombatEventManager p, 
            List<GenericCharacterController> l, 
            List<GenericCharacterController> r, 
            CombatMap m,
            CombatMapLoader loader) : 
            base(CombatEventEnum.MapDoneLoading, p)
        {
            this.LParty = l;
            this.RParty = r;
            this.Map = m;
            this.MapLoader = MapLoader;
            this.RegisterEvent();
        }
    }
}
