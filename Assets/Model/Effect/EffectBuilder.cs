using Assets.Template.Other;
using Assets.Template.Util;
using System.Xml.Linq;

namespace Assets.Model.Effect
{
    public class EffectBuilder : ASingleton<EffectBuilder>
    {
        public MEffect BuildEffect(XElement el)
        {
            var type = EEffect.None;
            if (EnumUtil<EEffect>.TryGetEnumValue(el.Value, ref type))
            {
                var data = new MEffectData();
                foreach (var ele in el.Elements())
                {
                    this.HandleIndex(data, ele.Name.ToString(), ele.Value);
                }
                var effect = new MEffect(type);
                effect.SetData(data);
                return effect;
            }
            return null;
        }

        private void HandleIndex(MEffectData data, string key, string value)
        {
            int temp = 0;
        }
    }
}
