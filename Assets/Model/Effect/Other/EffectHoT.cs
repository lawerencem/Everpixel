using Assets.Controller.Character;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect.Fortitude
{
    public class EffectHoT : MEffect
    {
        public EffectHoT() : base(EEffect.HoT) { }

        public override void TryProcessHit(MHit hit, bool prediction)
        {
            base.TryProcessHit(hit, prediction);
            if (base.CheckConditions(hit))
            {
                var tgt = hit.Data.Target.Current as CChar;
                hit.AddEffect(this);
                tgt.Proxy.AddEffect(this);
            }
        }
    }
}
