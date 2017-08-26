using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Action
{
    public class ActionData
    {
        public EAbility Ability { get; set; }
        public List<Hit> Hits { get; set; }
        public bool LWeapon { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }

        public ActionData()
        {
            this.Hits = new List<Hit>();
        }

        public bool Initialized()
        {
            if (this.Ability != EAbility.None && this.Source != null)
                return true;
            else
                return false;
        }
    }
}
