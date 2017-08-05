using Generics;

namespace Model.Effects
{
    public class EffectsFactory : AbstractSingleton<EffectsFactory>
    {
        public EffectsFactory() { }

        public Effect CreateNewObject(EnumEffect effect)
        {
            switch(effect)
            {
                case (EnumEffect.Horror): { return new Horror(); }
                default: { return null; }
            }
        }
    }
}