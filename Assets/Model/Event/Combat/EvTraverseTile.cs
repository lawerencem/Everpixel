//using Controller.Characters;
//using Controller.Managers;
//using Controller.Map;
//using Model.Map;

//namespace Assets.Model.Event.Combat
//{
//    public class EvTraverseTile : MCombatEv
//    {
//        public CharController Character { get; set; }
//        public Path Path { get; set; }
//        public TileController Source { get; set; }
//        public TileController Next { get; set; }      

//        public EvTraverseTile(
//            CombatEventManager parent,
//            Path p,
//            TileController s, 
//            TileController n) :
//            base(ECombatEv.TraverseTile, parent)
//        {
//            if (s.Model.Current.GetType() == typeof(CharController))
//            {
//                this.Character = s.Model.Current as CharController;
//                this.Path = p;
//                this.Source = s;
//                this.Next = n;

//                if (this.Character.Model.GetCurrentAP() >= this.Character.Model.GetTileTraversalAPCost(this.Next.Model))
//                {
//                    this.RegisterEvent();
//                }
//                else
//                {
//                    var traversed = new EvPathTraversed(CombatEventManager.Instance, this.Character);
//                }
//            }
//        }
//    }
//}
