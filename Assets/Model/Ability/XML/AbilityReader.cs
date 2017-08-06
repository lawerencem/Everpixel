using Generics;
using System.Xml.Linq;
using Generics.Utilities;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic;
using Assets.Model.Ability.Magic.Astral;
using Assets.Model.Ability.Magic.Fighting;
using Assets.Model.Ability.Magic.Water;
using Assets.Model.Ability.Music;
using Assets.Model.Ability.Shapeshift;
using Assets.Model.Injuries;
using Assets.Model.Weapon.Abilities;

namespace Assets.Models.Equipment.XML
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
                    var magicType = EMagicType.Physical;
                    if (EnumUtil<EMagicType>.TryGetEnumValue(att.Value, ref magicType))
                    {
                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
                            {
                                var type = EAbility.None;
                                if (EnumUtil<EAbility>.TryGetEnumValue(attr.Value, ref type))
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

        private void HandleIndex(EAbility type, XElement ele, string mod, string value)
        {
            double v = 1;
            double.TryParse(value, out v);
            switch (mod)
            {
                case ("AccMod"): { AbilityTable.Instance.Table[type].Params.AccMod = v; } break;
                case ("AoE"): { AbilityTable.Instance.Table[type].Params.AoE = v; } break;
                case ("APCost"): { AbilityTable.Instance.Table[type].Params.APCost = (int)v; } break;
                case ("ArmorIgnoreMod"): { AbilityTable.Instance.Table[type].Params.ArmorIgnoreMod = v; } break;
                case ("ArmorPierceMod"): { AbilityTable.Instance.Table[type].Params.ArmorPierceMod = v; } break;
                case ("BlockIgnoreMod"): { AbilityTable.Instance.Table[type].Params.BlockIgnoreMod = v; } break;
                case ("CastTime"): { AbilityTable.Instance.Table[type].Params.CastTime = v; } break;
                case ("CastTypeEnum"): { this.HandleCastType(type, value); } break;
                case ("Description"): { AbilityTable.Instance.Table[type].Params.Description = value; } break;
                case ("DmgPerPower"): { AbilityTable.Instance.Table[type].Params.DmgPerPower = double.Parse(value); } break;
                case ("Duration"): { AbilityTable.Instance.Table[type].Params.Duration = double.Parse(value); } break;
                case ("DodgeMod"): { AbilityTable.Instance.Table[type].Params.DodgeMod = v; } break;
                case ("EffectDur"): { AbilityTable.Instance.Table[type].Params.EffectDur = v; } break;
                case ("EffectValue"): { AbilityTable.Instance.Table[type].Params.EffectValue = v; } break;
                case ("FlatDamage"): { AbilityTable.Instance.Table[type].Params.FlatDamage = v; } break;
                case ("IconSprite"): { AbilityTable.Instance.Table[type].Params.Sprite = (int)v; } break;
                case ("Injury"): { this.HandlAPerk(type, value); } break;
                case ("MeleeBlockChanceMod"): { AbilityTable.Instance.Table[type].Params.MeleeBlockChanceMod = v; } break;
                case ("ParryModMod"): { AbilityTable.Instance.Table[type].Params.ParryModMod = v; } break;
                case ("Range"): { AbilityTable.Instance.Table[type].Params.Range = (int)v; } break;
                case ("RangeBlockMod"): { AbilityTable.Instance.Table[type].Params.RangeBlockMod = v; } break;
                case ("RechargeTime"): { AbilityTable.Instance.Table[type].Params.RechargeTime = v; } break;
                case ("EResistType"): { this.HandleResistType(type, value); } break;
                case ("ShapeshiftSprites"): { this.HandleShapeshiftSprites(ele, type); } break;
                case ("ShieldDamageMod"): { AbilityTable.Instance.Table[type].Params.ShieldDamageMod = v; } break;
                case ("SpellLevel"): { AbilityTable.Instance.Table[type].Params.SpellLevel = (int)v; } break;
                case ("StaminaCost"): { AbilityTable.Instance.Table[type].Params.StaminaCost = (int)v; } break;
            }
        }

        private void HandleCastType(EAbility key, string value)
        {
            var type = ECastType.None;
            if (EnumUtil<ECastType>.TryGetEnumValue(value, ref type))
                AbilityTable.Instance.Table[key].Params.CastType = type;
        }

        private void HandlAPerk(EAbility type, string s)
        {
            var injury = EInjury.None;
            if (EnumUtil<EInjury>.TryGetEnumValue(s, ref injury))
            {
                AbilityTable.Instance.Table[type].Params.Injuries.Add(injury);
            }
        }

        private void HandleResistType(EAbility type, string s)
        {
            var resist = EResistType.None;
            if (EnumUtil<EResistType>.TryGetEnumValue(s, ref resist))
                AbilityTable.Instance.Table[type].Params.Resist = resist;
        }

        private void HandleShapeshiftSprites(XElement element, EAbility type)
        {
            var v = AbilityTable.Instance.Table[type] as Shapeshift;
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

        private void HandleType(EAbility type)
        {
            switch (type)
            {
                case (EAbility.Aim): { AbilityTable.Instance.Table[type] = new Aim(); } break;
                case (EAbility.Bite): { AbilityTable.Instance.Table[type] = new Bite(); } break;
                case (EAbility.Break_Armor): { AbilityTable.Instance.Table[type] = new BreakArmor(); } break;
                case (EAbility.Break_Shield): { AbilityTable.Instance.Table[type] = new BreakShield(); } break;
                case (EAbility.Chop): { AbilityTable.Instance.Table[type] = new Chop(); } break;
                case (EAbility.Crush): { AbilityTable.Instance.Table[type] = new Crush(); } break;
                case (EAbility.Double_Strike): { AbilityTable.Instance.Table[type] = new DoubleStrike(); } break;
                case (EAbility.Eldritch_Chomp): { AbilityTable.Instance.Table[type] = new EldritchChomp(); } break;
                case (EAbility.Fire): { AbilityTable.Instance.Table[type] = new Fire(); } break;
                case (EAbility.Gash): { AbilityTable.Instance.Table[type] = new Gash(); } break;
                case (EAbility.Great_Strike): { AbilityTable.Instance.Table[type] = new GreatStrike(); } break;
                case (EAbility.Hadoken): { AbilityTable.Instance.Table[type] = new Hadoken(); } break;
                case (EAbility.Haste_Song): { AbilityTable.Instance.Table[type] = new HasteSong(); } break;
                case (EAbility.Kamehameha): { AbilityTable.Instance.Table[type] = new Kamehameha(); } break;
                case (EAbility.Maim): { AbilityTable.Instance.Table[type] = new Maim(); } break;
                //case (EnumAbility.Orc_Metal): { AbilityTable.Instance.Table[type] = new OrcMetal(); } break;
                case (EAbility.Pierce): { AbilityTable.Instance.Table[type] = new Pierce(); } break;
                case (EAbility.Pull): { AbilityTable.Instance.Table[type] = new Pull(); } break;
                case (EAbility.Riposte): { AbilityTable.Instance.Table[type] = new Riposte(); } break;
                case (EAbility.Scatter): { AbilityTable.Instance.Table[type] = new Scatter(); } break;
                case (EAbility.Soothing_Mist): { AbilityTable.Instance.Table[type] = new SoothingMist(); } break;
                case (EAbility.Shield_Wall): { AbilityTable.Instance.Table[type] = new ShieldWall(); } break;
                case (EAbility.Shove): { AbilityTable.Instance.Table[type] = new Shove(); } break;
                case (EAbility.Slash): { AbilityTable.Instance.Table[type] = new Slash(); } break;
                case (EAbility.Spear_Wall): { AbilityTable.Instance.Table[type] = new SpearWall(); } break;
                case (EAbility.Stab): { AbilityTable.Instance.Table[type] = new Stab(); } break;
                case (EAbility.Stun): { AbilityTable.Instance.Table[type] = new Stun(); } break;
                case (EAbility.Summon_Shoggoth): { AbilityTable.Instance.Table[type] = new SummonShoggoth(); } break;
                case (EAbility.Triple_Strike): { AbilityTable.Instance.Table[type] = new TripleStrike(); } break;
                case (EAbility.Were_Ween): { AbilityTable.Instance.Table[type] = new Wereween(); } break;
                case (EAbility.Wide_Slash): { AbilityTable.Instance.Table[type] = new WideSlash(); } break;
                case (EAbility.Wrap): { AbilityTable.Instance.Table[type] = new Wrap(); } break;
            }
        }
    }
}
