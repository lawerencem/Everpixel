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
    public class PredefinedCritterReader : GenericXMLReader
    {
        private PredefinedCharacterTable table = PredefinedCharacterTable.Instance;

        private static PredefinedCritterReader _instance;
        public static PredefinedCritterReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PredefinedCritterReader();
                return _instance;
            }
        }

        public PredefinedCritterReader()
        {
            this._path = "Assets/Model/Characters/XML/PredefinedCritters.xml";
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            ClassEnum baseClass = ClassEnum.None;
            CultureEnum culture = CultureEnum.None;
            CharacterTypeEnum type = CharacterTypeEnum.None;

            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    if (EnumUtil<CultureEnum>.TryGetEnumValue(att.Value, ref culture))
                    {
                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
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
                                        case (PredefinedReaderParams.CLASS): { HandleClassType(key, elem.Value.ToString(), ref baseClass); } break;
                                        case (PredefinedReaderParams.DEFAULT_WPN_ABILITES): { HandleDefaultWpnAbility(key, elem.Value.ToString()); } break;
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

        private void HandleDefaultWpnAbility(string rootKey, string value)
        {
            var wpnAbility = WeaponAbilitiesEnum.None;
            if (EnumUtil<WeaponAbilitiesEnum>.TryGetEnumValue(value, ref wpnAbility))
                table.Table[rootKey].DefaultWpnAbilities.Add(wpnAbility);
        }

        private void HandleRace(string rootKey, string value, ref RaceEnum race)
        {
            if (EnumUtil<RaceEnum>.TryGetEnumValue(value, ref race))
                table.Table[rootKey].Race = race;
        }

        private void HandleStats(XElement el, string rootKey)
        {
            foreach (var ele in el.Elements())
                PrimaryStatsParser.ParseStat(ele, table.Table[rootKey].Stats);
        }
    }
}