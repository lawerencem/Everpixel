using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect.Will
{
    public class Horror : MEffect
    {
        public Horror() : base(EEffect.Horror) { }

        public override void TryProcessHit(MHit hit, bool prediction)
        {
            // TODO
        }
    }
}
