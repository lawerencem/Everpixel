using Model.Combat;

namespace Model.Effects
{
    public class Horror : Effect
    {
        public Horror() : base(EnumEffect.Horror) { }

        public override void TryProcessEffect(Hit hit)
        {
            // TODO: Add via an event
            this.Target.Model.AddEffect(this);
        }
    }
}
