using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using Model.Injuries;
using Model.Abilities.Shapeshift;
using Model.Abilities.Music;

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
                    var type = AbilitiesEnum.None;
                    if (EnumUtil<AbilitiesEnum>.TryGetEnumValue(att.Value, ref type))
                    {
                        HandleType(type);
                        foreach (var ele in el.Elements())
                            HandleIndex(type, ele, ele.Name.ToString(), ele.Value);
                    }
                }
        }

        private void HandleIndex(AbilitiesEnum type, XElement ele, string mod, string value)
        {
            double v = 1;
            double.TryParse(value, out v);
            switch (mod)
            {
                case ("AbilityCastTypeEnum"): { HandleCastType(type, value); } break;
                case ("AccMod"): { GenericAbilityTable.Instance.Table[type].AccMod = v; } break;
                case ("AoE"): { GenericAbilityTable.Instance.Table[type].AoE = v; } break;
                case ("APCost"): { GenericAbilityTable.Instance.Table[type].APCost = (int)v; } break;
                case ("ArmorIgnoreMod"): { GenericAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                case ("ArmorPierceMod"): { GenericAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                case ("BlockIgnoreMod"): { GenericAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                case ("CastTime"): { GenericAbilityTable.Instance.Table[type].CastTime = v; } break;
                case ("Description"): { GenericAbilityTable.Instance.Table[type].Description = value; } break;
                case ("DmgPerPower"): { GenericAbilityTable.Instance.Table[type].DmgPerPower = double.Parse(value); } break;
                case ("Duration"): { GenericAbilityTable.Instance.Table[type].Duration = double.Parse(value); } break;
                case ("DodgeMod"): { GenericAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                case ("FlatDamage"): { GenericAbilityTable.Instance.Table[type].FlatDamage = v; } break;
                case ("Injury"): { this.HandleInjury(type, value); } break;
                case ("MeleeBlockChanceMod"): { GenericAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                case ("ParryModMod"): { GenericAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                case ("Range"): { GenericAbilityTable.Instance.Table[type].Range = (int)v; } break;
                case ("RangeBlockMod"): { GenericAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                case ("RechargeTime"): { GenericAbilityTable.Instance.Table[type].RechargeTime = v; } break;
                case ("ShapeshiftSprites"): { this.HandleShapeshiftSprites(ele, type); } break;
                case ("ShieldDamageMod"): { GenericAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
                case ("SpellLevel"): { GenericAbilityTable.Instance.Table[type].SpellLevel = (int)v; } break;
                case ("StaminaCost"): { GenericAbilityTable.Instance.Table[type].StaminaCost = (int)v; } break;
            }
        }

        private void HandleCastType(AbilitiesEnum key, string value)
        {
            var type = AbilityCastTypeEnum.None;
            if (EnumUtil<AbilityCastTypeEnum>.TryGetEnumValue(value, ref type))
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

        private void HandleType(AbilitiesEnum type)
        {
            switch (type)
            {
                case (AbilitiesEnum.Eldritch_Chomp): { GenericAbilityTable.Instance.Table[type] = new EldritchChomp(); } break;
                case (AbilitiesEnum.Hadoken): { GenericAbilityTable.Instance.Table[type] = new Hadoken(); } break;
                case (AbilitiesEnum.Haste_Song): { GenericAbilityTable.Instance.Table[type] = new HasteSong(); } break;
                case (AbilitiesEnum.Orc_Metal): { GenericAbilityTable.Instance.Table[type] = new OrcMetal(); } break;
                case (AbilitiesEnum.Summon_Shoggoth): { GenericAbilityTable.Instance.Table[type] = new SummonShoggoth(); } break;
                case (AbilitiesEnum.Were_Ween): { GenericAbilityTable.Instance.Table[type] = new Wereween(); } break;
            }
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
    }
}