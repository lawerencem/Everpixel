using Assets.Model.Combat.Hit;
using Assets.Model.Zone.Duration;

namespace Assets.Model.Effect.Zone
{
    namespace Assets.Model.Effect.Zone
    {
        public class EffectSuppressionZone : MEffect
        {
            public EffectSuppressionZone() : base(EEffect.Suppression_Zone) { }

            public override void TryProcessHit(MHit hit, bool prediction)
            {
                base.TryProcessHit(hit, prediction);
                if (base.CheckConditions(hit))
                {
                    if (!prediction)
                    {
                        var zoneData = new SuppressionZoneData();
                        zoneData.Dur = (int)this.Data.X;
                        zoneData.LWeapon = hit.Data.IsLWeapon;
                        zoneData.ParentWeapon = hit.Data.Action.Data.ParentWeapon;
                        zoneData.Parent = hit.Data.Target;
                        zoneData.Source = hit.Data.Source;
                        var zone = new SuppressionZone();
                        zone.SetSuppressionZoneData(zoneData);
                        hit.Data.Target.AddZone(zone);
                    }
                }
            }
        }
    }
}
