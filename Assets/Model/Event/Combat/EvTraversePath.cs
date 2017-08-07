//using Controller.Characters;
//using Controller.Managers;
//using Model.Map;

//namespace Model.Events.Combat
//{
//    public class EvTraversePath : MCombatEv
//    {
//        public CharController Character { get; set; }
//        public Path Path { get; set; }

//        public EvTraversePath(CombatEventManager parent, CharController c, Path p) : 
//            base(ECombatEv.TraversePath, parent)
//        {
//            if (!this._parent.GetInteractionLock())
//            {
//                this._parent.LockInteraction();
//                this.Character = c;
//                this.Path = p;
//                this.RegisterEvent();
//            }
//        }
//    }
//}
