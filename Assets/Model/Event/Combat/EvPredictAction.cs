//using Controller.Characters;
//using Controller.Managers;
//using Controller.Map;
//using Model.Abilities;
//using Model.Combat;
//using System.Collections.Generic;

//namespace Assets.Model.Event.Combat
//{
//    public class PredictionContainer
//    {
//        public Map Ability { get; set; }
//        public List<Hit> Hits { get; set; }
//        public CharController Source { get; set; }
//        public TileController Target { get; set; }

//        public PredictionContainer()
//        {
//            this.Hits = new List<Hit>();
//        }
//    }

//    public class EvPredictAction : MCombatEv
//    {
//        private Callback _callBack;
//        public delegate void Callback();

//        public PredictionContainer Container { get; set; }
        
//        public EvPredictAction(CombatEventManager parent, Callback callback = null) :
//            base(ECombatEv.PredictAction, parent)
//        {
//            this.Container = new PredictionContainer();
//            this._callBack = callback;
//            this._parent = parent;
//            this.RegisterEvent();
//        }

//        public void Process()
//        {
//            foreach (var hit in this.Container.Hits)
//                this.Container.Ability.PredictAbility(hit);
//        }
//    }
//}
