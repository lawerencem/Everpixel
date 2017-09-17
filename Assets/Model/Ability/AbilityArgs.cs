using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Equipment.Armor;

namespace Assets.Model.Ability
{
    public class AbilityArgs
    {
        public int AoE { get; set; }
        public int Range { get; set; }
        public bool LWeapon { get; set; }
        public CChar Source { get; set; }
        public CTile Target { get; set; }
        public bool WpnAbility { get; set; }
    }
}
