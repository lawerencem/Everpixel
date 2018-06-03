using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Model.Barrier;
using Assets.Controller.GUI.Combat;

namespace Assets.Model.Effect.Other
{
    public class EffectBarrier : MEffect
    {
        public EffectBarrier() : base(EEffect.Barrier) { }

        public override void TryProcessHit(MHit hit, bool prediction)
        {
            base.TryProcessHit(hit, prediction);
            if (!prediction)
            {
                var tgt = hit.Data.Target.Current as CChar;
                var hp = hit.Data.Ability.Data.DmgPerPower * hit.Data.Source.Proxy.GetStat(ESecondaryStat.Power);
                var barrier = new MBarrier((int)this.Data.Duration, (int)hp);
                var data = new EvBarrierData();
                data.barrier = barrier;
                data.target = tgt;
                var ev = new EvBarrier(data);
                VHitController.Instance.DisplayBarrierCreation(tgt, hit, barrier);
            }
        }
    }
}
