using Model.Abilities;
using Model.Classes;
using Model.Equipment;
using Model.Mounts;
using Model.Parties;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterPerkCollection
    {
        public List<GenericAbilityModPerk> AbilityModPerks;
        public List<GenericEquipmentSStatPerk> EquipmentSStatPerks;
        public List<GenericOnHitPerk> OnHitPerks;
        public List<GenericSStadModPerk> SStatModPerks;

        public CharacterPerkCollection()
        {
            this.AbilityModPerks = new List<GenericAbilityModPerk>();
            this.EquipmentSStatPerks = new List<GenericEquipmentSStatPerk>();
            this.OnHitPerks = new List<GenericOnHitPerk>();
            this.SStatModPerks = new List<GenericSStadModPerk>();
        }
    }
}
