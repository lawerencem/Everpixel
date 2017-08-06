using Assets.Model.Combat;
using Assets.Model.Effect;

namespace Assets.Model.Effects.Will
{
    public class Horror : MEffect
    {
        public Horror() : base(EEffect.Horror) { }

        public override void TryProcessEffect(Hit hit)
        {
            // TODO: Add via an event
            this.Target.Model.AddEffect(this);
        }
    }
}
