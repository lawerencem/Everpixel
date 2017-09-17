using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Party;
using System.Collections.Generic;

namespace Assets.Controller.Manager.Combat
{
    public class CurrentlyActingData
    {
        public EAbility Ability { get; set; }
        public CChar CurrentlyActing { get; set; }
        public CWeapon CurrentWeapon { get; set; }
        public bool IsWpnAbility { get; set; }
        public bool LWeapon { get; set; }
    }

    public class CombatManagerData
    {
        public List<CChar> Characters;
        public MMapController Map;
        public List<CChar> InitiativeOrder;
        public List<MParty> LParties;
        public bool LWeapon;
        public List<CTile> PotentialTgtTiles;
        public List<MParty> RParties;
        public bool WpnAbility;

        public CombatManagerData()
        {
            this.Characters = new List<CChar>();
            this.InitiativeOrder = new List<CChar>();
            this.LParties = new List<MParty>();
            this.PotentialTgtTiles = new List<CTile>();
            this.RParties = new List<MParty>();
        }
    }
}
