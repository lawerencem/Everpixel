//using System;

//namespace Controller.Map
//{
//    public class TileControllerFlags
//    {
//        [Flags]
//        public enum Flags
//        {
//            None = 0,
//            AwaitingAction = 1,
//        }

//        public Flags CurFlags { get; set; }

//        public TileControllerFlags()
//        {
//            this.CurFlags = new Flags();
//        }

//        public static void SetAllFlagsFalse(TileControllerFlags f)
//        {
//            foreach(var flag in Enum.GetValues(typeof(Flags)))
//                f.CurFlags &= ~(Flags)flag;
//        }

//        public static void SetAwaitingActionFlagFalse(TileControllerFlags f) { f.CurFlags &= ~Flags.AwaitingAction; }

//        public static void SetAwaitingActionFlagTrue(TileControllerFlags f) { f.CurFlags |= Flags.AwaitingAction; }

//        public static bool HasFlag(Flags a, Flags b) { return (a & b) == b; }
//    }
//}
