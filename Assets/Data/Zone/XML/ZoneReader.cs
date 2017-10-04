using Assets.Data.Zone.Table;
using Assets.Model.Zone;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Zone.XML
{
    class ZoneReader : XMLReader
    {
        private static ZoneReader _instance;

        public ZoneReader() : base()
        {
            this._paths.Add("Assets/Data/Zone/XML/Zones.xml");
        }

        public static ZoneReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ZoneReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var xDoc = XDocument.Load(path);
                this.ReadFromFileHelper(xDoc);
            }
        }

        private void ReadFromFileHelper(XDocument doc)
        {
            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    var type = EZone.None;
                    if (EnumUtil<EZone>.TryGetEnumValue(att.Value, ref type))
                    {

                    }
                }
            }
        }

        private void HandleIndex(EZone type, XElement ele, string mod, string value)
        {
            var table = ZoneTable.Instance.Table;
            double v = 1;
            double.TryParse(value, out v);
            switch (mod)
            {
                case ("Sprites"): { this.HandleSprites(ele, type); } break;
            }
        }

        private void HandleSprites(XElement el, EZone type)
        {
            var table = ZoneTable.Instance.Table;
            var csv = el.Value.ToString().Split(',');
            foreach(var v in csv)
            {
                int result = 0;
                if (int.TryParse(v, out result))
                    table[type].Sprites.Add(result);
            }
        }
    }
}
