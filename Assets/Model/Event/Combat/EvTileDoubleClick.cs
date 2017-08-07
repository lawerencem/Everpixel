//using Controller.Managers;
//using Controller.Map;

//namespace Assets.Model.Event.Combat
//{
//    public class EvTileDoubleClick : MCombatEv
//    {
//        public TileController Tile { get; set; }

//        public EvTileDoubleClick(CombatEventManager parent, TileController t) : base(ECombatEv.TileDoubleClick, parent)
//        {
//            if (!this._parent.GetInteractionLock() && !this._parent.GetGUILock())
//            {
//                this.Tile = t;
//                this.RegisterEvent();
//            }
//        }
//    }
//}
