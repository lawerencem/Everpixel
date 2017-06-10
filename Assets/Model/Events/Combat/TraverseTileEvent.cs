using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Map;

namespace Model.Events.Combat
{
    public class TraverseTileEvent : CombatEvent
    {
        public GenericCharacterController Character { get; set; }
        public Path Path { get; set; }
        public TileController Source { get; set; }
        public TileController Next { get; set; }      

        public TraverseTileEvent(
            CombatEventManager parent,
            Path p,
            TileController s, 
            TileController n) :
            base(CombatEventEnum.TraverseTile, parent)
        {
            if (s.Model.Current.GetType() == typeof(GenericCharacterController))
            {
                this.Character = s.Model.Current as GenericCharacterController;
                this.Path = p;
                this.Source = s;
                this.Next = n;

                if (this.Character.Model.CurrentAP >= this.Character.Model.GetTileTraversalAPCost(this.Next.Model))
                {
                    this.RegisterEvent();
                }
                else
                {
                    var traversed = new PathTraversedEvent(CombatEventManager.Instance, this.Character);
                }
            }
        }
    }
}

