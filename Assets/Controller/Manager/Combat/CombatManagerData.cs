using Assets.Controller.Character;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Party;
using System.Collections.Generic;

namespace Assets.Controller.Manager.Combat
{
    public class CombatManagerData
    {
        public List<CharController> Characters;
        public EAbility CurrentAbility;
        public CharController CurrentlyActing;
        public MMapController Map;
        public List<CharController> InitiativeOrder;
        public List<MParty> LParties;
        public bool LWeapon;
        public List<TileController> PotentialTgtTiles;
        public List<MParty> RParties;

        public CombatManagerData()
        {
            this.Characters = new List<CharController>();
            this.InitiativeOrder = new List<CharController>();
            this.LParties = new List<MParty>();
            this.PotentialTgtTiles = new List<TileController>();
            this.RParties = new List<MParty>();
        }
    }
}
