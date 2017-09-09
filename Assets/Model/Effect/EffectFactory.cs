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
                case (EEffect.Horror): { return new Horror(); }
                case (EEffect.Ween_Bullet): { return new WeenBullet(); }
                default: { return null; }
            }
        }
    }
}