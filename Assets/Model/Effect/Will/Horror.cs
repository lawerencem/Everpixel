using Assets.Model.Effect;

namespace Assets.Model.Effects.Will
{
    public class Horror : MEffect
    {
        public Horror(MEffectData data) : base(EEffect.Horror, data) { }

        public override void TryProcessHit()
        {
            // TODO
        }
    }
}
