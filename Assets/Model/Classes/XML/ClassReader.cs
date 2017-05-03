using Generics;
using Generics.Utilities;
using Model.Characters;
using System.Xml.Linq;

namespace Model.Classes
{
    public class ClassReader : GenericXMLReader
    {
        private const int STAT_INDEX = 0;
        private const int VALUE_INDEX = 1;

        private static ClassReader _instance;
        public ClassReader()
        {
            this._path = "Assets/Model/Classes/XML/Classes.xml";
            this._table = ClassParamTable.Instance;
        }

        private ClassParamTable _table;

        public static ClassReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);
            var type = ClassEnum.None;

            foreach (var el in doc.Root.Elements())
                foreach (var att in el.Attributes())
                    foreach (var ele in el.Elements())
                        HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
        }

        private void HandleIndex(string name, string param, string value, ref ClassEnum type)
        {
            if (EnumUtil<ClassEnum>.TryGetEnumValue(name, ref type))
            {
                if (!this._table.Table.ContainsKey(type))
                    this._table.Table.Add(type, new ClassParams());

                switch (param)
                {
                    case ("PrimaryStatsEnum"): { HandlePrimaryStatsFromFile(param, value, ref type); } break;
                    case ("SecondaryStatsEnum"): { HandleSecondaryStatsFromFile(param, value, ref type); } break;
                }
            }
           
            
        }

        private void HandlePrimaryStatsFromFile(string param, string value, ref ClassEnum type)
        {
            PrimaryStatsEnum stat = PrimaryStatsEnum.None;
            var v = value.Split(',');

            if (EnumUtil<PrimaryStatsEnum>.TryGetEnumValue(v[STAT_INDEX], ref stat) && v.Length > 1)
                this._table.Table[type].PrimaryStats[stat] = int.Parse(v[VALUE_INDEX]);
        }

        private void HandleSecondaryStatsFromFile(string param, string value, ref ClassEnum type)
        {
            SecondaryStatsEnum stat = SecondaryStatsEnum.None;
            var v = value.Split(',');

            if (EnumUtil<SecondaryStatsEnum>.TryGetEnumValue(v[STAT_INDEX], ref stat) && v.Length > 1)
                this._table.Table[type].SecondaryStats[stat] = int.Parse(v[VALUE_INDEX]);
        }
    }
}
