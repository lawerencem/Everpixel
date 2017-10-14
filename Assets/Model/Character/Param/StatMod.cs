namespace Assets.Model.Character.Param
{
    public class StatModData
    {
        public bool DurationMod { get; set; }
        public int Dur { get; set; }
        public double FlatScalar { get; set; }
        public bool FlatMod { get; set; }
        public double Scalar { get; set; }
        public object StatType { get; set; }
    }

    public class StatMod
    {
        private StatModData _data;
        public StatModData Data { get { return this._data; } }

        public StatMod(StatModData data)
        {
            this._data = data;
        }

        public void ProcessTurn()
        {
            if (this.Data.DurationMod)
                this._data.Dur--;
        }
    }
}
