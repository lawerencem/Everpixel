using Assets.Model.Ability.Enum;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Param;
using Assets.Model.Equipment.Table;
using Assets.Template.Util;
using System.Xml.Linq;

namespace Assets.Model.Equipment.XML
{
    public class WeaponReader : MEquipmentReader
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

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);
            var tier = EEquipmentTier.None;
            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    var skill = att.Value;
                    foreach (var ele in el.Elements())
                    {
                        foreach (var attr in ele.Attributes())
                        {
                            var name = attr.Value;
                            foreach (var elem in ele.Elements())
                            {
                                this.HandleIndex(name, skill, elem.Name.ToString(), elem.Value, ref tier);
                            }
                            this.HandleWeaponSkillFromFile(name, skill, tier);
                        }
                    }
                }
            }
        }

        protected override void HandleIndex(string name, string skill, string param, string value, ref EEquipmentTier tier)
        {
            double v = 0;
            double.TryParse(value, out v);

            switch (param)
            {
                case ("Tier"): { HandleTierFromFile(name, value, ref tier); } break;
                case ("Accuracy"): { HandleStatsFromFile(name, EWeaponStat.Accuracy, v, tier); } break;
                case ("AP_Reduce"): { HandleStatsFromFile(name, EWeaponStat.AP_Reduce, v, tier); } break;
                case ("Armor_Ignore"): { HandleStatsFromFile(name, EWeaponStat.Armor_Ignore, v, tier); } break;
                case ("Armor_Pierce"): { HandleStatsFromFile(name, EWeaponStat.Armor_Pierce, v, tier); } break;
                case ("Block_Ignore"): { HandleStatsFromFile(name, EWeaponStat.Block_Ignore, v, tier); } break;
                case ("Damage"): { HandleStatsFromFile(name, EWeaponStat.Damage, v, tier); } break;
                case ("Description"): { } break;
                case ("Dodge_Reduce"): { HandleStatsFromFile(name, EWeaponStat.Dodge_Reduce, v, tier); } break;
                case ("Fatigue_Cost_Mod"): { HandleStatsFromFile(name, EWeaponStat.Fatigue_Cost_Mod, v, tier); } break;
                case ("Initiative_Reduce"): { HandleStatsFromFile(name, EWeaponStat.Initiative_Reduce, v, tier); } break;
                case ("Max_Durability"): { HandleStatsFromFile(name, EWeaponStat.Max_Durability, v, tier); } break;
                case ("Melee_Block_Chance"): { HandleStatsFromFile(name, EWeaponStat.Melee_Block_Chance, v, tier); } break;
                case ("Parry_Chance"): { HandleStatsFromFile(name, EWeaponStat.Parry_Chance, v, tier); } break;
                case ("Parry_Mod"): { HandleStatsFromFile(name, EWeaponStat.Parry_Mod, v, tier); } break;
                case ("Range_Mod"): { HandleStatsFromFile(name, EWeaponStat.Range_Mod, v, tier); } break;
                case ("Ranged_Block_Chance"): { HandleStatsFromFile(name, EWeaponStat.Ranged_Block_Chance, v, tier); } break;
                case ("Sprites"): { HandleSpritesFromFile(name, value, tier); } break;
                case ("Shield_Damage"): { HandleStatsFromFile(name, EWeaponStat.Shield_Damage, v, tier); } break;
                case ("Stamina_Reduce"): { HandleStatsFromFile(name, EWeaponStat.Stamina_Reduce, v, tier); } break;
                case ("Value"): { HandleStatsFromFile(name, EWeaponStat.Value, v, tier); } break;
                case ("WeaponEAbility"): { HandleWeaponAbilitiesFromFile(name, value, tier); } break;
                case ("EWeaponType"): { HandleWeaponTypeFromFile(name, value, tier); } break;
                case ("EWeaponUse"): { HandleWeaponUseFromFile(name, value, tier); } break;
            }
        }

        private void HandleStatsFromFile(string name, EWeaponStat x, double v, EEquipmentTier tier)
        {
            var stats = WeaponParamTable.Instance;
            var key = name + "_" + tier.ToString();

            switch (x)
            {
                case (EWeaponStat.Accuracy): { stats.Table[key].ArmorIgnore = v; } break;
                case (EWeaponStat.Armor_Ignore): { stats.Table[key].ArmorIgnore = v; } break;
                case (EWeaponStat.Armor_Pierce): { stats.Table[key].ArmorPierce = v; } break;
                case (EWeaponStat.AP_Reduce): { stats.Table[key].APReduce = v; } break;
                case (EWeaponStat.Block_Ignore): { stats.Table[key].BlockIgnore = v; } break;
                case (EWeaponStat.Damage): { stats.Table[key].Damage = (int)v; } break;
                case (EWeaponStat.Dodge_Reduce): { stats.Table[key].DodgeMod = v; } break;
                case (EWeaponStat.Max_Durability): { stats.Table[key].Durability = (int)v; } break;
                case (EWeaponStat.Fatigue_Cost_Mod): { stats.Table[key].FatigueCostMod = v; } break;
                case (EWeaponStat.Initiative_Reduce): { stats.Table[key].InitiativeReduce = v; } break;
                case (EWeaponStat.Melee_Block_Chance): { stats.Table[key].MeleeBlockChance = v; } break;
                case (EWeaponStat.Parry_Mod): { stats.Table[key].ParryMod = v; } break;
                case (EWeaponStat.Range_Mod): { stats.Table[key].RangeMod = (int)v; } break;
                case (EWeaponStat.Ranged_Block_Chance): { stats.Table[key].RangeBlockChance = v; } break;
                case (EWeaponStat.Shield_Damage): { stats.Table[key].ShieldDamage = v; } break;
                case (EWeaponStat.Stamina_Reduce): { stats.Table[key].StaminaReduce = v; } break;
                case (EWeaponStat.Value): { stats.Table[key].Value = v; } break;
            }
        }

        protected override void HandleTierFromFile(string name, string value, ref EEquipmentTier tier)
        {
            var x = EEquipmentTier.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EEquipmentTier>.TryGetEnumValue(value, ref x))
            {
                tier = x;
                var key = name + "_" + tier.ToString();
                stats.Table.Add(key, new WeaponParams());
                stats.Table[key].Tier = x;
                stats.Table[key].Name = name;
            }
        }

        private void HandleWeaponAbilitiesFromFile(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var ability = EAbility.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EAbility>.TryGetEnumValue(value, ref ability))
                stats.Table[key].Abilities.Add(ability);
        }

        private void HandleWeaponSkillFromFile(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var x = EWeaponSkill.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EWeaponSkill>.TryGetEnumValue(value, ref x))
                stats.Table[key].Skill = x;
        }

        private void HandleWeaponTypeFromFile(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var x = EWeaponType.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EWeaponType>.TryGetEnumValue(value, ref x))
                stats.Table[key].Type = x;
        }

        private void HandleWeaponUseFromFile(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var x = EWeaponUse.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EWeaponUse>.TryGetEnumValue(value, ref x))
                stats.Table[key].Use = x;
        }
    }
}
