using Assets.Model.Ability.Enum;
using Assets.Template.Other;
using Assets.Template.Util;
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

        public MEffect BuildEffect(MEffectData data, EEffect type)
        {
            var effect = EffectFactory.Instance.CreateNewObject(type);
            effect.SetData(data);
            return effect;
        }
        
        private void HandleIndex(MEffectData data, string key, string value)
        {
            switch(key)
            {
                case ("AbilityCondition"): { this.HandleAbilityCondition(data, key, value); } break;
                case ("CastCondition"): { this.HandleCastCondition(data, key, value); } break;
                case ("Duration"): { data.Duration = int.Parse(value); } break;
                case ("SpritesIndexes"): { this.HandleSpriteIndexes(data, value); } break;
                case ("SpritesMax"): { data.SpritesMax = int.Parse(value); } break;
                case ("SpritesMin"): { data.SpritesMin = int.Parse(value); } break;
                case ("SpritesPath"): { data.SpritesPath = value; } break;
                case ("ParticlePath"): { data.ParticlePath = value; } break;
                case ("SummonKey"): { data.SummonKey = value; } break;
                case ("WeaponCondition"): { data.WeaponCondition = value; } break;
                case ("X"): { data.X = double.Parse(value); } break;
                case ("Y"): { data.Y = double.Parse(value); } break;
                case ("Z"): { data.Z = double.Parse(value); } break;
            }
        }

        private void HandleAbilityCondition(MEffectData data, string key, string value)
        {
            var type = EAbility.None;
            if (EnumUtil<EAbility>.TryGetEnumValue(value, ref type))
            {
                data.AbilityCondition = type;
            }
        }

        private void HandleCastCondition(MEffectData data, string key, string value)
        {
            var type = ECastType.None;
            if (EnumUtil<ECastType>.TryGetEnumValue(value, ref type))
            {
                data.CastCondition = type;
            }
        }

        private void HandleSpriteIndexes(MEffectData data, string value)
        {
            var csv = value.Split(',');
            for(int i = 0; i < csv.Length; i++)
            {
                int result = 0;
                if (int.TryParse(csv[i], out result))
                    data.SpriteIndexes.Add(result);
            }
        }
    }
}
