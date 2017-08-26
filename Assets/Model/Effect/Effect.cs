using Assets.Controller.Character;
using Assets.Model.Combat;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect
{
    public class MEffect
    {
        private EffectContainer _params;
        private CharController _target;
        private EEffect _type;

        public EffectContainer Params { get { return this._params; } }
        public CharController Target { get { return this._target; } }
        public EEffect Type { get { return this._type; } }
        
        public MEffect(EEffect type)
        {
            this._params = new EffectContainer();
            this._type = type;
        }

        public virtual void TryProcessEffect(Hit hit) { }

        public MEffect Copy()
        {
            var clone = new MEffect(this._type);
            clone.Params.Duration = this.Params.Duration;
            clone.Params.Value = this.Params.Duration;
            return clone;
        }

        public void SetDuration(int v) { this.Params.Duration = v; }
        public void SetValue(int v) { this.Params.Value = v; }
        public void SetTarget(CharController t) { this._target = t; }
    }
}
