using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using Model.Injuries;
using Model.Abilities.Shapeshift;
using Model.Abilities.Music;
using Model.Abilities.Magic;

namespace Models.Equipment.XML
{
    public class AbiltiyReader : GenericXMLReader
    {
        private static AbiltiyReader _instance;

        private string _weaponPath = "Assets/Model/Abilities/XML/WeaponAbilities.xml";
        public AbiltiyReader() { this._path = "Assets/Model/Abilities/XML/Abilities.xml"; }

        public static AbiltiyReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AbiltiyReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            var abilities = XDocument.Load(this._path);
            var weaponAbilities = XDocument.Load(this._weaponPath);
            this.ReadFromFileHelper(abilities);
            this.ReadFromFileHelper(weaponAbilities);
        }

        private void ReadFromFileHelper(XDocument doc)
        {
            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    var magicType = MagicTypeEnum.Physical;
                    if (EnumUtil<MagicTypeEnum>.TryGetEnumValue(att.Value, ref magicType))
                    {
                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
                            {
                                var type = AbilitiesEnum.None;
                                if (EnumUtil<AbilitiesEnum>.TryGetEnumValue(attr.Value, ref type))
                                {
                                    this.HandleType(type);
                                    foreach (var elem in ele.Elements())
                                    {
                                        this.HandleIndex(type, elem, elem.Name.ToString(), elem.Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleIndex(AbilitiesEnum type, XElement ele, string mod, string value)
        {
            double v = 1;
            double.TryParse(value, out v);
            switch (mod)
            {
                case ("AccMod"): { GenericAbilityTable.Instance.Table[type].AccMod = v; } break;
                case ("AoE"): { GenericAbilityTable.Instance.Table[type].AoE = v; } break;
                case ("APCost"): { GenericAbilityTable.Instance.Table[type].APCost = (int)v; } break;
                case ("ArmorIgnoreMod"): { GenericAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                case ("ArmorPierceMod"): { GenericAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                case ("BlockIgnoreMod"): { GenericAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                case ("CastTime"): { GenericAbilityTable.Instance.Table[type].CastTime = v; } break;
                case ("CastTypeEnum"): { this.HandleCastType(type, value); } break;
                case ("Description"): { GenericAbilityTable.Instance.Table[type].Description = value; } break;
                case ("DmgPerPower"): { GenericAbilityTable.Instance.Table[type].DmgPerPower = double.Parse(value); } break;
                case ("Duration"): { GenericAbilityTable.Instance.Table[type].Duration = double.Parse(value); } break;
                case ("DodgeMod"): { GenericAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                case ("EffectDur"): { GenericAbilityTable.Instance.Table[type].EffectDur = v; } break;
                case ("EffectValue"): { GenericAbilityTable.Instance.Table[type].EffectValue = v; } break;
                case ("FlatDamage"): { GenericAbilityTable.Instance.Table[type].FlatDamage = v; } break;
                case ("IconSprite"): { GenericAbilityTable.Instance.Table[type].Sprite = (int)v; } break;
                case ("Injury"): { this.HandleInjury(type, value); } break;
                case ("MeleeBlockChanceMod"): { GenericAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                case ("ParryModMod"): { GenericAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                case ("Range"): { GenericAbilityTable.Instance.Table[type].Range = (int)v; } break;
                case ("RangeBlockMod"): { GenericAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                case ("RechargeTime"): { GenericAbilityTable.Instance.Table[type].RechargeTime = v; } break;
                case ("ResistTypeEnum"): { this.HandleResistType(type, value); } break;
                case ("ShapeshiftSprites"): { this.HandleShapeshiftSprites(ele, type); } break;
                case ("ShieldDamageMod"): { GenericAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
                case ("SpellLevel"): { GenericAbilityTable.Instance.Table[type].SpellLevel = (int)v; } break;
                case ("StaminaCost"): { GenericAbilityTable.Instance.Table[type].StaminaCost = (int)v; } break;
            }
        }

        private void HandleCastType(AbilitiesEnum key, string value)
        {
            var type = CastTypeEnum.None;
            if (EnumUtil<CastTypeEnum>.TryGetEnumValue(value, ref type))
                GenericAbilityTable.Instance.Table[key].CastType = type;
        }

        private void HandleInjury(AbilitiesEnum type, string s)
        {
            var injury = InjuryEnum.None;
            if (EnumUtil<InjuryEnum>.TryGetEnumValue(s, ref injury))
            {
                GenericAbilityTable.Instance.Table[type].Injuries.Add(injury);
            }
        }

        private void HandleResistType(AbilitiesEnum type, string s)
        {
            var resist = ResistTypeEnum.None;
            if (EnumUtil<ResistTypeEnum>.TryGetEnumValue(s, ref resist))
                GenericAbilityTable.Instance.Table[type].Resist = resist;
        }

        private void HandleShapeshiftSprites(XElement element, AbilitiesEnum type)
        {
            var v = GenericAbilityTable.Instance.Table[type] as GenericShapeshiftAbility;
            foreach(var ele in element.Elements())
            {
                switch(ele.Name.ToString())
                {
                    case ("CharAttackHead"): { v.Info.CharAttackHead = int.Parse(ele.Value); } break;
                    case ("CharAttackTorso"): { v.Info.CharAttackTorso = int.Parse(ele.Value); } break;
                    case ("CharHeadDead"): { v.Info.CharHeadDead = int.Parse(ele.Value); } break;
                    case ("CharHeadFlinch"): { v.Info.CharHeadFlinch = int.Parse(ele.Value); } break;
                    case ("CharHead"): { v.Info.CharHead = int.Parse(ele.Value); } break;
                    case ("CharTorso"): { v.Info.CharTorso = int.Parse(ele.Value); } break;
                }
            }
        }

        private void HandleType(AbilitiesEnum type)
        {
            switch (type)
            {
                case (AbilitiesEnum.Aim): { GenericAbilityTable.Instance.Table[type] = new Aim(); } break;
                case (AbilitiesEnum.Bite): { GenericAbilityTable.Instance.Table[type] = new Bite(); } break;
                case (AbilitiesEnum.Break_Armor): { GenericAbilityTable.Instance.Table[type] = new BreakArmor(); } break;
                case (AbilitiesEnum.Break_Shield): { GenericAbilityTable.Instance.Table[type] = new BreakShield(); } break;
                case (AbilitiesEnum.Chop): { GenericAbilityTable.Instance.Table[type] = new Chop(); } break;
                case (AbilitiesEnum.Crush): { GenericAbilityTable.Instance.Table[type] = new Crush(); } break;
                case (AbilitiesEnum.Double_Strike): { GenericAbilityTable.Instance.Table[type] = new DoubleStrike(); } break;
                case (AbilitiesEnum.Eldritch_Chomp): { GenericAbilityTable.Instance.Table[type] = new EldritchChomp(); } break;
                case (AbilitiesEnum.Fire): { GenericAbilityTable.Instance.Table[type] = new Fire(); } break;
                case (AbilitiesEnum.Gash): { GenericAbilityTable.Instance.Table[type] = new Gash(); } break;
                case (AbilitiesEnum.Great_Strike): { GenericAbilityTable.Instance.Table[type] = new GreatStrike(); } break;
                case (AbilitiesEnum.Hadoken): { GenericAbilityTable.Instance.Table[type] = new Hadoken(); } break;
                case (AbilitiesEnum.Haste_Song): { GenericAbilityTable.Instance.Table[type] = new HasteSong(); } break;
                case (AbilitiesEnum.Kamehameha): { GenericAbilityTable.Instance.Table[type] = new Kamehameha(); } break;
                case (AbilitiesEnum.Maim): { GenericAbilityTable.Instance.Table[type] = new Maim(); } break;
                case (AbilitiesEnum.Orc_Metal): { GenericAbilityTable.Instance.Table[type] = new OrcMetal(); } break;
                case (AbilitiesEnum.Pierce): { GenericAbilityTable.Instance.Table[type] = new Pierce(); } break;
                case (AbilitiesEnum.Pull): { GenericAbilityTable.Instance.Table[type] = new Pull(); } break;
                case (AbilitiesEnum.Riposte): { GenericAbilityTable.Instance.Table[type] = new Riposte(); } break;
                case (AbilitiesEnum.Scatter): { GenericAbilityTable.Instance.Table[type] = new Scatter(); } break;
                case (AbilitiesEnum.Soothing_Mist): { GenericAbilityTable.Instance.Table[type] = new SoothingMist(); } break;
                case (AbilitiesEnum.Shield_Wall): { GenericAbilityTable.Instance.Table[type] = new ShieldWall(); } break;
                case (AbilitiesEnum.Shove): { GenericAbilityTable.Instance.Table[type] = new Shove(); } break;
                case (AbilitiesEnum.Slash): { GenericAbilityTable.Instance.Table[type] = new Slash(); } break;
                case (AbilitiesEnum.Spear_Wall): { GenericAbilityTable.Instance.Table[type] = new SpearWall(); } break;
                case (AbilitiesEnum.Stab): { GenericAbilityTable.Instance.Table[type] = new Stab(); } break;
                case (AbilitiesEnum.Stun): { GenericAbilityTable.Instance.Table[type] = new Stun(); } break;
                case (AbilitiesEnum.Summon_Shoggoth): { GenericAbilityTable.Instance.Table[type] = new SummonShoggoth(); } break;
                case (AbilitiesEnum.Triple_Strike): { GenericAbilityTable.Instance.Table[type] = new TripleStrike(); } break;
                case (AbilitiesEnum.Were_Ween): { GenericAbilityTable.Instance.Table[type] = new Wereween(); } break;
                case (AbilitiesEnum.Wide_Slash): { GenericAbilityTable.Instance.Table[type] = new WideSlash(); } break;
                case (AbilitiesEnum.Wrap): { GenericAbilityTable.Instance.Table[type] = new Wrap(); } break;
            }
        }
    }
}
