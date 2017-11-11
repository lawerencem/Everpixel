using Assets.Data.Map.Deco.Table;
using Assets.Model.Map.Tile;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Map.Deco.XML
{
    public class TileDecoReader : XMLReader
    {
        private DecoTable table = DecoTable.Instance;

        private static TileDecoReader _instance;
        public static TileDecoReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TileDecoReader();
                return _instance;
            }
        }

        public TileDecoReader() : base()
        {
            this._paths.Add("Assets/Data/Map/Deco/XML/TileDeco.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = ETileDeco.None;

                foreach (var el in doc.Root.Elements())
                    foreach (var att in el.Attributes())
                        foreach (var ele in el.Elements())
                            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
            }
        }

        private void HandleIndex(string name, string param, string value, ref ETileDeco type)
        {
            if (EnumUtil<ETileDeco>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new TileDecoParam(type));

                switch(param)
                {
                    case ("Sprites"): { this.HandleSprites(value, ref type); } break;
                }
            }
        }

        private void HandleSprites(string value, ref ETileDeco type)
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
