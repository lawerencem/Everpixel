using Assets.Model.Effects.Will;
using Assets.Template.Other;

namespace Assets.Model.Effect
{
    public class EffectFactory : ASingleton<EffectFactory>
    {
        public EffectFactory() { }

        public MEffect CreateNewObject(EEffect effect)
        {
            switch(effect)
            {
                case (EEffect.Horror): { return new Horror(); }
                default: { return null; }
            }
        }
    }
}