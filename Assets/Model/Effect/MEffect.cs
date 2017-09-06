using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect
{
    public class MEffectData
    {
        public int Duration { get; set; }
        public Hit Hit { get; set; }
        public int Value { get; set; }
    }

    public class MEffect
    {
        private MEffectData _data;
        private EEffect _type;

        public MEffectData Data { get { return this._data; } }
        public EEffect Type { get { return this._type; } }
        
        public MEffect(EEffect type, MEffectData data)
        {
            this._data = data;
            this._type = type;
        }

        public virtual void TryProcessHit() { }
        public virtual void TryProcessTurn() { }
    }
}
