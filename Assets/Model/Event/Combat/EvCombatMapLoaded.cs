//using Controller.Characters;
//using Controller.Managers;
//using Controller.Managers.Map;
//using Model.Map;
//using System.Collections.Generic;

//namespace Assets.Model.Event.Combat
//{
//    public class EvCombatMapLoaded : MCombatEv
//    {
//        public List<CharController> LParty { get; set; }
//        public List<CharController> RParty { get; set; }
//        public CombatMap Map { get; set; }
//        public CombatMapLoader MapLoader { get; set; }

//        public EvCombatMapLoaded(CombatEventManager p, 
//            List<CharController> l, 
//            List<CharController> r, 
//            CombatMap m,
//            CombatMapLoader loader) : 
//            base(ECombatEv.MapDoneLoading, p)
//        {
//            this.LParty = l;
//            this.RParty = r;
//            this.Map = m;
//            this.MapLoader = loader;
//            this.RegisterEvent();
//        }
//    }
//}
