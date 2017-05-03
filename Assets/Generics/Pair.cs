namespace Assets.Generics
{
    public struct Pair<T, U>
    {
        private T _x;
        private U _y;

        public T X { get { return this._x; } }
        public U Y { get { return this._y; } }

        public Pair(T x, U y)
        {
            this._x = x;
            this._y = y;
        }
    }
}
