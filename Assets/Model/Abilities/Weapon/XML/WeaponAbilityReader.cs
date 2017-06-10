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
                        HandleType(type);
                        foreach (var ele in el.Elements())
                            HandleIndex(type, ele.Name.ToString(), ele.Value);
                    }
                }
        }

        private void HandleIndex(WeaponAbilitiesEnum type, string mod, string value)
        {
            int v = 1;
            if (int.TryParse(value, out v))
            {
                switch (mod)
                {
                    case ("AccMod"): { WeaponAbilityTable.Instance.Table[type].AccMod = v; } break;
                    case ("APCost"): { WeaponAbilityTable.Instance.Table[type].APCost = v; } break;
                    case ("ArmorIgnoreMod"): { WeaponAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                    case ("ArmorPierceMod"): { WeaponAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                    case ("BlockIgnoreMod"): { WeaponAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                    case ("DamageMod"): { WeaponAbilityTable.Instance.Table[type].DamageMod = v; } break;
                    case ("Description"): { WeaponAbilityTable.Instance.Table[type].Description = value; } break;
                    case ("DodgeReduceMod"): { WeaponAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                    case ("MeleeBlockChanceMod"): { WeaponAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                    case ("ParryModMod"): { WeaponAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                    case ("Range"): { WeaponAbilityTable.Instance.Table[type].Range = v; } break;
                    case ("RangeBlockMod"): { WeaponAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                    case ("ShieldDamageMod"): { WeaponAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
                    case ("StaminaCost"): { WeaponAbilityTable.Instance.Table[type].StaminaCost = v; } break;
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

        private void HandleType(WeaponAbilitiesEnum type)
        {
            switch(type)
            {
                case (WeaponAbilitiesEnum.Aim): { WeaponAbilityTable.Instance.Table[type] = new Aim(); } break;
                case (WeaponAbilitiesEnum.Bite): { WeaponAbilityTable.Instance.Table[type] = new Bite(); } break;
                case (WeaponAbilitiesEnum.Break_Armor): { WeaponAbilityTable.Instance.Table[type] = new BreakArmor(); } break;
                case (WeaponAbilitiesEnum.Break_Shield): { WeaponAbilityTable.Instance.Table[type] = new BreakShield(); } break;
                case (WeaponAbilitiesEnum.Chop): { WeaponAbilityTable.Instance.Table[type] = new Chop(); } break;
                case (WeaponAbilitiesEnum.Crush): { WeaponAbilityTable.Instance.Table[type] = new Crush(); } break;
                case (WeaponAbilitiesEnum.Double_Strike): { WeaponAbilityTable.Instance.Table[type] = new DoubleStrike(); } break;
                case (WeaponAbilitiesEnum.Fire): { WeaponAbilityTable.Instance.Table[type] = new Fire(); } break;
                case (WeaponAbilitiesEnum.Gash): { WeaponAbilityTable.Instance.Table[type] = new Gash(); } break;
                case (WeaponAbilitiesEnum.Great_Strike): { WeaponAbilityTable.Instance.Table[type] = new GreatStrike(); } break;
                case (WeaponAbilitiesEnum.Maim): { WeaponAbilityTable.Instance.Table[type] = new Maim(); } break;
                case (WeaponAbilitiesEnum.Pierce): { WeaponAbilityTable.Instance.Table[type] = new Pierce(); } break;
                case (WeaponAbilitiesEnum.Pull): { WeaponAbilityTable.Instance.Table[type] = new Pull(); } break;
                case (WeaponAbilitiesEnum.Riposte): { WeaponAbilityTable.Instance.Table[type] = new Riposte(); } break;
                case (WeaponAbilitiesEnum.Scatter): { WeaponAbilityTable.Instance.Table[type] = new Scatter(); } break;
                case (WeaponAbilitiesEnum.Shield_Wall): { WeaponAbilityTable.Instance.Table[type] = new ShieldWall(); } break;
                case (WeaponAbilitiesEnum.Shove): { WeaponAbilityTable.Instance.Table[type] = new Shove(); } break;
                case (WeaponAbilitiesEnum.Slash): { WeaponAbilityTable.Instance.Table[type] = new Slash(); } break;
                case (WeaponAbilitiesEnum.Spear_Wall): { WeaponAbilityTable.Instance.Table[type] = new SpearWall(); } break;
                case (WeaponAbilitiesEnum.Stab): { WeaponAbilityTable.Instance.Table[type] = new Stab(); } break;
                case (WeaponAbilitiesEnum.Stun): { WeaponAbilityTable.Instance.Table[type] = new Stun(); } break;
                case (WeaponAbilitiesEnum.Triple_Strike): { WeaponAbilityTable.Instance.Table[type] = new TripleStrike(); } break;
                case (WeaponAbilitiesEnum.Wide_Slash): { WeaponAbilityTable.Instance.Table[type] = new WideSlash(); } break;
                case (WeaponAbilitiesEnum.Wrap): { WeaponAbilityTable.Instance.Table[type] = new Wrap(); } break;
            }
        }

        private void HandleInjury(WeaponAbilitiesEnum type, string s)
        {
            var injury = InjuryEnum.None;
            if (EnumUtil<InjuryEnum>.TryGetEnumValue(s, ref injury))
            {
                WeaponAbilityTable.Instance.Table[type].Injuries.Add(injury);
            }
        }
    }
}