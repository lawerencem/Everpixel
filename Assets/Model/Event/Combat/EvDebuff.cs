//using Assets.Controller.Character;
//using Assets.Controller.Manager;
//using Assets.Model.Character.Param;

//namespace Assets.Model.Event.Combat
//{
//    public class EvDebuff : MCombatEv
//    {
//        private FlatSecondaryStatModifier _flatDebuff;
//        private SecondaryStatMod _debuff;

//        public CharController ToDebuff { get; set; }
//        public bool Resisted { get; set; }

//        public EvDebuff(CombatEventManager parent, FlatSecondaryStatModifier debuff, CharController toDebuff) :
//            base(ECombatEv.Debuff, parent)
//        {
//            this.ToDebuff = toDebuff;
//            this.Process();
//        }

//        public EvDebuff(CombatEventManager parent, SecondaryStatMod debuff, CharController toDebuff) :
//            base(ECombatEv.Debuff, parent)
//        {
//            this.ToDebuff = toDebuff;
//            this.Process();
//        }

//        private void Process()
//        {
//            // TODO:
//            this.RegisterEvent();
//        }
//    }
//}
