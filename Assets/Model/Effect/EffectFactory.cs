using Assets.Model.Effect.Other;
using Assets.Model.Effect.Will;
using Assets.Template.Other;

namespace Assets.Model.Effect
{
    public class EffectFactory : ASingleton<EffectFactory>
    {
        public EffectFactory() { }

        public MEffect CreateNewObject(EEffect effect, MEffectData data)
        {
            switch(effect)
            {
                case (EEffect.Horror): { return new Horror(data); }
                case (EEffect.Ween_Bullet): { return new WeenBullet(data); }
                default: { return null; }
            }
        }
    }
}