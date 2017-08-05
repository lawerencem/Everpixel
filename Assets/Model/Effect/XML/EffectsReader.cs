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
                                var effect = EnumEffect.None;
                                if (EnumUtil<EnumEffect>.TryGetEnumValue(attr.Value, ref effect))
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

        private void HandleIndex(ResistTypeEnum resist, EnumEffect effect, XElement ele, string mod, string value)
        {

        }

        private void HandleType(EnumEffect e)
        {
            switch(e)
            {
                case (EnumEffect.Horror): { EffectsTable.Instance.Table[e] = new Horror(); } break;
            }
        }
    }
}
