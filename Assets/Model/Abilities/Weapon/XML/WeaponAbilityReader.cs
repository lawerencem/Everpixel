using System;
using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using System.Collections.Generic;
using Model.Events;

namespace Models.Equipment.XML
{
    public class WeaponAbilityReader : GenericXMLReader
    {
        private static WeaponAbilityReader _instance;

        public WeaponAbilityReader() { this._path = "Assets/Model/Abilities/Weapon/XML/WeaponAbilities.xml"; }

        public static WeaponAbilityReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WeaponAbilityReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            foreach (var el in doc.Root.Elements())
                foreach (var att in el.Attributes())
                {
                    var type = WeaponAbilitiesEnum.None;
                    if (EnumUtil<WeaponAbilitiesEnum>.TryGetEnumValue(att.Value, ref type))
                    {
                        if (!WeaponAbilityTable.Instance.Table.ContainsKey(type))
                            WeaponAbilityTable.Instance.Table.Add(type, new WeaponAbility(type));

                        foreach (var ele in el.Elements())
                            HandleIndex(type, ele.Name.ToString(), ele.Value);
                    }
                }
        }

        private void HandleIndex(WeaponAbilitiesEnum type, string mod, string value)
        {
            int v = 1;
            if (int.TryParse(value, out v))

            switch(mod)
            {
                case ("AccMod"): { WeaponAbilityTable.Instance.Table[type].AccMod = v; } break;
                case ("APMod"): { WeaponAbilityTable.Instance.Table[type].APMod = v; } break;
                case ("ArmorIgnoreMod"): { WeaponAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                case ("ArmorPierceMod"): { WeaponAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                case ("BlockIgnoreMod"): { WeaponAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                case ("DamageMod"): { WeaponAbilityTable.Instance.Table[type].DamageMod = v; } break;
                case ("Description"): { WeaponAbilityTable.Instance.Table[type].Description = value; } break;
                case ("DodgeReduceMod"): { WeaponAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                case ("FatigueCostMod"): { WeaponAbilityTable.Instance.Table[type].FatigueCostMod = v; } break;
                case ("MeleeBlockChanceMod"): { WeaponAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                case ("ParryModMod"): { WeaponAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                case ("RangeBlockMod"): { WeaponAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                case ("ShieldDamageMod"): { WeaponAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
            }
        }
    }
}