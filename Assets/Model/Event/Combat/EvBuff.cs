//using Assets.Controller.Character;
//using Assets.Controller.Manager;
//using Assets.Model.Character.Param;

//namespace Assets.Model.Event.Combat
//{
//    public class EvBuff : MCombatEv
//    {
//        public string BuffStr { get; set; }
//        public CharController ToBuff { get; set; }

//        public EvBuff(CombatEventManager parent, FlatSecondaryStatModifier buff, CharController toBuff) :
//            base(ECombatEv.Buff, parent)
//        {
//            this.BuffStr = buff.Type.ToString().Replace("_", " ");
//            this.ToBuff = toBuff;
//            toBuff.Model.TryAddMod(buff);
//            this.RegisterEvent();
//        }

//        public EvBuff(CombatEventManager parent, SecondaryStatMod buff, CharController toBuff) :
//            base(ECombatEv.Buff, parent)
//        {
//            this.BuffStr = buff.Type.ToString().Replace("_", " ");
//            this.ToBuff = toBuff;
//            toBuff.Model.TryAddMod(buff);
//            this.RegisterEvent();
//        }
//    }
//}
