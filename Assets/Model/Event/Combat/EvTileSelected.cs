//using Controller.Managers;
//using Controller.Map;

//namespace Assets.Model.Event.Combat
//{
//    public class EvTileSelected : MCombatEv
//    {
//        public TileController Selected { get; set; }

//        public EvTileSelected(TileController t, CombatEventManager parent) :
//            base(ECombatEv.HexSelectedForMove, parent)
//        {
//            if (!this._parent.GetInteractionLock() && !this._parent.GetGUILock())
//            {
//                this.Selected = t;
//                this.RegisterEvent();
//            }
//        }
//    }
//}