using Assets.Data.OTE.Table;
using Assets.Model.Ability.Enum;
using Assets.Model.Effect.OTE;
using Assets.Model.OTE;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.OTE.XML
{
    public class OTEReader : XMLReader
    {
        private static OTEReader _instance;
        private OTETable _table;

        public OTEReader() : base()
        {
            this._paths.Add("Assets/Data/OTE/XML/OTEs.xml");

            this._table = OTETable.Instance;
        }

        public static OTEReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OTEReader();
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
                    var type = EOTE.None;
                    if (EnumUtil<EOTE>.TryGetEnumValue(att.Value, ref type))
                    {
                        if (!this._table.Table.ContainsKey(type))
                            this._table.Table.Add(type, new OTEParams());

                        foreach (var ele in el.Elements())
                            this.HandleIndex(type, ele, ele.Name.ToString());
                    }
                }
            }
        }

        private void HandleIndex(EOTE type, XElement ele, string mod)
        {
            switch (mod)
            {
                case ("Dur"): { this.HandleDur(type, ele, mod); } break;
                case ("EResist"): { this.HandleResist(type, ele, mod); } break;
                case ("X"): { this.HandleX(type, ele, mod); } break;
                case ("Y"): { this.HandleY(type, ele, mod); } break;
            }
        }

        private void HandleDur(EOTE type, XElement ele, string mod)
        {
            int result = 0;
            if (int.TryParse(ele.Value, out result))
                this._table.Table[type].Dur = result;
        }

        private void HandleResist(EOTE type, XElement ele, string mod)
        {
            EResistType resist = EResistType.None;
            if (EnumUtil<EResistType>.TryGetEnumValue(ele.Value, ref resist))
                this._table.Table[type].Resist = resist;
        }

        private void HandleX(EOTE type, XElement ele, string mod)
        {
            double result = 0;
            if (double.TryParse(ele.Value, out result))
                this._table.Table[type].X = result;
        }

        private void HandleY(EOTE type, XElement ele, string mod)
        {
            double result = 0;
            if (double.TryParse(ele.Value, out result))
                this._table.Table[type].Y = result;
        }
    }
}
