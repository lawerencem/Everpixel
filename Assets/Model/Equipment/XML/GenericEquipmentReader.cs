using System;
using Generics;
using System.Xml.Linq;
using Model.Abilities;
using Generics.Utilities;
using System.Collections.Generic;
using Model.Events;

namespace Model.Equipment.XML
{
    public class GenericEquipmentReader : GenericXMLReader
    {
        public GenericEquipmentReader() { }

        //public override void ReadFromFile()
        //{
            //var doc = XDocument.Load(this._path);
            //var tier = EquipmentTierEnum.None;

            //foreach (var el in doc.Root.Elements())
            //    foreach (var att in el.Attributes())
            //        foreach (var ele in el.Elements())
            //            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref tier);
        //}

        protected virtual void SetName(string name) { }

        //protected virtual void HandleIndex(string name, string param, string value, ref EquipmentTierEnum tier) { }
        protected virtual void HandleIndex(string name, string skill, string param, string value, ref EquipmentTierEnum tier) { }
        protected virtual void HandleSpritesFromFile(string name, string s, EquipmentTierEnum tier)
        {
            var key = name + "_" + tier.ToString();
            var indexTable = EquipmentSpritesTable.Instance;
            var indexes = s.Split(',');
            indexTable.Table.Add(key, new List<int>());
            for (int i = 0; i < indexes.Length; i++)
                indexTable.Table[key].Add(int.Parse(indexes[i]));
        }

        protected virtual void HandleTierFromFile(string name, string value, ref EquipmentTierEnum tier) { }
        protected virtual void HandleTypeFromFile(string name, string value, EquipmentTierEnum tier ) {  }
    }
}
