//using Controller.Managers;
//using Model.Abilities;

//namespace Assets.Model.Event.Combat
//{
//    public class EvAttackSelected : MCombatEv
//    {
//        public object AttackType { get; set; }
//        public bool RWeapon { get; set; }

//        public EvAttackSelected(CombatEventManager parent, bool rWeapon, EAbility type, bool selfCast = false) :
//            base(ECombatEv.AttackSelected, parent)
//        {
//            if (!this._parent.GetInteractionLock())
//            {
//                this.AttackType = type;
//                this.RWeapon = rWeapon;
//                this.RegisterEvent();
//            }
//        }

//        public EvAttackSelected(CombatEventManager parent, EAbility type, bool selfCast = false) : 
//            base(ECombatEv.AttackSelected, parent)
//        {
//            if (!this._parent.GetInteractionLock())
//            {
//                this.AttackType = type;
//                this.RegisterEvent();
//            }
//        }
//    }
//}
