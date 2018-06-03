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
                case (EEffect.Barrier): { return new EffectBarrier(); }
                case (EEffect.Megabite): { return new EffectMegabite(); }
                case (EEffect.Horror): { return new Horror(); }
                case (EEffect.HoT): { return new EffectHoT(); }
                case (EEffect.Push): { return new EffectPush(); }
                case (EEffect.Slime): { return new EffectSlime(); }
                case (EEffect.Slime_Zone): { return new EffectZoneSlime(); }
                case (EEffect.Spear_Wall_Zone): { return new EffectZoneSpearWall(); }
                case (EEffect.Stun): { return new EffectStun(); }
                case (EEffect.Summon_On_Hit): { return new EffectSummonOnHit(); }
                case (EEffect.Suppression_Zone): { return new EffectSuppressionZone(); }
                default: { return null; }
            }
        }
    }
}