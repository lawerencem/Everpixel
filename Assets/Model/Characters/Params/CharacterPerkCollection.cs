using Model.Perks;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterPerkCollection
    {
        public List<GenericAbilityModPerk> AbilityModPerks;
        public List<GenericEquipmentSStatPerk> EquipmentSStatPerks;
        public List<GenericOnActionPerk> OnActionPerks;
        public List<GenericOnHitPerk> OnHitPerks;
        public List<GenericSStadModPerk> SStatModPerks;

        public CharacterPerkCollection()
        {
            this.AbilityModPerks = new List<GenericAbilityModPerk>();
            this.EquipmentSStatPerks = new List<GenericEquipmentSStatPerk>();
            this.OnActionPerks = new List<GenericOnActionPerk>();
            this.OnHitPerks = new List<GenericOnHitPerk>();
            this.SStatModPerks = new List<GenericSStadModPerk>();
        }
    }
}
