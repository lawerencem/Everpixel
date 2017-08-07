﻿using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Table;
using Assets.Model.Characters.Params;
using Assets.Model.Class.Enum;
using Assets.Model.Culture;
using Assets.Model.Mount;
using System.Collections.Generic;
using System.Xml.Linq;
using Template.Utility;
using Template.XML;

namespace Assets.Model.Character.XML
{
    public class PredefinedCharReader : XMLReader
    {
        private PredefinedCharTable table = PredefinedCharTable.Instance;

        private static PredefinedCharReader _instance;
        public static PredefinedCharReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PredefinedCharReader();
                return _instance;
            }
        }

        public PredefinedCharReader()
        {
            this._path = "Assets/Model/Characters/XML/PredefinedCharacters.xml";
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            EClass baseClass = EClass.None;
            ECharType type = ECharType.None;
            ECulture culture = ECulture.None;
            ERace race = ERace.None;

            foreach (var el in doc.Root.Elements())
            {
                foreach(var att in el.Attributes())
                {
                    if (EnumUtil<ECulture>.TryGetEnumValue(att.Value, ref culture))
                    {
                        foreach(var ele in el.Elements())
                        {
                            foreach(var attr in ele.Attributes())
                            {
                                string key = "";

                                if (!table.Table.ContainsKey(attr.Value.ToString()))
                                {
                                    key = attr.Value.ToString();

                                    table.Table.Add(key, new PreCharParams());
                                    table.Table[key].Name = key;
                                    table.Table[key].Culture = culture;
                                }
                                foreach (var elem in ele.Elements())
                                {
                                    switch (elem.Name.ToString())
                                    {
                                        case (PredefinedReaderParams.ABILITY): { HandleActiveAbility(key, elem.Value.ToString()); } break;
                                        case (PredefinedReaderParams.CLASS): { HandleClassType(key, elem.Value.ToString(), ref baseClass); } break;
                                        case (PredefinedReaderParams.MOUNT): { HandleMount(key, elem.Value); } break;
                                        case (PredefinedReaderParams.PERKS): { HandlePerks(elem, key); } break;
                                        case (PredefinedReaderParams.POTENTIAL_ARMORS): { HandleEquipment(elem, key); } break;
                                        case (PredefinedReaderParams.POTENTIAL_WEAPONS): { HandleEquipment(elem, key); } break;
                                        case (PredefinedReaderParams.RACE): { HandleRace(key, elem.Value.ToString(), ref race); } break;
                                        //case (PredefinedReaderParams.SPELLS): { HandleSpells(elem, key); } break;
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
            var ab = EAbility.None;
            if (EnumUtil<EAbility>.TryGetEnumValue(value, ref ab))
                table.Table[rootKey].ActiveAbilities.Add(ab);
        }

        private void HandleClassType(string rootKey, string value, ref EClass type)
        {
            var classAndLevel = value.Split(',');
            int level = int.Parse(classAndLevel[1]);

            if (EnumUtil<EClass>.TryGetEnumValue(classAndLevel[0], ref type))
                table.Table[rootKey].Classes.Add(type, level);
        }

        private void HandleCharacterType(string rootKey, string value, ref ECharType type)
        {
            if (EnumUtil<ECharType>.TryGetEnumValue(value, ref type))
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
            var mount = EMount.None;
            if (EnumUtil<EMount>.TryGetEnumValue(value, ref mount))
                table.Table[rootKey].Mount = mount;
        }

        private void HandlePerks(XElement el, string rootkey)
        {
            foreach (var ele in el.Elements())
                PerkParser.ParsePerk(rootkey, ele.Value);
        }

        private void HandleRace(string rootKey, string value, ref ERace race)
        {
            if (EnumUtil<ERace>.TryGetEnumValue(value, ref race))
                table.Table[rootKey].Race = race;
        }

        //private void HandleSpells(XElement el, string rootKey)
        //{
        //    foreach (var ele in el.Elements())
        //        SpellParser.ParseSpell(ele, table.Table[rootKey]);
        //}

        private void HandleStats(XElement el, string rootKey)
        {
            foreach(var ele in el.Elements())
                PrimaryStatsParser.ParseStats(ele, table.Table[rootKey].Stats);
        } 
    }
}