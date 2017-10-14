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
    public class CharPerks
    {
        private List<MAbilityModPerk> _abilityModPerks;
        private List<MEquipmentPerk> _equipmentPerks;
        private List<MEquipmentSStatPerk> _equipmentSStatPerks;
        private List<MOnActionPerk> _onActionPerks;
        private List<MPostHitPerk> _postHitPerks;
        private List<MPreHitPerk> _preHitPerks;
        private List<MSStatModPerk> _sStatModPerks;
        private List<MWhenHitPerk> _whenHitPerks;

        public List<MAbilityModPerk> GetAbilityModPerks() { return this._abilityModPerks; }
        public List<MEquipmentPerk> GetEquipmentPerks() { return this._equipmentPerks; }
        public List<MEquipmentSStatPerk> GetEquipmentSStatPerks() { return this._equipmentSStatPerks; }
        public List<MOnActionPerk> GetOnActionPerks() { return this._onActionPerks; }
        public List<MPostHitPerk> GetPostHitPerks() { return this._postHitPerks; }
        public List<MPreHitPerk> GetPreHitPerks() { return this._preHitPerks; }
        public List<MSStatModPerk> GetSStatModPerks() { return this._sStatModPerks; }
        public List<MWhenHitPerk> GetWhenHitPerks() { return this._whenHitPerks; }

        public CharPerks()
        {
            this._abilityModPerks = new List<MAbilityModPerk>();
            this._equipmentPerks = new List<MEquipmentPerk>();
            this._equipmentSStatPerks = new List<MEquipmentSStatPerk>();
            this._onActionPerks = new List<MOnActionPerk>();
            this._postHitPerks = new List<MPostHitPerk>();
            this._preHitPerks = new List<MPreHitPerk>();
            this._sStatModPerks = new List<MSStatModPerk>();
            this._whenHitPerks = new List<MWhenHitPerk>();
        }
    }
}
