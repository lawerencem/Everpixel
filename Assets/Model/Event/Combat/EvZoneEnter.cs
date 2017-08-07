//using Assets.Model.Zone;
//using Controller.Characters;
//using Controller.Managers;

//namespace Assets.Model.Event.Combat
//{
//    public class EvZoneEnter : MCombatEv
//    {
//        public CharController Character { get; set; }
//        public AZone Zone { get; set; }

//        public EvZoneEnter(CombatEventManager parent, CharController character, AZone zone)
//            : base(ECombatEv.ZoneEnter, parent)
//        {
//            this.Character = character;
//            this.Zone = zone;
//            this.RegisterEvent();
//            zone.ProcessEnterZone(this);
            
//        }
//    }
//}
