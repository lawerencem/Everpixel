using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect.Other
{
    public class SummonOnHit : MEffect
    {
        public SummonOnHit() : base(EEffect.SummonOnHit) { }

        public override void TryProcessHit(MHit hit)
        {
            // TODO
        }
    }
}
