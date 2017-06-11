using System;
using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using System.Collections.Generic;
using Model.Events;
using Model.Injuries;

namespace Models.Equipment.XML
{
    public class ActiveAbilityReader : GenericXMLReader
    {
        private static ActiveAbilityReader _instance;

        public ActiveAbilityReader() { this._path = "Assets/Model/Abilities/Active/XML/ActiveAbilities.xml"; }

        public static ActiveAbilityReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActiveAbilityReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            foreach (var el in doc.Root.Elements())
                foreach (var att in el.Attributes())
                {
                    var type = ActiveAbilitiesEnum.None;
                    if (EnumUtil<ActiveAbilitiesEnum>.TryGetEnumValue(att.Value, ref type))
                    {
                        HandleType(type);
                        foreach (var ele in el.Elements())
                            HandleIndex(type, ele.Name.ToString(), ele.Value);
                    }
                }
        }

        private void HandleIndex(ActiveAbilitiesEnum type, string mod, string value)
        {
            int v = 1;
            if (int.TryParse(value, out v))
            {
                switch (mod)
                {
                    case ("AccMod"): { ActiveAbilityTable.Instance.Table[type].AccMod = v; } break;
                    case ("AbilityCastTypeEnum"): { HandleCastType(type, value); } break;
                    case ("APCost"): { ActiveAbilityTable.Instance.Table[type].APCost = v; } break;
                    case ("ArmorIgnoreMod"): { ActiveAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                    case ("ArmorPierceMod"): { ActiveAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                    case ("BlockIgnoreMod"): { ActiveAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                    case ("DamageMod"): { ActiveAbilityTable.Instance.Table[type].DamageMod = v; } break;
                    case ("Description"): { ActiveAbilityTable.Instance.Table[type].Description = value; } break;
                    case ("DodgeReduceMod"): { ActiveAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                    case ("MeleeBlockChanceMod"): { ActiveAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                    case ("ParryModMod"): { ActiveAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                    case ("Range"): { ActiveAbilityTable.Instance.Table[type].Range = v; } break;
                    case ("RangeBlockMod"): { ActiveAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                    case ("ShieldDamageMod"): { ActiveAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
                    case ("StaminaCost"): { ActiveAbilityTable.Instance.Table[type].StaminaCost = v; } break;
                }
            }
            else
            {
                switch (mod)
                {
                    case ("Injury"): { this.HandleInjury(type, value); } break;
                }
            }
        }

        private void HandleType(ActiveAbilitiesEnum type)
        {
            switch (type)
            {
                case (ActiveAbilitiesEnum.Hadoken): { ActiveAbilityTable.Instance.Table[type] = new Hadoken(); } break;
            }
        }

        private void HandleInjury(ActiveAbilitiesEnum type, string s)
        {
            var injury = InjuryEnum.None;
            if (EnumUtil<InjuryEnum>.TryGetEnumValue(s, ref injury))
            {
                ActiveAbilityTable.Instance.Table[type].Injuries.Add(injury);
            }
        }

        private void HandleCastType(ActiveAbilitiesEnum key, string value)
        {
            var type = AbilityCastTypeEnum.None;
            if (EnumUtil<AbilityCastTypeEnum>.TryGetEnumValue(value, ref type))
                ActiveAbilityTable.Instance.Table[key].CastType = type;
        }
    }
}