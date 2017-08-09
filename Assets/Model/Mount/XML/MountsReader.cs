using System.Xml.Linq;
using Template.Utility;
using Template.XML;

namespace Assets.Model.Mount.XML
{
    public class MountReader : XMLReader
    {
        private static MountReader _instance;
        public static MountReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MountReader();
                return _instance;
            }
        }

        public MountReader()
        {
            this._path = "Assets/Model/Mount/XML/Mounts.xml";
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            foreach (var el in doc.Root.Elements())
            {
                if (el.Name == "Mount")
                {
                    var mParams = new MountParams();
                    var type = EMount.None;

                    foreach(var att in el.Attributes())
                    {
                        if (EnumUtil<EMount>.TryGetEnumValue(att.Value, ref type))
                            MountsTable.Instance.Table.Add(type, mParams);
                    }
                }
            }
        }
    }
}