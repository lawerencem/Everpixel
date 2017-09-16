using Assets.Model.Effect.Ability;
using Assets.Model.Effect.Other;
using Assets.Model.Effect.Will;
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
                case (EEffect.Megabite): { return new Megabite(); }
                case (EEffect.Horror): { return new Horror(); }
                case (EEffect.SummonOnHit): { return new SummonOnHit(); }
                default: { return null; }
            }
        }
    }
}