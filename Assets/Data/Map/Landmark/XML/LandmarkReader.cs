using Assets.Data.Map.Landmark.Table;
using Assets.Model.Map.Combat.Landmark;
using Assets.Model.Map.Combat.Tile;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Map.Landmark.XML
{
    public class LandmarkReader : XMLReader
    {
        private LandmarkTable table = LandmarkTable.Instance;

        private static LandmarkReader _instance;
        public static LandmarkReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LandmarkReader();
                return _instance;
            }
        }

        public LandmarkReader() : base()
        {
            this._paths.Add("Assets/Data/Map/Landmark/XML/Landmarks.xml");
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = ELandmark.None;

                foreach (var el in doc.Root.Elements())
                    foreach (var att in el.Attributes())
                        foreach (var ele in el.Elements())
                            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
            }
        }

        private void HandleIndex(string name, string param, string value, ref ELandmark type)
        {
            if (EnumUtil<ELandmark>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new LandmarkParams(type));

                switch (param)
                {
                    case ("Height"): { this.table.Table[type].Height = int.Parse(value); } break;
                    case ("RadiusMax"): { this.table.Table[type].RadiusMax = int.Parse(value); } break;
                    case ("RadiusMin"): { this.table.Table[type].RadiusMin = int.Parse(value); } break;
                    case ("LengthMax"): { this.table.Table[type].LengthMax = int.Parse(value); } break;
                    case ("LengthMin"): { this.table.Table[type].LengthMin = int.Parse(value); } break;
                }
            }
        }
    }
}
