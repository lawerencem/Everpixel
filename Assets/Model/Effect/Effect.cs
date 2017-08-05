using Controller.Characters;
using Model.Combat;

namespace Model.Effects
{
    public class Effect
    {
        private EffectContainer _params;
        private CharController _target;
        private EnumEffect _type;

        public EffectContainer Params { get { return this._params; } }
        public CharController Target { get { return this._target; } }
        public EnumEffect Type { get { return this._type; } }
        
        public Effect(EnumEffect type)
        {
            this._params = new EffectContainer();
            this._type = type;
        }

        public virtual void TryProcessEffect(Hit hit) { }

        public Effect Copy()
        {
            var clone = new Effect(this._type);
            clone.Params.Duration = this.Params.Duration;
            clone.Params.Value = this.Params.Duration;
            return clone;
        }

        public void SetDuration(int v) { this.Params.Duration = v; }
        public void SetValue(int v) { this.Params.Value = v; }
        public void SetTarget(CharController t) { this._target = t; }
    }
}
