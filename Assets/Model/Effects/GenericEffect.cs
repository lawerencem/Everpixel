using Model.Combat;

namespace Model.Effects
{
    public class GenericEffect
    {
        private EffectsEnum _type;
        public EffectsEnum Type { get { return this._type; } }

        public EffectContainer Container { get; set; }

        public GenericEffect(EffectsEnum type)
        {
            this.Container = new EffectContainer();
            this._type = type;
        }

        public virtual void TryProcessEffect(HitInfo hit)
        {

        }

        public GenericEffect Copy()
        {
            var clone = new GenericEffect(this._type);
            clone.Container.Duration = this.Container.Duration;
            clone.Container.Value = this.Container.Duration;
            return clone;
        }

        public void SetDuration(int v) { this.Container.Duration = v; }
        public void SetValue(int v) { this.Container.Value = v; }
    }
}
