using Assets.Controller.Character;
using Assets.Controller.Map.Combat;
using Assets.Model.Ability.Enum;
using Assets.Model.Party;
using System.Collections.Generic;

namespace Assets.Controller.Manager.Combat
{
    public class CombatManagerData
    {
        public List<CharController> Characters;
        public EAbility CurrenAbility;
        public CharController CurrentlyActing;
        public MMapController Map;
        public List<CharController> InitiativeOrder;
        public List<MParty> LParties;
        public List<MParty> RParties;

        public CombatManagerData()
        {
            this.Characters = new List<CharController>();
            this.InitiativeOrder = new List<CharController>();
            this.LParties = new List<MParty>();
            this.RParties = new List<MParty>();
        }
    }
}
