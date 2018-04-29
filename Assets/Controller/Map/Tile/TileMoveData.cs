using Assets.Controller.Character;
using Assets.Model.Event.Combat;
using Assets.Template.CB;

namespace Assets.Controller.Map.Tile
{
    public class TileMoveData
    {
        public bool DoAttackOfOpportunity { get; set; }
        public Callback Callback { get; set; }
        public MEvCombat ParentEvent { get; set; }
        public CChar Target { get; set; }
    }
}
