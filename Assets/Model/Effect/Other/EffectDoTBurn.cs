using Assets.Controller.Character;
using Assets.Model.Combat.Hit;
using Assets.Model.OTE;
using Assets.Model.OTE.DoT;

namespace Assets.Model.Effect.Fortitude
{
    public class EffectDoTBurn : MEffect
    {
        public EffectDoTBurn() : base(EEffect.DoTBurn) { }

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
                data.Dur = (int)hit.Data.Ability.Data.Duration;;
                var hot = new MDoT(EDoT.Burn, data);
                tgt.Proxy.AddDoT(hot);
            }
        }
    }
}
