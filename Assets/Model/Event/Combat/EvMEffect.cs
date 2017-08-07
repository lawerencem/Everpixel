//using Characters.Params;
//using Controller.Characters;
//using Controller.Managers;
//using Model.Effects;

//namespace Assets.Model.Event.Combat
//{
//    public class EvMEffect : MCombatEv
//    {
//        public MEffect Effect { get; set; }
//        public CharController Target { get; set; }

//        public EvMEffect(CombatEventManager parent, CharController target, MEffect effect) :
//            base(ECombatEv.GenericEffect, parent)
//        {
//            this.Effect = effect;
//            this.Target = target;
//            this.Target.Model.AddEffect(this.Effect);
//            this.RegisterEvent();
//        }
//    }
//}
