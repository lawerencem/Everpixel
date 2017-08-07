//using Controller.Characters;
//using Controller.Managers;
//using Model.OverTimeEffects;

//namespace Assets.Model.Event.Combat
//{
//    public class EvDoT : MCombatEv
//    {
//        public MDoT DoT { get; set; }
//        public CharController ToDoT { get; set; }

//        public EvDoT(CombatEventManager parent, MDoT dot, CharController toDot) :
//            base(ECombatEv.DoT, parent)
//        {
//            this.DoT = dot;
//            this.ToDoT = toDot;
//            this.ToDoT.Model.AddDoT(dot);
//            this.RegisterEvent();
//        }
//    }
//}
