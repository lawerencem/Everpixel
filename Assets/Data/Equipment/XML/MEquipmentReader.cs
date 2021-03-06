﻿using Assets.Data.Equipment.Table;
using Assets.Model.Equipment.Enum;
using Assets.Template.XML;
using System.Collections.Generic;

namespace Assets.Data.Equipment.XML
{
    public class MEquipmentReader : XMLReader
    {
        public MEquipmentReader() : base() { }

        protected virtual void SetName(string name) { }
        protected virtual void HandleIndex(string name, string skill, string param, string value, ref EEquipmentTier tier) { }
        protected virtual void HandleSpritesFromFile(string name, string s, EEquipmentTier tier)
        {
            var key = name + "_" + tier.ToString();
            var indexTable = EquipmentSpritesTable.Instance;
            var indexes = s.Split(',');
            indexTable.Table.Add(key, new List<int>());
            for (int i = 0; i < indexes.Length; i++)
                indexTable.Table[key].Add(int.Parse(indexes[i]));
        }

        protected virtual void HandleTierFromFile(string name, string value, ref EEquipmentTier tier) { }
        protected virtual void HandleTypeFromFile(string name, string value, EEquipmentTier tier ) {  }
    }
}
