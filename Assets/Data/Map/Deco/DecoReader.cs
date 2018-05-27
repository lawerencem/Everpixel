using Assets.Model.Map.Deco;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Map.Environment
{
    public class DecoReader : XMLReader
    {
        private DecoTable table = DecoTable.Instance;

        private static DecoReader _instance;
        public static DecoReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DecoReader();
                return _instance;
            }
        }

        public DecoReader() : base()
        {
            this._paths.Add("Assets/Data/Map/Deco/Deco.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = EDeco.None;

                foreach (var el in doc.Root.Elements())
                    foreach (var att in el.Attributes())
                        foreach (var ele in el.Elements())
                            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
            }
        }

        private void HandleIndex(string name, string param, string value, ref EDeco type)
        {
            if (EnumUtil<EDeco>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new decoParam(type));

                switch(param)
                {
                    case ("BulletObstructionChance"): { this.table.Table[type].BulletObstructionChance = double.Parse(value); } break;
                    case ("Sprites"): { this.HandleSprites(value, ref type); } break;
                }
            }
        }

        private void HandleSprites(string value, ref EDeco type)
        {
            var values = value.Split(',');
            foreach(var v in values)
            {
                int result = -1;
                if (int.TryParse(v, out result))
                    this.table.Table[type].Sprites.Add(result);
            }
        }
    }
}
