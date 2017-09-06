using Assets.Controller.Character;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect
{
    public class MEffectData
    {
        public int Duration { get; set; }
        public int Value { get; set; }
    }

    public class MEffect
    {
        private MEffectData _data;
        private CharController _target;
        private EEffect _type;

        public MEffectData Data { get { return this._data; } }
        public CharController Target { get { return this._target; } }
        public EEffect Type { get { return this._type; } }
        
        public MEffect(EEffect type)
        {
            this._data = new MEffectData();
            this._type = type;
        }

        public virtual void TryProcessEffect(Hit hit) { }

        public MEffect Copy()
        {
            var clone = new MEffect(this._type);
            clone.Data.Duration = this._data.Duration;
            clone.Data.Value = this._data.Duration;
            return clone;
        }

        public void SetDuration(int v) { this._data.Duration = v; }
        public void SetValue(int v) { this._data.Value = v; }
        public void SetTarget(CharController t) { this._target = t; }
    }
}
