using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect.Fortitude
{
    public class EffectSlime : MEffect
    {
        public EffectSlime() : base(EEffect.Slime) { }

        public override void TryProcessHit(MHit hit, bool prediction)
        {
            base.TryProcessHit(hit, prediction);
            if (base.CheckConditions(hit))
            {

            }
        }
    }
}
