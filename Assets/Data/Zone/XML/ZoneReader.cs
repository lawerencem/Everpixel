using Assets.Data.Zone.Table;
using Assets.Model.Effect;
using Assets.Model.Zone;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Zone.XML
{
    public class ZoneReader : XMLReader
    {
        private static ZoneReader _instance;
        private ZoneTable ZoneTable;

        public ZoneReader() : base()
        {
            this._paths.Add("Assets/Data/Zone/XML/Zones.xml");
            this.ZoneTable = ZoneTable.Instance;
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
                        if (!this.ZoneTable.Table.ContainsKey(type))
                            this.ZoneTable.Table.Add(type, new ZoneParams());

                        foreach (var ele in el.Elements())
                            this.HandleIndex(type, ele, ele.Name.ToString());
                    }
                }
            }
        }

        private void HandleIndex(EZone type, XElement ele, string mod)
        {
            switch (mod)
            {
                case ("EEffect"): { this.HandleEEffect(ele, type); } break;
                case ("Sprites"): { this.HandleSprites(ele, type); } break;
            }
        }

        private void HandleEEffect(XElement el, EZone type)
        {
            var effectType = EEffect.None;
            if (EnumUtil<EEffect>.TryGetEnumValue(el.Value, ref effectType))
            {
                var effect = EffectBuilder.Instance.BuildEffect(el, effectType);
                this.ZoneTable.Table[type].Effects.Add(effect);
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
