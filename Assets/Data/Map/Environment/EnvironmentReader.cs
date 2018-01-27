using Assets.Data.Map.Deco.Table;
using Assets.Model.Map.Combat.Tile;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Map.Environment
{
    public class EnvironmentReader : XMLReader
    {
        private EnvironmentTable table = EnvironmentTable.Instance;

        private static EnvironmentReader _instance;
        public static EnvironmentReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EnvironmentReader();
                return _instance;
            }
        }

        public EnvironmentReader() : base()
        {
            this._paths.Add("Assets/Data/Map/Environment/Environments.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = EEnvironment.None;

                foreach (var el in doc.Root.Elements())
                    foreach (var att in el.Attributes())
                        foreach (var ele in el.Elements())
                            HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
            }
        }

        private void HandleIndex(string name, string param, string value, ref EEnvironment type)
        {
            if (EnumUtil<EEnvironment>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new EnvironmentParam(type));

                switch(param)
                {
                    case ("Sprites"): { this.HandleSprites(value, ref type); } break;
                }
            }
        }

        private void HandleSprites(string value, ref EEnvironment type)
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
