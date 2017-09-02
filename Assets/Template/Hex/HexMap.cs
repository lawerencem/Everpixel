using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public class HexMap
    {
        protected Dictionary<int, Dictionary<int, HexTile>> _colRowDictionary;
        protected int _cols;
        protected int _rows;

        public List<HexTile> Tiles { get; set; }

        public HexMap(List<HexTile> tiles, int rows, int cols)
        {
            this.Tiles = new List<HexTile>();
            this._colRowDictionary = new Dictionary<int, Dictionary<int, HexTile>>();

            foreach (var t in tiles)
            {
                if (!this._colRowDictionary.ContainsKey(t.Col))
                    this._colRowDictionary.Add(t.Col, new Dictionary<int, HexTile>());
                this._colRowDictionary[t.Col].Add(t.Row, t);

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

        public HexTile GetOppositeTile(HexTile source, HexTile target)
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

        public HexTile GetTileViaColRowPair(int col, int row)
        {
            if (this._colRowDictionary.ContainsKey(col))
                if (this._colRowDictionary[col].ContainsKey(row))
                    return this._colRowDictionary[col][row];
            return null;
        }

        public HexTile GetNE(HexTile t)
        {
            int col = -1;
            int row = -1;

            if (t.Col % 2 == 0)
            {
                col = t.Col + 1;
                row = t.Row - 1;
            }
            else
            {
                col = t.Col + 1;
                row = t.Row;
            }
            return GetDirectionalTile(col, row);
        }

        public HexTile GetSE(HexTile t)
        {
            int col = -1;
            int row = -1;

            if (t.Col % 2 == 0)
            {
                col = t.Col + 1;
                row = t.Row;
            }
            else
            {
                col = t.Col + 1;
                row = t.Row + 1;
            }
            return GetDirectionalTile(col, row);
        }

        public HexTile GetS(HexTile t)
        {
            return GetDirectionalTile(t.Col, t.Row + 1);
        }

        public HexTile GetSW(HexTile t)
        {
            int col = -1;
            int row = -1;

            if (t.Col % 2 == 0)
            {
                col = t.Col - 1;
                row = t.Row;
            }
            else
            {
                col = t.Col - 1;
                row = t.Row + 1;
            }
            return GetDirectionalTile(col, row);
        }

        public HexTile GetNW(HexTile t)
        {
            int col = -1;
            int row = -1;

            if (t.Col % 2 == 0)
            {
                col = t.Col - 1;
                row = t.Row - 1;
            }
            else
            {
                col = t.Col - 1;
                row = t.Row;
            }
            return GetDirectionalTile(col, row);
        }

        public HexTile GetN(HexTile t)
        {
            return GetDirectionalTile(t.Col, t.Row - 1);
        }

        public bool IsTileN(HexTile s, HexTile t)
        {
            if (this.GetN(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileNE(HexTile s, HexTile t)
        {
            if (this.GetNE(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileSE(HexTile s, HexTile t)
        {
            if (this.GetSE(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileS(HexTile s, HexTile t)
        {
            if (this.GetS(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileSW(HexTile s, HexTile t)
        {
            if (this.GetSW(s).Equals(t))
                return true;
            else
                return false;
        }

        public bool IsTileNW(HexTile s, HexTile t)
        {
            if (this.GetNW(s).Equals(t))
                return true;
            else
                return false;
        }

        private HexTile GetDirectionalTile(int c, int r)
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
