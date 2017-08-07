//using Controller.Characters;
//using Controller.Managers;
//using Model.Shields;

//namespace Assets.Model.Event.Combat
//{
//    public class EvShield : MCombatEv
//    {
//        public CharController ToShield { get; set; }

//        public EvShield(CombatEventManager parent, Shield shield, CharController toShield) :
//            base(ECombatEv.Shield, parent)
//        {
//            this.ToShield = toShield;
//            this.ToShield.Model.AddShield(shield);
//            this.RegisterEvent();
//        }
//    }
//}
