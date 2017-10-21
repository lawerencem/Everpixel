using Assets.Model.Combat.Hit;
using Assets.Model.Zone.Duration;

namespace Assets.Model.Effect.Zone
{
    namespace Assets.Model.Effect.Zone
    {
        public class EffectSlimeZone : MEffect
        {
            public EffectSlimeZone() : base(EEffect.ZoneSlime) { }

            public override void TryProcessHit(MHit hit)
            {
                base.TryProcessHit(hit);
                if (base.CheckConditions(hit))
                {
                    var data = new DurationZoneData();
                    data.Dur = (int)this.Data.X;
                    data.Handle = hit.Data.Target.Handle;
                    data.Source = hit.Data.Source;
                    var zone = new ZoneSlime();
                    zone.SetData(data);
                }
            }
        }
    }

}
