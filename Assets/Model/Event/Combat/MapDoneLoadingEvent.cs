using Controller.Characters;
using Controller.Managers;
using Controller.Managers.Map;
using Model.Map;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class MapDoneLoadingEvent : CombatEvent
    {
        public List<CharController> LParty { get; set; }
        public List<CharController> RParty { get; set; }
        public CombatMap Map { get; set; }
        public CombatMapLoader MapLoader { get; set; }

        public MapDoneLoadingEvent(CombatEventManager p, 
            List<CharController> l, 
            List<CharController> r, 
            CombatMap m,
            CombatMapLoader loader) : 
            base(ECombatEv.MapDoneLoading, p)
        {
            this.LParty = l;
            this.RParty = r;
            this.Map = m;
            this.MapLoader = loader;
            this.RegisterEvent();
        }
    }
}
