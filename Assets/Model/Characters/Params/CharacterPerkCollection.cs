using Model.Perks;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterPerkCollection
    {
        public List<GenericAbilityModPerk> AbilityModPerks;
        public List<GenericEquipmentSStatPerk> EquipmentSStatPerks;
        public List<GenericOnActionPerk> OnActionPerks;
        public List<GenericPostHitPerk> PostHitPerks;
        public List<GenericPreHitPerk> PreHitPerks;
        public List<GenericSStadModPerk> SStatModPerks;
        public List<GenericWhenHitPerk> WhenHitPerks;

        public CharacterPerkCollection()
        {
            this.AbilityModPerks = new List<GenericAbilityModPerk>();
            this.EquipmentSStatPerks = new List<GenericEquipmentSStatPerk>();
            this.OnActionPerks = new List<GenericOnActionPerk>();
            this.PostHitPerks = new List<GenericPostHitPerk>();
            this.PreHitPerks = new List<GenericPreHitPerk>();
            this.SStatModPerks = new List<GenericSStadModPerk>();
            this.WhenHitPerks = new List<GenericWhenHitPerk>();
        }
    }
}
