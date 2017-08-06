using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Character.Param
{
    public class CharPerkCollection
    {
        public List<MAbilityModPerk> AbilityModPerks;
        public List<MEquipmentPerk> EquipmentPerks;
        public List<MEquipmentSStatPerk> EquipmentSStatPerks;
        public List<GenericOnActionPerk> OnActionPerks;
        public List<MPostHitPerk> PostHitPerks;
        public List<GenericPreHitPerk> PreHitPerks;
        public List<MSStatModPerk> SStatModPerks;
        public List<MWhenHitPerk> WhenHitPerks;

        public CharPerkCollection()
        {
            this.AbilityModPerks = new List<MAbilityModPerk>();
            this.EquipmentPerks = new List<MEquipmentPerk>();
            this.EquipmentSStatPerks = new List<MEquipmentSStatPerk>();
            this.OnActionPerks = new List<GenericOnActionPerk>();
            this.PostHitPerks = new List<MPostHitPerk>();
            this.PreHitPerks = new List<GenericPreHitPerk>();
            this.SStatModPerks = new List<MSStatModPerk>();
            this.WhenHitPerks = new List<MWhenHitPerk>();
        }
    }
}
