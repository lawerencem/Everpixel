using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Action
{
    public class ActionData
    {
        public EAbility Ability { get; set; }
        public List<MHit> Hits { get; set; }
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
        public CChar Source { get; set; }
        public CTile Target { get; set; }
        public bool WpnAbility { get; set; }

        public ActionData()
        {
            this.Hits = new List<MHit>();
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
