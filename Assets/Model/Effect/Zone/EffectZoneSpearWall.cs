using Assets.Model.Combat.Hit;
using Assets.Model.Zone.Duration;

namespace Assets.Model.Effect.Zone
{
    namespace Assets.Model.Effect.Zone
    {
        public class EffectZoneSpearWall : MEffect
        {
            public EffectZoneSpearWall() : base(EEffect.Spear_Wall_Zone) { }

            public override void TryProcessHit(MHit hit, bool prediction)
            {
                base.TryProcessHit(hit, prediction);
                if (base.CheckConditions(hit))
                {
                    if (!prediction)
                    {
                        foreach (var neighbor in hit.Data.Target.GetAdjacent())
                        {
                            var zoneData = new ZoneSpearWallData();
                            zoneData.Dur = (int)this.Data.X;
                            zoneData.LWeapon = hit.Data.IsLWeapon;
                            zoneData.ParentWeapon = hit.Data.Action.Data.ParentWeapon;
                            zoneData.Source = hit.Data.Source;
                            var zone = new SpearWallZone();
                            zone.SetSpearWallZoneData(zoneData);
                            neighbor.AddZone(zone);
                        }
                    }
                }
            }
        }
    }
}
