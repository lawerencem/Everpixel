using System;
using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using System.Collections.Generic;
using Model.Events;

namespace Model.Equipment.XML
{
    public class ArmorReader : GenericEquipmentReader
    {
        private static ArmorReader _instance;
        public ArmorReader() { this._path = "Assets/Model/Equipment/XML/Armors.xml"; }

        public static ArmorReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ArmorReader();
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
                case ("AP_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.AP_Reduce, v, tier); } break;
                case ("ArmorSkillEnum"): { HandleArmorSkillFromFile(name, value, tier); } break;
                case ("ArmorTypeEnum"): { HandleArmorTypeFromFile(name, value, tier); } break;
                case ("Block_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.Block_Reduce, v, tier); } break;
                case ("Damage_Ignore"): { HandleStatsFromFile(name, ArmorStatsEnum.Damage_Ignore, v, tier); } break;
                case ("Damage_Reduction"): { HandleStatsFromFile(name, ArmorStatsEnum.Damage_Reduction, v, tier); } break;
                case ("Description"): { } break;
                case ("Dodge_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.Dodge_Reduce, v, tier); } break;
                case ("Durability"): { HandleStatsFromFile(name, ArmorStatsEnum.Durability, v, tier); } break;
                case ("Fatigue_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.Fatigue_Reduce, v, tier); } break;
                case ("Initiative_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.Initiative_Reduce, v, tier); } break;
                case ("Parry_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.Parry_Reduce, v, tier); } break;
                case ("Sprites"): { HandleSpritesFromFile(name, value, tier); } break;
                case ("Stamina_Reduce"): { HandleStatsFromFile(name, ArmorStatsEnum.Stamina_Reduce, v, tier); } break;
                case ("Value"): { HandleStatsFromFile(name, ArmorStatsEnum.Value, v, tier); } break;
            }
        }

        private void HandleStatsFromFile(string name, ArmorStatsEnum x, int v, EquipmentTierEnum tier)
        {
            var stats = ArmorParamTable.Instance;
            var key = name + "_" + tier.ToString();

            switch (x)
            {
                case (ArmorStatsEnum.AP_Reduce): { stats.Table[key].APReduce = v; } break;
                case (ArmorStatsEnum.Block_Reduce): { stats.Table[key].BlockReduce = v; } break;
                case (ArmorStatsEnum.Damage_Ignore): { stats.Table[key].DamageIgnore = v; } break;
                case (ArmorStatsEnum.Damage_Reduction): { stats.Table[key].DamageReduction = v; } break;
                case (ArmorStatsEnum.Dodge_Reduce): { stats.Table[key].DodgeReduce = v; } break;
                case (ArmorStatsEnum.Durability): { stats.Table[key].Durability = v; } break;
                case (ArmorStatsEnum.Fatigue_Reduce): { stats.Table[key].FatigueCost = v; } break;
                case (ArmorStatsEnum.Initiative_Reduce): { stats.Table[key].InitiativeReduce = v; } break;
                case (ArmorStatsEnum.Parry_Reduce): { stats.Table[key].ParryReduce = v; } break;
                case (ArmorStatsEnum.Stamina_Reduce): { stats.Table[key].StaminaReduce = v; } break;
                case (ArmorStatsEnum.Value): { stats.Table[key].Value = v; } break;
            }
        }

        protected override void HandleTierFromFile(string name, string value, ref EquipmentTierEnum tier)
        {
            var x = EquipmentTierEnum.None;
            var stats = ArmorParamTable.Instance;
            if (EnumUtil<EquipmentTierEnum>.TryGetEnumValue(value, ref x))
            {
                tier = x;
                var key = name + "_" + tier.ToString();
                stats.Table.Add(key, new ArmorParams());
                stats.Table[key].Tier = x;
                stats.Table[key].Name = name;
            }
        }

        private void HandleArmorSkillFromFile(string name, string value, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();

            var x = ArmorSkillEnum.None;
            var stats = ArmorParamTable.Instance;
            if (EnumUtil<ArmorSkillEnum>.TryGetEnumValue(value, ref x))
                stats.Table[key].Skill = x;
        }

        private void HandleArmorTypeFromFile(string name, string value, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();

            var x = ArmorTypeEnum.None;
            var stats = ArmorParamTable.Instance;
            if (EnumUtil<ArmorTypeEnum>.TryGetEnumValue(value, ref x))
                stats.Table[key].Type = x;
        }
    }
}
