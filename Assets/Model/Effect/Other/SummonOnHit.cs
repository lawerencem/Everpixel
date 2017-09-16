using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;

namespace Assets.Model.Effect.Other
{
    public class SummonOnHit : MEffect
    {
        public SummonOnHit() : base(EEffect.SummonOnHit) { }

        public override void TryProcessHit(MHit hit)
        {
            hit.AddCallback(this.ProcessSummon);
        }

        private void ProcessSummon(object o)
        {
            if (o.GetType().Equals(typeof(MHit)))
            {
                var hit = o as MHit;
                var data = new EvSummonData();
                data.Duration = this.Data.Duration;
                data.LParty = hit.Data.Source.Proxy.LParty;
                data.ParticlePath = this.Data.ParticlePath;
                data.Party = hit.Data.Source.Proxy.GetParentParty();
                data.TargetTile = hit.Data.Target;
                data.ToSummon = this.Data.SummonKey;
                var e = new EvSummon(data);
                e.TryProcess();
            }
        }
    }
}
