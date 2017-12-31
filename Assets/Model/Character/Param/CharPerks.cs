﻿using Assets.Model.Perk;
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

        public void AddPerk(MPerk perk)
        {
            switch(perk.ArcheType)
            {
                case (EPerkArcheType.AbilityMod): { this._abilityModPerks.Add(perk as MAbilityModPerk); } break;
                case (EPerkArcheType.EquipmentModPerk): { this._equipmentSStatPerks.Add(perk as MEquipmentSStatPerk); } break;
                case (EPerkArcheType.EquipmentPerk): { this._equipmentPerks.Add(perk as MEquipmentPerk); } break;
                case (EPerkArcheType.OnActionPerk): { this._onActionPerks.Add(perk as MOnActionPerk); } break;
                case (EPerkArcheType.PostHitPerk): { this._postHitPerks.Add(perk as MPostHitPerk); } break;
                case (EPerkArcheType.PreHitPerk): { this._preHitPerks.Add(perk as MPreHitPerk); } break;
                case (EPerkArcheType.SStadModPerk): { this._sStatModPerks.Add(perk as MSStatModPerk); } break;
                case (EPerkArcheType.WhenHitPerk): { this._whenHitPerks.Add(perk as MWhenHitPerk); } break;
            }
        }
    }
}
