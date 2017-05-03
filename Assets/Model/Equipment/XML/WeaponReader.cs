using System;
using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using System.Collections.Generic;
using Model.Events;

namespace Model.Equipment.XML
{
    public class WeaponReader : GenericEquipmentReader
    {
        private static WeaponReader _instance;

        public WeaponReader() { this._path = "Assets/Model/Equipment/XML/Weapons.xml"; }

        public static WeaponReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WeaponReader();
                return _instance;
            }
        }

        protected override void HandleIndex(string name, string param, string value, ref EquipmentTierEnum tier)
        {
            int v = 0;
            int.TryParse(value, out v);

            switch (param)
            {
                case ("Tier"): { HandleTierFromFile(name, value, ref tier); } break;
                case ("Accuracy"): { HandleStatsFromFile(name, WeaponStatsEnum.Accuracy, v, tier); } break;
                case ("AP_Reduce"): { HandleStatsFromFile(name, WeaponStatsEnum.AP_Reduce, v, tier); } break;
                case ("Armor_Ignore"): { HandleStatsFromFile(name, WeaponStatsEnum.Armor_Ignore, v, tier); } break;
                case ("Armor_Pierce"): { HandleStatsFromFile(name, WeaponStatsEnum.Armor_Pierce, v, tier); } break;
                case ("Block_Ignore"): { HandleStatsFromFile(name, WeaponStatsEnum.Block_Ignore, v, tier); } break;
                case ("Damage"): { HandleStatsFromFile(name, WeaponStatsEnum.Damage, v, tier); } break;
                case ("Description"): { } break;
                case ("Dodge_Reduce"): { HandleStatsFromFile(name, WeaponStatsEnum.Dodge_Reduce, v, tier); } break;
                case ("Fatigue_Cost"): { HandleStatsFromFile(name, WeaponStatsEnum.Fatigue_Cost, v, tier); } break;
                case ("Fatigue_Reduce"): { HandleStatsFromFile(name, WeaponStatsEnum.Fatigue_Reduce, v, tier); } break;
                case ("Initiative_Reduce"): { HandleStatsFromFile(name, WeaponStatsEnum.Initiative_Reduce, v, tier); } break;
                case ("Max_Durability"): { HandleStatsFromFile(name, WeaponStatsEnum.Max_Durability, v, tier); } break;
                case ("Melee_Block_Chance"): { HandleStatsFromFile(name, WeaponStatsEnum.Melee_Block_Chance, v, tier); } break;
                case ("Parry_Chance"): { HandleStatsFromFile(name, WeaponStatsEnum.Parry_Chance, v, tier); } break;
                case ("Parry_Reduce"): { HandleStatsFromFile(name, WeaponStatsEnum.Parry_Reduce, v, tier); } break;
                case ("Range"): { HandleStatsFromFile(name, WeaponStatsEnum.Range, v, tier); } break;
                case ("Ranged_Block_Chance"): { HandleStatsFromFile(name, WeaponStatsEnum.Ranged_Block_Chance, v, tier); } break;
                case ("Sprites"): { HandleSpritesFromFile(name, value, tier); } break;
                case ("Shield_Damage"): { HandleStatsFromFile(name, WeaponStatsEnum.Shield_Damage, v, tier); } break;
                case ("Stamina_Reduce"): { HandleStatsFromFile(name, WeaponStatsEnum.Stamina_Reduce, v, tier); } break;
                case ("Value"): { HandleStatsFromFile(name, WeaponStatsEnum.Value, v, tier); } break;
                case ("WeaponAbilitiesEnum"): { HandleWeaponAbilitiesFromFile(name, value, tier); } break;
                case ("WeaponSkillEnum"): { HandleWeaponSkillFromFile(name, value, tier); } break;
                case ("WeaponTypeEnum"): { HandleWeaponTypeFromFile(name, value, tier); } break;
                case ("WeaponUseEnum"): { HandleWeaponUseFromFile(name, value, tier); } break;
            }
        }

        private void HandleStatsFromFile(string name, WeaponStatsEnum x, int v, EquipmentTierEnum tier)
        {
            var stats = WeaponParamTable.Instance;
            var key = name + "_" + tier.ToString();

            switch (x)
            {
                case (WeaponStatsEnum.Accuracy): { stats.Table[key].ArmorIgnore = v; } break;
                case (WeaponStatsEnum.Armor_Ignore): { stats.Table[key].ArmorIgnore = v; } break;
                case (WeaponStatsEnum.Armor_Pierce): { stats.Table[key].ArmorPierce = v; } break;
                case (WeaponStatsEnum.AP_Reduce): { stats.Table[key].APReduce = v; } break;
                case (WeaponStatsEnum.Block_Ignore): { stats.Table[key].BlockIgnore = v; } break;
                case (WeaponStatsEnum.Damage): { stats.Table[key].Damage = v; } break;
                case (WeaponStatsEnum.Dodge_Reduce): { stats.Table[key].DodgeReduce = v; } break;
                case (WeaponStatsEnum.Max_Durability): { stats.Table[key].Durability = v; } break;
                case (WeaponStatsEnum.Fatigue_Cost): { stats.Table[key].FatigueCost = v; } break;
                case (WeaponStatsEnum.Fatigue_Reduce): { stats.Table[key].FatigueReduce = v; } break;
                case (WeaponStatsEnum.Initiative_Reduce): { stats.Table[key].InitiativeReduce = v; } break;
                case (WeaponStatsEnum.Melee_Block_Chance): { stats.Table[key].MeleeBlockChance = v; } break;
                case (WeaponStatsEnum.Parry_Chance): { stats.Table[key].ParryChance = v; } break;
                case (WeaponStatsEnum.Parry_Reduce): { stats.Table[key].ParryReduce = v; } break;
                case (WeaponStatsEnum.Range): { stats.Table[key].Range = v; } break;
                case (WeaponStatsEnum.Ranged_Block_Chance): { stats.Table[key].RangeBlockChance = v; } break;
                case (WeaponStatsEnum.Shield_Damage): { stats.Table[key].ShieldDamage = v; } break;
                case (WeaponStatsEnum.Stamina_Reduce): { stats.Table[key].StaminaReduce = v; } break;
                case (WeaponStatsEnum.Value): { stats.Table[key].Value = v; } break;
            }
        }

        protected override void HandleTierFromFile(string name, string value, ref EquipmentTierEnum tier)
        {
            var x = EquipmentTierEnum.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EquipmentTierEnum>.TryGetEnumValue(value, ref x))
            {
                tier = x;
                var key = name + "_" + tier.ToString();
                stats.Table.Add(key, new WeaponParams());
                stats.Table[key].Tier = x;
                stats.Table[key].Name = name;
            }
        }

        private void HandleWeaponAbilitiesFromFile(string name, string value, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();

            var ability = WeaponAbilitiesEnum.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<WeaponAbilitiesEnum>.TryGetEnumValue(value, ref ability))
                stats.Table[key].Abilities.Add(ability);
        }

        private void HandleWeaponSkillFromFile(string name, string value, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();

            var x = WeaponSkillEnum.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<WeaponSkillEnum>.TryGetEnumValue(value, ref x))
                stats.Table[key].Skill = x;
        }

        private void HandleWeaponTypeFromFile(string name, string value, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();

            var x = WeaponTypeEnum.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<WeaponTypeEnum>.TryGetEnumValue(value, ref x))
                stats.Table[key].Type = x;
        }

        private void HandleWeaponUseFromFile(string name, string value, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();

            var x = WeaponUseEnum.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<WeaponUseEnum>.TryGetEnumValue(value, ref x))
                stats.Table[key].Use = x;
        }
    }
}
