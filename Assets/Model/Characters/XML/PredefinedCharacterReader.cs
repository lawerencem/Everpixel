using Assets.Model;
using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Classes;
using Model.Mounts;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Model.Characters.XML
{
    public class PredefinedCharacterReader : GenericXMLReader
    {
        private PredefinedCharacterTable table = PredefinedCharacterTable.Instance;

        private static PredefinedCharacterReader _instance;
        public static PredefinedCharacterReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PredefinedCharacterReader();
                return _instance;
            }
        }

        public PredefinedCharacterReader()
        {
            this._path = "Assets/Model/Characters/XML/PredefinedCharacters.xml";
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            ClassEnum baseClass = ClassEnum.None;
            CharacterTypeEnum type = CharacterTypeEnum.None;
            CultureEnum culture = CultureEnum.None;
            RaceEnum race = RaceEnum.None;

            foreach (var el in doc.Root.Elements())
            {
                foreach(var att in el.Attributes())
                {
                    if (EnumUtil<CultureEnum>.TryGetEnumValue(att.Value, ref culture))
                    {
                        foreach(var ele in el.Elements())
                        {
                            foreach(var attr in ele.Attributes())
                            {
                                string key = "";

                                if (!table.Table.ContainsKey(attr.Value.ToString()))
                                {
                                    key = attr.Value.ToString();

                                    table.Table.Add(key, new PredefinedCharacterParams());
                                    table.Table[key].Name = key;
                                    table.Table[key].Culture = culture;
                                }
                                foreach (var elem in ele.Elements())
                                {
                                    switch (elem.Name.ToString())
                                    {
                                        case (PredefinedReaderParams.ACTIVE_ABILITY): { HandleActiveAbility(key, elem.Value.ToString()); } break;
                                        case (PredefinedReaderParams.CLASS): { HandleClassType(key, elem.Value.ToString(), ref baseClass); } break;
                                        case (PredefinedReaderParams.MOUNT): { HandleMount(key, elem.Value); } break;
                                        case (PredefinedReaderParams.PERKS): { HandlePerks(elem, key); } break;
                                        case (PredefinedReaderParams.POTENTIAL_ARMORS): { HandleEquipment(elem, key); } break;
                                        case (PredefinedReaderParams.POTENTIAL_WEAPONS): { HandleEquipment(elem, key); } break;
                                        case (PredefinedReaderParams.RACE): { HandleRace(key, elem.Value.ToString(), ref race); } break;
                                        case (PredefinedReaderParams.SPELLS): { HandleSpells(elem, key); } break;
                                        case (PredefinedReaderParams.STATS): { HandleStats(elem, key); } break;
                                        case (PredefinedReaderParams.TYPE): { HandleCharacterType(key, elem.Value.ToString(), ref type); } break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleActiveAbility(string rootKey, string value)
        {
            var ab = AbilitiesEnum.None;
            if (EnumUtil<AbilitiesEnum>.TryGetEnumValue(value, ref ab))
                table.Table[rootKey].ActiveAbilities.Add(ab);
        }

        private void HandleClassType(string rootKey, string value, ref ClassEnum type)
        {
            var classAndLevel = value.Split(',');
            int level = int.Parse(classAndLevel[1]);

            if (EnumUtil<ClassEnum>.TryGetEnumValue(classAndLevel[0], ref type))
                table.Table[rootKey].Classes.Add(type, level);
        }

        private void HandleCharacterType(string rootKey, string value, ref CharacterTypeEnum type)
        {
            if (EnumUtil<CharacterTypeEnum>.TryGetEnumValue(value, ref type))
                table.Table[rootKey].Type = type;
        }
        
        private void HandleEquipment(XElement el, string rootKey)
        {
            foreach (var att in el.Elements())
            {
                foreach (var ele in att.Elements())
                {
                    foreach(var subAtt in att.Attributes())
                    {
                        var csv = ele.Value.ToString().Split(',');
                        var values = new List<string>();
                        foreach (var v in csv) { values.Add(v); }

                        switch(att.Name.ToString())
                        {
                            case (PredefinedReaderParams.ARMORS): { HandleEquipmentHelper(table.Table[rootKey].Armors, subAtt.Value, values); } break;
                            case (PredefinedReaderParams.HELMS): { HandleEquipmentHelper(table.Table[rootKey].Helms, subAtt.Value, values); } break;
                            case (PredefinedReaderParams.L_WEAPONS): { HandleEquipmentHelper(table.Table[rootKey].LWeapons, subAtt.Value, values); } break;
                            case (PredefinedReaderParams.R_WEAPONS): { HandleEquipmentHelper(table.Table[rootKey].RWeapons, subAtt.Value, values); } break;
                        }
                    }
                }
            }
        }

        private void HandleEquipmentHelper(Dictionary<string, List<List<string>>> eqTable, string key, List<string> values)
        {
            if (!eqTable.ContainsKey(key))
                eqTable.Add(key, new List<List<string>>());

            eqTable[key].Add(values);
        }

        private void HandleMount(string rootKey, string value)
        {
            var mount = MountEnum.None;
            if (EnumUtil<MountEnum>.TryGetEnumValue(value, ref mount))
                table.Table[rootKey].Mount = mount;
        }

        private void HandlePerks(XElement el, string rootkey)
        {
            foreach (var ele in el.Elements())
                PerkParser.ParsePerk(rootkey, ele.Value);
        }

        private void HandleRace(string rootKey, string value, ref RaceEnum race)
        {
            if (EnumUtil<RaceEnum>.TryGetEnumValue(value, ref race))
                table.Table[rootKey].Race = race;
        }

        private void HandleSpells(XElement el, string rootKey)
        {
            foreach (var ele in el.Elements())
                SpellParser.ParseSpell(ele, table.Table[rootKey]);
        }

        private void HandleStats(XElement el, string rootKey)
        {
            foreach(var ele in el.Elements())
                PrimaryStatsParser.ParseStats(ele, table.Table[rootKey].Stats);
        } 
    }
}