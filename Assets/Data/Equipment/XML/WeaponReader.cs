using Assets.Model.Ability.Enum;
using Assets.Model.Effect;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Table;
using Assets.Model.Equipment.Weapon;
using Assets.Template.Util;
using Assets.View.Fatality;
using System.Xml.Linq;

namespace Assets.Data.Equipment.XML
{
    public class WeaponReader : MEquipmentReader
    {
        private static WeaponReader _instance;

        public WeaponReader() : base()
        {
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Axe.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Flail.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Hammer.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/LargeShield.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/LightCrossbow.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/MediumShield.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Polearm.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/ShortBow.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/SmallShield.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Spear.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Staff.xml");
            this._paths.Add("Assets/Data/Equipment/XML/Weapon/Sword.xml");
        }

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
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);
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
                                    this.HandleIndex(name, skill, elem, elem.Value, ref tier);
                                    //this.HandleIndex(name, skill, elem.Name.ToString(), elem.Value, ref tier);
                                }
                                this.HandleWeaponSkillFromFile(name, skill, tier);
                            }
                        }
                    }
                }
            }
        }

        protected void HandleIndex(string name, string skill, XElement elem, string value, ref EEquipmentTier tier)
        {
            double v = 0;
            double.TryParse(value, out v);

            switch (elem.Name.ToString())
            {
                case ("Tier"): { HandleTierFromFile(name, value, ref tier); } break;
                case ("Accuracy"): { HandleStatsFromFile(name, EWeaponStat.Accuracy_Mod, v, tier); } break;
                case ("AP_Mod"): { HandleStatsFromFile(name, EWeaponStat.AP_Mod, v, tier); } break;
                case ("Armor_Ignore"): { HandleStatsFromFile(name, EWeaponStat.Armor_Ignore, v, tier); } break;
                case ("Armor_Pierce"): { HandleStatsFromFile(name, EWeaponStat.Armor_Pierce, v, tier); } break;
                case ("Block_Ignore"): { HandleStatsFromFile(name, EWeaponStat.Block_Ignore, v, tier); } break;
                case ("Custom_Bullet"): { this.HandleCustomBullet(name, value, tier); } break;
                case ("Custom_Fatality"): { this.HandleCustomFatality(name, value, tier); } break;
                case ("Damage"): { HandleStatsFromFile(name, EWeaponStat.Damage, v, tier); } break;
                case ("Description"): { } break;
                case ("Dodge_Mod"): { HandleStatsFromFile(name, EWeaponStat.Dodge_Mod, v, tier); } break;
                case ("EEffect"): { this.HandleEffects(elem, name, tier); } break;
                case ("Embed"): { this.HandleEmbed(name, value, tier); } break;
                case ("Initiative_Mod"): { HandleStatsFromFile(name, EWeaponStat.Initiative_Mod, v, tier); } break;
                case ("Max_Durability"): { HandleStatsFromFile(name, EWeaponStat.Max_Durability, v, tier); } break;
                case ("Melee_Block_Chance"): { HandleStatsFromFile(name, EWeaponStat.Melee_Block_Chance, v, tier); } break;
                case ("Parry_Mod"): { HandleStatsFromFile(name, EWeaponStat.Parry_Mod, v, tier); } break;
                case ("Range_Mod"): { HandleStatsFromFile(name, EWeaponStat.Range_Mod, v, tier); } break;
                case ("Ranged_Block_Chance"): { HandleStatsFromFile(name, EWeaponStat.Ranged_Block_Chance, v, tier); } break;
                case ("Sprites"): { HandleSpritesFromFile(name, value, tier); } break;
                case ("Sprite_FX_Path"): { this.HandleSpriteFXPath(name, value, tier); } break;
                case ("Shield_Damage"): { HandleStatsFromFile(name, EWeaponStat.Shield_Damage_Percent, v, tier); } break;
                case ("Stamina_Mod"): { HandleStatsFromFile(name, EWeaponStat.Stamina_Mod, v, tier); } break;
                case ("Value"): { HandleStatsFromFile(name, EWeaponStat.Value, v, tier); } break;
                case ("WeaponEAbility"): { HandleWeaponAbilitiesFromFile(name, value, tier); } break;
                case ("EWeaponType"): { HandleWeaponTypeFromFile(name, value, tier); } break;
                case ("EWeaponUse"): { HandleWeaponUseFromFile(name, value, tier); } break;
            }
        }

        private void HandleEffects(XElement el, string name, EEquipmentTier tier)
        {
            var stats = WeaponParamTable.Instance;
            var key = name + "_" + tier.ToString();
            foreach(var att in el.Attributes())
            {
                var type = EEffect.None;
                if (EnumUtil<EEffect>.TryGetEnumValue(att.Value, ref type))
                {
                    var effect = EffectBuilder.Instance.BuildEffect(el, type);
                    if (effect != null)
                    {
                        stats.Table[key].Effects.Add(effect);
                    }
                }
            }
        }

        private void HandleStatsFromFile(string name, EWeaponStat x, double v, EEquipmentTier tier)
        {
            var stats = WeaponParamTable.Instance;
            var key = name + "_" + tier.ToString();

            switch (x)
            {
                case (EWeaponStat.Accuracy_Mod): { stats.Table[key].ArmorIgnore = v; } break;
                case (EWeaponStat.Armor_Ignore): { stats.Table[key].ArmorIgnore = v; } break;
                case (EWeaponStat.Armor_Pierce): { stats.Table[key].ArmorPierce = v; } break;
                case (EWeaponStat.AP_Mod): { stats.Table[key].APMod = v; } break;
                case (EWeaponStat.Block_Ignore): { stats.Table[key].BlockIgnore = v; } break;
                case (EWeaponStat.Damage): { stats.Table[key].Damage = (int)v; } break;
                case (EWeaponStat.Dodge_Mod): { stats.Table[key].Dodge_Mod = v; } break;
                case (EWeaponStat.Max_Durability): { stats.Table[key].MaxDurability = (int)v; } break;
                case (EWeaponStat.Initiative_Mod): { stats.Table[key].InitiativeMod = v; } break;
                case (EWeaponStat.Melee_Block_Chance): { stats.Table[key].MeleeBlockChance = v; } break;
                case (EWeaponStat.Parry_Mod): { stats.Table[key].ParryMod = v; } break;
                case (EWeaponStat.Range_Mod): { stats.Table[key].RangeMod = (int)v; } break;
                case (EWeaponStat.Ranged_Block_Chance): { stats.Table[key].RangeBlockChance = v; } break;
                case (EWeaponStat.Shield_Damage_Percent): { stats.Table[key].ShieldDamagePercent = v; } break;
                case (EWeaponStat.Stamina_Mod): { stats.Table[key].StaminaMod = v; } break;
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

        private void HandleCustomBullet(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();
            if (value.ToLowerInvariant().Equals("true"))
            {
                var stats = WeaponParamTable.Instance;
                stats.Table[key].CustomBullet = true;
            }
        }

        private void HandleCustomFatality(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var x = EFatality.None;
            var stats = WeaponParamTable.Instance;
            if (EnumUtil<EFatality>.TryGetEnumValue(value, ref x))
                stats.Table[key].CustomFatality = x;
        }

        private void HandleEmbed(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();
            if (value.ToLowerInvariant().Equals("true"))
            {
                var stats = WeaponParamTable.Instance;
                stats.Table[key].Embed = true;
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

        private void HandleSpriteFXPath(string name, string value, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();

            var x = EWeaponUse.None;
            var stats = WeaponParamTable.Instance;
            stats.Table[key].SpriteFXPath = value;
        }
    }
}
