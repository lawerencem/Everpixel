//using Controller.Characters;
//using Controller.Managers;
//using Controller.Map;

//namespace Assets.Model.Event.Combat
//{
//    public class ShowPotentialPathEvent : MCombatEv
//    {
//        public CharController Character;
//        public TileController Target;

//        public ShowPotentialPathEvent(
//            CharController c,
//            TileController t,
//            CombatEventManager parent) :
//            base(ECombatEv.ShowPotentialPath, parent)
//        {
//            if (!this._parent.GetInteractionLock() && !this._parent.GetGUILock())
//            {
//                this.Character = c;
//                this.Target = t;
//                this.RegisterEvent();
//            }
//        }
//    }
//}
