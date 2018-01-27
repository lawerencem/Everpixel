using Assets.Data.Map.Landmark.Table;
using Assets.Model.Map.Landmark;
using Assets.Model.Map.Tile;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Map.Landmark.XML
{
    public class TileReader : XMLReader
    {
        private TileTable table = TileTable.Instance;

        private static TileReader _instance;
        public static TileReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TileReader();
                return _instance;
            }
        }

        public TileReader() : base()
        {
            this._paths.Add("Assets/Data/Map/Tile/Tiles.xml");
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = ETile.None;

                foreach (var el in doc.Root.Elements())
                    foreach (var att in el.Attributes())
                        foreach (var ele in el.Elements())
                            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
            }
        }

        private void HandleIndex(string name, string param, string value, ref ETile type)
        {
            if (EnumUtil<ETile>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new TileParams(type));

                switch (param)
                {
                    case ("Cost"): { this.table.Table[type].Cost = int.Parse(value); } break;
                    case ("Sprites"): { this.HandleSprites(type, value); } break;
                }
            }
        }

        private void HandleSprites(ETile type, string value)
        {
            var csv = value.Split(',');
            foreach(var v in csv)
            {
                int result = 0;
                if (int.TryParse(v, out result))
                    this.table.Table[type].Sprites.Add(result);
            }
        }
    }
}
