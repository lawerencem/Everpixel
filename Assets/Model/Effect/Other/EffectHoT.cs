using Assets.Controller.Character;
using Assets.Model.Combat.Hit;
using Assets.Model.OTE;
using Assets.Model.OTE.HoT;

namespace Assets.Model.Effect.Fortitude
{
    public class EffectHoT : MEffect
    {
        public EffectHoT() : base(EEffect.HoT) { }

        public override void TryProcessHit(MHit hit, bool prediction)
        {
            base.TryProcessHit(hit, prediction);
            if (base.CheckConditions(hit))
                hit.AddEffect(this);
            if (base.CheckConditions(hit) && !prediction)
            {
                var tgt = hit.Data.Target.Current as CChar;
                var data = new MOTEData();
                data.Dmg = hit.Data.Dmg;
                // TODO: Will need to grab dur from hit for dynamic durations at some point
                data.Dur = (int)hit.Data.Ability.Data.Duration;
                var hot = new MHoT(data);
                tgt.Proxy.AddHoT(hot);
            }
        }
    }
}
