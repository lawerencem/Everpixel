//using Controller.Characters;
//using Controller.Managers;
//using Model.OverTimeEffects;

//namespace Assets.Model.Event.Combat
//{
//    public class EvHoT : MCombatEv
//    {
//        public GenericHoT HoT { get; set; }
//        public CharController ToHoT { get; set; }

//        public EvHoT(CombatEventManager parent, GenericHoT hot, CharController toHot) :
//            base(ECombatEv.HoT, parent)
//        {
//            this.HoT = hot;
//            this.ToHoT = toHot;
//            this.ToHoT.Model.AddHoT(hot);
//            this.RegisterEvent();
//        }
//    }
//}
