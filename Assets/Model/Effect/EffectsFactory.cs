using Assets.Model.Effects.Will;
using Generics;

namespace Assets.Model.Effect
{
    public class EffectFactory : AbstractSingleton<EffectFactory>
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