//using Controller.Characters;
//using Controller.Managers;
//using Model.Combat;
//using Model.Injuries;

//namespace Assets.Model.Event.Combat
//{
//    public class EvApplyInjury : MCombatEv
//    {
//        public MInjury Injury { get; set; }
//        public CharController Target { get; set; }

//        public EvApplyInjury(CombatEventManager parent, Hit hit, MInjury injury) :
//            base(ECombatEv.ApplyInjury, parent)
//        {
//            var exists = hit.Target.Model.Injuries.Find(x => x.Type == injury.Type);
//            if (exists == null)
//            {
//                this.Injury = injury;
//                this.Target = hit.Target;
//                this.Target.Model.AddInjury(injury);
//                this.RegisterEvent();
//            }
//        }
//    }
//}
