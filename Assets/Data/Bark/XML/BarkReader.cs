using Assets.Data.Bark.Table;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.View.Bark
{
    public class BarkReader : XMLReader
    {
        private BarkTable table = BarkTable.Instance;

        private static BarkReader _instance;
        public static BarkReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BarkReader();
                return _instance;
            }
        }

        public BarkReader() : base()
        {
            this._paths.Add("Assets/Data/Bark/XML/Barks.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = EBark.None;

                foreach (var el in doc.Root.Elements())
                    foreach (var att in el.Attributes())
                        foreach (var ele in el.Elements())
                            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
            }
        }

        private void HandleIndex(string name, string param, string value, ref EBark type)
        {
            if (EnumUtil<EBark>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new List<string>());

                this.table.Table[type].Add(value);
            }
        }
    }
}
