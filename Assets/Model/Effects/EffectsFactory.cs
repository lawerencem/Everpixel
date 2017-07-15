using Generics;

namespace Model.Effects
{
    public class EffectsFactory : AbstractSingleton<EffectsFactory>
    {
        public EffectsFactory() { }

        public GenericEffect CreateNewObject(EffectsEnum effect)
        {
            switch(effect)
            {
                case (EffectsEnum.Horror): { return new Horror(); }
                default: { return null; }
            }
        }
    }
}