using Assets.Template.Hex;
using System.Collections.Generic;

namespace Assets.Template.Pathing
{
    public class Path
    {
        private int _score;
        private List<IHex> _tiles;

        public int Score { get { return this._score; } }

        public List<IHex> GetTiles() { return this._tiles; }

        public void SetScore(int s) { this._score = s; }
        public void SetTiles(List<IHex> t) { this._tiles = t; }

        public Path()
        {
            this._score = 0;
            this._tiles = new List<IHex>();
        }

        public IHex GetNextTile(IHex t)
        {
            if (this._tiles.Contains(t))
            {
                int index = this._tiles.IndexOf(t) + 1;
                if (this._tiles.Count > index)
                    return this._tiles[index];
            }
            return null;
        }

        public void AddTile(IHex t, IPathable navigator)
        {
            if (this._tiles.Count == 0)
                this._score += navigator.GetTileTraversalCost(navigator.GetCurrentTile(), t);
            else
                this._score += navigator.GetTileTraversalCost(this._tiles[this._tiles.Count - 1], t);
            this._tiles.Add(t);
        }

        public Path DeepCopy()
        {
            var path = new Path();
            path.SetScore(this.Score);
            var newTiles = new List<IHex>();
            foreach (var tile in this._tiles)
                newTiles.Add(tile);
            path.SetTiles(newTiles);
            return path;
        }

        public IHex GetFirstTile()
        {
            if (this._tiles.Count == 1)
                return this._tiles[0];
            else if (this._tiles.Count > 1)
                return this._tiles[1];
            else
                return null;
        }

        public IHex GetLastTile()
        {
            if (this._tiles.Count > 0)
                return this._tiles[this._tiles.Count - 1];
            else
                return null;
        }
    }
}
