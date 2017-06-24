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
                                    table.Table[key].Type = CharacterTypeEnum.Critter;
                                }
                                foreach (var elem in ele.Elements())
                                {
                                    switch (elem.Name.ToString())
                                    {
                                        case (PredefinedReaderParams.ACTIVE_ABILITY): { HandleActiveAbility(key, elem.Value.ToString()); } break;
                                        case (PredefinedReaderParams.ATTACK_SPRITE_INDEX): { HandleAttackSpriteIndex(key, elem.Value.ToString()); } break;
                                        case (PredefinedReaderParams.DEFAULT_WPN_ABILITES): { HandleDefaultWpnAbility(key, elem.Value.ToString()); } break;
                                        case (PredefinedReaderParams.FLINCH_SPRITE_INDEX): { HandleFlinchSpriteIndex(key, elem.Value.ToString()); } break;
                                        case (PredefinedReaderParams.PERKS): { HandlePerks(elem, key); } break;
                                        case (PredefinedReaderParams.STATS): { HandleStats(elem, key); } break;
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
            var ab = ActiveAbilitiesEnum.None;
            if (EnumUtil<ActiveAbilitiesEnum>.TryGetEnumValue(value, ref ab))
                table.Table[rootKey].ActiveAbilities.Add(ab);
        }

        private void HandleAttackSpriteIndex(string rootkey, string value)
        {
            int v = 0;
            if (int.TryParse(value, out v))
                CritterAttackSpriteTable.Instance.Table.Add(rootkey, v);
        }

        private void HandleDefaultWpnAbility(string rootKey, string value)
        {
            var wpnAbility = WeaponAbilitiesEnum.None;
            if (EnumUtil<WeaponAbilitiesEnum>.TryGetEnumValue(value, ref wpnAbility))
                table.Table[rootKey].DefaultWpnAbilities.Add(wpnAbility);
        }

        private void HandleFlinchSpriteIndex(string rootkey, string value)
        {
            int v = 0;
            if (int.TryParse(value, out v))
                CritterFlinchSpriteTable.Instance.Table.Add(rootkey, v);
        }

        private void HandlePerks(XElement el, string rootkey)
        {
            foreach (var ele in el.Elements())
                PerkParser.ParsePerk(rootkey, ele.Value);
        }

        private void HandleStats(XElement el, string rootKey)
        {
            foreach (var ele in el.Elements())
                PrimaryStatsParser.ParseStat(ele, table.Table[rootKey].Stats);
        }
    }
}