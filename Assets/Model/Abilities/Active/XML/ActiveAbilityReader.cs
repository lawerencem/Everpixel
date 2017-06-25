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
                    var type = ActiveAbilitiesEnum.None;
                    if (EnumUtil<ActiveAbilitiesEnum>.TryGetEnumValue(att.Value, ref type))
                    {
                        HandleType(type);
                        foreach (var ele in el.Elements())
                            HandleIndex(type, ele, ele.Name.ToString(), ele.Value);
                    }
                }
        }

        private void HandleIndex(ActiveAbilitiesEnum type, XElement ele, string mod, string value)
        {
            int v = 1;
            int.TryParse(value, out v);
            switch (mod)
            {
                case ("AbilityCastTypeEnum"): { HandleCastType(type, value); } break;
                case ("AccMod"): { ActiveAbilityTable.Instance.Table[type].AccMod = v; } break;
                case ("AoE"): { ActiveAbilityTable.Instance.Table[type].AoE = v; } break;
                case ("APCost"): { ActiveAbilityTable.Instance.Table[type].APCost = v; } break;
                case ("ArmorIgnoreMod"): { ActiveAbilityTable.Instance.Table[type].ArmorIgnoreMod = v; } break;
                case ("ArmorPierceMod"): { ActiveAbilityTable.Instance.Table[type].ArmorPierceMod = v; } break;
                case ("BlockIgnoreMod"): { ActiveAbilityTable.Instance.Table[type].BlockIgnoreMod = v; } break;
                case ("CastTime"): { ActiveAbilityTable.Instance.Table[type].CastTime = v; } break;
                case ("Description"): { ActiveAbilityTable.Instance.Table[type].Description = value; } break;
                case ("DmgPerPower"): { ActiveAbilityTable.Instance.Table[type].DmgPerPower = double.Parse(value); } break;
                case ("Duration"): { ActiveAbilityTable.Instance.Table[type].Duration = double.Parse(value); } break;
                case ("DodgeReduceMod"): { ActiveAbilityTable.Instance.Table[type].DodgeMod = v; } break;
                case ("FlatDamage"): { ActiveAbilityTable.Instance.Table[type].FlatDamage = v; } break;
                case ("Injury"): { this.HandleInjury(type, value); } break;
                case ("MeleeBlockChanceMod"): { ActiveAbilityTable.Instance.Table[type].MeleeBlockChanceMod = v; } break;
                case ("ParryModMod"): { ActiveAbilityTable.Instance.Table[type].ParryModMod = v; } break;
                case ("Range"): { ActiveAbilityTable.Instance.Table[type].Range = v; } break;
                case ("RangeBlockMod"): { ActiveAbilityTable.Instance.Table[type].RangeBlockMod = v; } break;
                case ("RechargeTime"): { ActiveAbilityTable.Instance.Table[type].RechargeTime = v; } break;
                case ("ShapeshiftSprites"): { this.HandleShapeshiftSprites(ele, type); } break;
                case ("ShieldDamageMod"): { ActiveAbilityTable.Instance.Table[type].ShieldDamageMod = v; } break;
                case ("SpellLevel"): { ActiveAbilityTable.Instance.Table[type].SpellLevel = v; } break;
                case ("StaminaCost"): { ActiveAbilityTable.Instance.Table[type].StaminaCost = v; } break;
            }
        }

        private void HandleCastType(ActiveAbilitiesEnum key, string value)
        {
            var type = AbilityCastTypeEnum.None;
            if (EnumUtil<AbilityCastTypeEnum>.TryGetEnumValue(value, ref type))
                ActiveAbilityTable.Instance.Table[key].CastType = type;
        }

        private void HandleInjury(ActiveAbilitiesEnum type, string s)
        {
            var injury = InjuryEnum.None;
            if (EnumUtil<InjuryEnum>.TryGetEnumValue(s, ref injury))
            {
                ActiveAbilityTable.Instance.Table[type].Injuries.Add(injury);
            }
        }

        private void HandleType(ActiveAbilitiesEnum type)
        {
            switch (type)
            {
                case (ActiveAbilitiesEnum.Eldritch_Chomp): { ActiveAbilityTable.Instance.Table[type] = new EldritchChomp(); } break;
                case (ActiveAbilitiesEnum.Hadoken): { ActiveAbilityTable.Instance.Table[type] = new Hadoken(); } break;
                case (ActiveAbilitiesEnum.Haste_Song): { ActiveAbilityTable.Instance.Table[type] = new HasteSong(); } break;
                case (ActiveAbilitiesEnum.Orc_Metal): { ActiveAbilityTable.Instance.Table[type] = new OrcMetal(); } break;
                case (ActiveAbilitiesEnum.Summon_Shoggoth): { ActiveAbilityTable.Instance.Table[type] = new SummonShoggoth(); } break;
                case (ActiveAbilitiesEnum.Were_Ween): { ActiveAbilityTable.Instance.Table[type] = new Wereween(); } break;
            }
        }

        private void HandleShapeshiftSprites(XElement element, ActiveAbilitiesEnum type)
        {
            var v = ActiveAbilityTable.Instance.Table[type] as GenericShapeshiftAbility;
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