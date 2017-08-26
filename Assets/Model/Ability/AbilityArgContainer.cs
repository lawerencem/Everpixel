using Assets.Controller.Character;
using Assets.Controller.Map.Tile;

namespace Assets.Model.Ability
{
    public class AbilityArgContainer
    {
        public int Range { get; set; }
        public bool LWeapon { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }
    }
}
