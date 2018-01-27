using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public class HexMap
    {
        protected Dictionary<int, Dictionary<int, IHex>> _colRowDictionary;
        protected int _cols;
        protected int _rows;

        public List<IHex> Tiles { get; set; }

        public HexMap(List<IHex> tiles, int rows, int cols)
        {
            this.Tiles = new List<IHex>();
            this._colRowDictionary = new Dictionary<int, Dictionary<int, IHex>>();

            foreach (var t in tiles)
            {
                if (!this._colRowDictionary.ContainsKey(t.GetCol()))
                    this._colRowDictionary.Add(t.GetCol(), new Dictionary<int, IHex>());
                this._colRowDictionary[t.GetCol()].Add(t.GetRow(), t);

                this.Tiles.Add(t);
            }

            this._cols = cols;
            this._rows = rows;
        }

        public int GetFirstCol() { return 0; }
        public int GetFirstRow() { return 0; }
        public int GetMidCol() { return this._cols / 2; }
        public int GetMidRow() { return this._rows / 2; }
        public int GetLastCol() { return this._cols; }
        public int GetLastRow() { return this._rows; }

        public IHex GetOppositeTile(IHex source, IHex target)
        {
            if (this.GetN(source).Equals(target))
                return this.GetN(target);
            else if (this.GetNE(source).Equals(target))
                return this.GetNE(target);
            else if (this.GetSE(source).Equals(target))
                return this.GetSE(target);
            else if (this.GetS(source).Equals(target))
                return this.GetS(target);
            else if (this.GetSW(source).Equals(target))
                return this.GetSW(target);
            else if (this.GetNW(source).Equals(target))
                return this.GetNW(target);
            else
                return null;
        }

        public IHex GetTileViaColRowPair(int col, int row)
        {
            if (this._colRowDictionary.ContainsKey(col))
                if (this._colRowDictionary[col].ContainsKey(row))
                    return this._colRowDictionary[col][row];
            return null;
        }

        public IHex GetNE(IHex t)
        {
            int col = -1;
            int row = -1;

            if (t.GetCol() % 2 == 0)
            {
                col = t.GetCol() + 1;
                row = t.GetRow() - 1;
            }
            else
            {
                col = t.GetCol() + 1;
                row = t.GetRow();
            }
            return GetDirectionalTile(col, row);
        }

        public IHex GetSE(IHex t)
        {
            int col = -1;
            int row = -1;

            if (t.GetCol() % 2 == 0)
            {
                col = t.GetCol() + 1;
                row = t.GetRow();
            }
            else
            {
                col = t.GetCol() + 1;
                row = t.GetRow() + 1;
            }
            return GetDirectionalTile(col, row);
        }

        public IHex GetS(IHex t)
        {
            return GetDirectionalTile(t.GetCol(), t.GetRow() + 1);
        }

        public IHex GetSW(IHex t)
        {
            int col = -1;
            int row = -1;

            if (t.GetCol() % 2 == 0)
            {
                col = t.GetCol() - 1;
                row = t.GetRow();
            }
            else
            {
                col = t.GetCol() - 1;
                row = t.GetRow() + 1;
            }
            return GetDirectionalTile(col, row);
        }

        public IHex GetNW(IHex t)
        {
            int col = -1;
            int row = -1;

            if (t.GetCol() % 2 == 0)
            {
                col = t.GetCol() - 1;
                row = t.GetRow() - 1;
            }
            else
            {
                col = t.GetCol() - 1;
                row = t.GetRow();
            }
            return GetDirectionalTile(col, row);
        }

        public IHex GetN(IHex t)
        {
            return GetDirectionalTile(t.GetCol(), t.GetRow() - 1);
        }

        public bool IsTileN(IHex s, IHex t)
        {
            if (this.GetN(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileNE(IHex s, IHex t)
        {
            if (this.GetNE(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileSE(IHex s, IHex t)
        {
            if (this.GetSE(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileS(IHex s, IHex t)
        {
            if (this.GetS(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileSW(IHex s, IHex t)
        {
            if (this.GetSW(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileNW(IHex s, IHex t)
        {
            if (this.GetNW(s).Equals(t))
                return true;
            else
                return false;
        }

        private IHex GetDirectionalTile(int c, int r)
        {
            if (this._colRowDictionary.ContainsKey(c))
                if (this._colRowDictionary[c].ContainsKey(r))
                    return this._colRowDictionary[c][r];
                else
                    return null;
            else
                return null;
        }
    }
}
