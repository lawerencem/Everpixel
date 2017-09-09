using Assets.Template.Other;
using System.Xml.Linq;

namespace Assets.Model.Effect
{
    public class EffectBuilder : ASingleton<EffectBuilder>
    {
        public MEffect BuildEffect(XElement el, EEffect type)
        {
            var data = new MEffectData();
            foreach (var ele in el.Elements())
            {
                this.HandleIndex(data, ele.Name.ToString(), ele.Value);
            }
            var effect = EffectFactory.Instance.CreateNewObject(type);
            effect.SetData(data);
            return effect;
        }
        
        private void HandleIndex(MEffectData data, string key, string value)
        {
            switch(key)
            {
                case ("Duration"): { data.Duration = int.Parse(value); } break;
                case ("X"): { data.X = double.Parse(value); } break;
                case ("Y"): { data.Y = double.Parse(value); } break;
            }
        }
    }
}
