using Assets.Model.Effect.Ability;
using Assets.Model.Effect.Fortitude;
using Assets.Model.Effect.Other;
using Assets.Model.Effect.Will;
using Assets.Model.Effect.Zone.Assets.Model.Effect.Zone;
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
                case (EEffect.Push): { return new EffectPush(); }
                case (EEffect.Horror): { return new Horror(); }
                case (EEffect.HoT): { return new EffectHoT(); }
                case (EEffect.Slime): { return new EffectSlime(); }
                case (EEffect.Stun): { return new EffectStun(); }
                case (EEffect.Summon_On_Hit): { return new EffectSummonOnHit(); }
                case (EEffect.Zone_Slime): { return new EffectSlimeZone(); }
                default: { return null; }
            }
        }
    }
}