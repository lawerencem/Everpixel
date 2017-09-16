using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect
{
    public class MEffectData
    {
        public int Duration { get; set; }
        public string SummonKey { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class MEffect
    {
        private MEffectData _data;
        private EEffect _type;

        public MEffectData Data { get { return this._data; } }
        public EEffect Type { get { return this._type; } }

        public void SetData(MEffectData data) { this._data = data; }
        
        public MEffect(EEffect type)
        {
            this._type = type;
        }

        public virtual void TryProcessHit(MHit hit) { }
        public virtual void TryProcessTurn(MHit hit) { }
    }
}
