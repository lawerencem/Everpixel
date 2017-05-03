using Model.Map;
using System.Collections.Generic;

namespace Generics.Hex
{
    public class GenericHexMap
    {
        public List<HexTile> Tiles { get; set; }
        protected Dictionary<int, Dictionary<int, HexTile>> _colRowDictionary;

        public GenericHexMap(List<HexTile> tiles)
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
