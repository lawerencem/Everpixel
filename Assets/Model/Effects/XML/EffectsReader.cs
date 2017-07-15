using Generics;
using Generics.Utilities;
using Model.Abilities;
using System;
using System.Xml.Linq;

namespace Model.Effects
{
    public class EffectsReader : GenericXMLReader
    {
        private EffectsTable table = EffectsTable.Instance;

        private static EffectsReader _instance;
        public static EffectsReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EffectsReader();
                return _instance;
            }
        }

        public EffectsReader()
        {
            this._path = "Assets/Model/Effects/XML/Effects.xml";
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);

            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    var resist = ResistTypeEnum.None;
                    if (EnumUtil<ResistTypeEnum>.TryGetEnumValue(att.Value, ref resist))
                    {
                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
                            {
                                var effect = EffectsEnum.None;
                                if (EnumUtil<EffectsEnum>.TryGetEnumValue(attr.Value, ref effect))
                                {
                                    this.HandleType(effect);
                                    foreach (var elem in ele.Elements())
                                    {
                                        this.HandleIndex(resist, effect, elem, elem.Name.ToString(), elem.Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleIndex(ResistTypeEnum resist, EffectsEnum effect, XElement ele, string mod, string value)
        {

        }

        private void HandleType(EffectsEnum e)
        {
            switch(e)
            {
                case (EffectsEnum.Horror): { EffectsTable.Instance.Table[e] = new Horror(); } break;
            }
        }
    }
}
