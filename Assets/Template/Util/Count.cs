namespace Assets.Template.Util
{
    public class Count
    {
        private int _max;
        private int _min;

        public int Max {get {return this._max;} }
        public int Min { get { return this._min; } }

        public Count(int max, int min) { this._max = max; this._min = min; }
    }
}
