﻿using Assets.Model.Ability.Enum;
using Assets.Model.Effects.Will;
using System.Xml.Linq;
using Template.Utility;
using Template.XML;

namespace Assets.Model.Effect
{
    public class EffectReader : XMLReader
    {
        private EffectTable table = EffectTable.Instance;

        private static EffectReader _instance;
        public static EffectReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EffectReader();
                return _instance;
            }
        }

        public EffectReader()
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
                    var resist = EResistType.None;
                    if (EnumUtil<EResistType>.TryGetEnumValue(att.Value, ref resist))
                    {
                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
                            {
                                var effect = EEffect.None;
                                if (EnumUtil<EEffect>.TryGetEnumValue(attr.Value, ref effect))
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

        private void HandleIndex(EResistType resist, EEffect effect, XElement ele, string mod, string value)
        {

        }

        private void HandleType(EEffect e)
        {
            switch(e)
            {
                case (EEffect.Horror): { EffectTable.Instance.Table[e] = new Horror(); } break;
            }
        }
    }
}