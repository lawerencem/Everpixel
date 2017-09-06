using Assets.Data.Equipment.Table;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Param;
using Assets.Template.Util;
using System.Xml.Linq;

namespace Assets.Data.Equipment.XML
{
    public class ArmorReader : MEquipmentReader
    {
        private static ArmorReader _instance;
        public ArmorReader() : base()
        {
            this._paths.Add("Assets/Data/Equipment/XML/Armor/HeavyArmor.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Armor/HeavyHelm.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Armor/LightArmor.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Armor/LightHelm.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Armor/MediumArmor.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Armor/MediumHelm.xml");
        }

        public static ArmorReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ArmorReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var tier = EEquipmentTier.None;
                foreach (var el in doc.Root.Elements())
                {
                    foreach (var att in el.Attributes())
                    {
                        var type = att.Value;

                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
                            {
                                var name = attr.Value;
                                foreach (var elem in ele.Elements())
                                {
                                    this.HandleIndex(name, type, elem.Name.ToString(), elem.Value, ref tier);
                                }
                                this.HandleArmorTypeFromFile(name, type, tier);
                            }
                        }
                    }
                }
            }
        }

        protected override void HandleIndex(string name, string type, string param, string value, ref EEquipmentTier tier)
        {
            double v = 0;
            double.TryParse(value, out v);

            switch (param)
            {
                case ("Tier"): { HandleTierFromFile(name, value, ref tier); } break;
                case ("AP_Reduce"): { HandleStatsFromFile(name, EArmorStat.AP_Reduce, v, tier); } break;
                case ("Block_Reduce"): { HandleStatsFromFile(name, EArmorStat.Block_Reduce, v, tier); } break;
                case ("Damage_Ignore"): { HandleStatsFromFile(name, EArmorStat.Damage_Ignore, v, tier); } break;
                case ("Damage_Reduction"): { HandleStatsFromFile(name, EArmorStat.Damage_Reduction, v, tier); } break;
                case ("Description"): { } break;
                case ("Dodge_Reduce"): { HandleStatsFromFile(name, EArmorStat.Dodge_Reduce, v, tier); } break;
                case ("Durability"): { HandleStatsFromFile(name, EArmorStat.Durability, v, tier); } break;
                case ("Fatigue_Cost"): { HandleStatsFromFile(name, EArmorStat.Fatigue_Cost, v, tier); } break;
                case ("Initiative_Reduce"): { HandleStatsFromFile(name, EArmorStat.Initiative_Reduce, v, tier); } break;
                case ("Parry_Reduce"): { HandleStatsFromFile(name, EArmorStat.Parry_Reduce, v, tier); } break;
                case ("Sprites"): { HandleSpritesFromFile(name, value, tier); } break;
                case ("Stamina_Reduce"): { HandleStatsFromFile(name, EArmorStat.Stamina_Reduce, v, tier); } break;
                case ("Value"): { HandleStatsFromFile(name, EArmorStat.Value, v, tier); } break;
            }
        }

        private void HandleStatsFromFile(string name, EArmorStat x, double v, EEquipmentTier tier)
        {
            var stats = ArmorParamTable.Instance;
            var key = name + "_" + tier.ToString();

            switch (x)
            {
                case (EArmorStat.AP_Reduce): { stats.Table[key].APReduce = v; } break;
                case (EArmorStat.Block_Reduce): { stats.Table[key].BlockReduce = v; } break;
                case (EArmorStat.Damage_Ignore): { stats.Table[key].DamageIgnore = v; } break;
                case (EArmorStat.Damage_Reduction): { stats.Table[key].DamageReduction = v; } break;
                case (EArmorStat.Dodge_Reduce): { stats.Table[key].DodgeMod = v; } break;
                case (EArmorStat.Durability): { stats.Table[key].Durability = (int)v; } break;
                case (EArmorStat.Fatigue_Cost): { stats.Table[key].FatigueCost = v; } break;
                case (EArmorStat.Initiative_Reduce): { stats.Table[key].InitiativeReduce = v; } break;
                case (EArmorStat.Parry_Reduce): { stats.Table[key].ParryReduce = v; } break;
                case (EArmorStat.Stamina_Reduce): { stats.Table[key].StaminaReduce = v; } break;
                case (EArmorStat.Value): { stats.Table[key].Value = v; } break;
            }
        }

        protected override void HandleTierFromFile(string name, string value, ref EEquipmentTier tier)
        {
            var x = EEquipmentTier.None;
            var stats = ArmorParamTable.Instance;
            if (EnumUtil<EEquipmentTier>.TryGetEnumValue(value, ref x))
            {
                tier = x;
                var key = name + "_" + tier.ToString();
                stats.Table.Add(key, new ArmorParams());
                stats.Table[key].Tier = x;
                stats.Table[key].Name = name;
            }
        }

        private void HandleArmorTypeFromFile(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var x = EArmorType.None;
            var stats = ArmorParamTable.Instance;
            if (EnumUtil<EArmorType>.TryGetEnumValue(value, ref x))
                stats.Table[key].Type = x;
        }
    }
}
