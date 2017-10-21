using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;

namespace Assets.Model.Effect.Other
{
    public class EffectSummonOnHit : MEffect
    {
        public EffectSummonOnHit() : base(EEffect.Summon_On_Hit) { }

        public override void TryProcessHit(MHit hit)
        {
            base.TryProcessHit(hit);
            if (base.CheckConditions(hit))
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
