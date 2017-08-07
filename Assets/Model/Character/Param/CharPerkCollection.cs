using Assets.Model.Perk.AbilityMod;
using Assets.Model.Perk.Equipment;
using Assets.Model.Perk.EquipmentSStat;
using Assets.Model.Perk.OnAction;
using Assets.Model.Perk.PostHit;
using Assets.Model.Perk.PreHit;
using Assets.Model.Perk.SStatMod;
using Assets.Model.Perk.WhenHit;
using System.Collections.Generic;

namespace Assets.Model.Character.Param
{
    public class CharPerkCollection
    {
        public List<MAbilityModPerk> AbilityModPerks;
        public List<MEquipmentPerk> EquipmentPerks;
        public List<MEquipmentSStatPerk> EquipmentSStatPerks;
        public List<MOnActionPerk> OnActionPerks;
        public List<MPostHitPerk> PostHitPerks;
        public List<MPreHitPerk> PreHitPerks;
        public List<MSStatModPerk> SStatModPerks;
        public List<MWhenHitPerk> WhenHitPerks;

        public CharPerkCollection()
        {
            this.AbilityModPerks = new List<MAbilityModPerk>();
            this.EquipmentPerks = new List<MEquipmentPerk>();
            this.EquipmentSStatPerks = new List<MEquipmentSStatPerk>();
            this.OnActionPerks = new List<MOnActionPerk>();
            this.PostHitPerks = new List<MPostHitPerk>();
            this.PreHitPerks = new List<MPreHitPerk>();
            this.SStatModPerks = new List<MSStatModPerk>();
            this.WhenHitPerks = new List<MWhenHitPerk>();
        }
    }
}
