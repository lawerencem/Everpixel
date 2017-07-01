using System;
using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using Model.Injuries;

namespace Models.Equipment.XML
{
    public class WeaponAbilityReader : GenericXMLReader
    {
        private static WeaponAbilityReader _instance;

        public WeaponAbilityReader() { this._path = "Assets/Model/Abilities/XML/WeaponAbilities.xml"; }

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
                    var type = AbilitiesEnum.None;
                    if (EnumUtil<AbilitiesEnum>.TryGetEnumValue(att.Value, ref type))
                    {
                        HandleType(type);
                        foreach (var ele in el.Elements())
                            HandleIndex(type, ele.Name.ToString(), ele.Value);
                    }
                }
        }

        private void HandleIndex(AbilitiesEnum type, string mod, string value)
        {
            double v = 1;
            if (double.TryParse(value, out v))
            {
                switch (mod)
                {
                    case ("AccMod"): { GenericAbilityTable.Instance.Table[type].AccMod = v; } break;
                    case ("APCost"): { GenericAbilityTable.Instance.Table[type].APCost = (int)v; } break;
                    case ("ArmorIgnoreMod"): { GenericAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                    case ("ArmorPierceMod"): { GenericAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                    case ("BlockIgnoreMod"): { GenericAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                    case ("DamageMod"): { GenericAbilityTable.Instance.Table[type].DamageMod = v; } break;
                    case ("Description"): { GenericAbilityTable.Instance.Table[type].Description = value; } break;
                    case ("DodgeMod"): { GenericAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                    case ("IconSprite"): { GenericAbilityTable.Instance.Table[type].Sprite = (int)v; } break;
                    case ("MeleeBlockChanceMod"): { GenericAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                    case ("ParryModMod"): { GenericAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                    case ("Range"): { GenericAbilityTable.Instance.Table[type].Range = (int)v; } break;
                    case ("RangeBlockMod"): { GenericAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                    case ("ShieldDamageMod"): { GenericAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
                    case ("StaminaCost"): { GenericAbilityTable.Instance.Table[type].StaminaCost = (int)v; } break;
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

        private void HandleType(AbilitiesEnum type)
        {
            switch(type)
            {
                case (AbilitiesEnum.Aim): { GenericAbilityTable.Instance.Table[type] = new Aim(); } break;
                case (AbilitiesEnum.Bite): { GenericAbilityTable.Instance.Table[type] = new Bite(); } break;
                case (AbilitiesEnum.Break_Armor): { GenericAbilityTable.Instance.Table[type] = new BreakArmor(); } break;
                case (AbilitiesEnum.Break_Shield): { GenericAbilityTable.Instance.Table[type] = new BreakShield(); } break;
                case (AbilitiesEnum.Chop): { GenericAbilityTable.Instance.Table[type] = new Chop(); } break;
                case (AbilitiesEnum.Crush): { GenericAbilityTable.Instance.Table[type] = new Crush(); } break;
                case (AbilitiesEnum.Double_Strike): { GenericAbilityTable.Instance.Table[type] = new DoubleStrike(); } break;
                case (AbilitiesEnum.Fire): { GenericAbilityTable.Instance.Table[type] = new Fire(); } break;
                case (AbilitiesEnum.Gash): { GenericAbilityTable.Instance.Table[type] = new Gash(); } break;
                case (AbilitiesEnum.Great_Strike): { GenericAbilityTable.Instance.Table[type] = new GreatStrike(); } break;
                case (AbilitiesEnum.Maim): { GenericAbilityTable.Instance.Table[type] = new Maim(); } break;
                case (AbilitiesEnum.Pierce): { GenericAbilityTable.Instance.Table[type] = new Pierce(); } break;
                case (AbilitiesEnum.Pull): { GenericAbilityTable.Instance.Table[type] = new Pull(); } break;
                case (AbilitiesEnum.Riposte): { GenericAbilityTable.Instance.Table[type] = new Riposte(); } break;
                case (AbilitiesEnum.Scatter): { GenericAbilityTable.Instance.Table[type] = new Scatter(); } break;
                case (AbilitiesEnum.Shield_Wall): { GenericAbilityTable.Instance.Table[type] = new ShieldWall(); } break;
                case (AbilitiesEnum.Shove): { GenericAbilityTable.Instance.Table[type] = new Shove(); } break;
                case (AbilitiesEnum.Slash): { GenericAbilityTable.Instance.Table[type] = new Slash(); } break;
                case (AbilitiesEnum.Spear_Wall): { GenericAbilityTable.Instance.Table[type] = new SpearWall(); } break;
                case (AbilitiesEnum.Stab): { GenericAbilityTable.Instance.Table[type] = new Stab(); } break;
                case (AbilitiesEnum.Stun): { GenericAbilityTable.Instance.Table[type] = new Stun(); } break;
                case (AbilitiesEnum.Triple_Strike): { GenericAbilityTable.Instance.Table[type] = new TripleStrike(); } break;
                case (AbilitiesEnum.Wide_Slash): { GenericAbilityTable.Instance.Table[type] = new WideSlash(); } break;
                case (AbilitiesEnum.Wrap): { GenericAbilityTable.Instance.Table[type] = new Wrap(); } break;
            }
        }

        private void HandleInjury(AbilitiesEnum type, string s)
        {
            var injury = InjuryEnum.None;
            if (EnumUtil<InjuryEnum>.TryGetEnumValue(s, ref injury))
            {
                GenericAbilityTable.Instance.Table[type].Injuries.Add(injury);
            }
        }
    }
}