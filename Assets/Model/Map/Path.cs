using Assets.Controller.Map.Tile;
using System.Collections.Generic;

namespace Assets.Model.Map
{
    public class Path
    {
        private int _score;
        private List<MTile> _tiles;

        public int Score { get { return this._score; } }

        public List<MTile> GetTiles() { return this._tiles; }

        public void SetScore(int s) { this._score = s; }
        public void SetTiles(List<MTile> t) { this._tiles = t; }

        public Path()
        {
            this._score = 0;
            this._tiles = new List<MTile>();
        }

        public TileController GetNextTile(TileController t)
        {
            //if (this.Tiles.Contains(t.Model))
            //{
            //    int index = this.Tiles.IndexOf(t.Model) + 1;
            //    if (this.Tiles.Count > index)
            //        return this.Tiles[index].Parent;
            //}
            return null;
        }

        public void AddTile(MTile t)
        {
            if (this._tiles.Count > 0)
                this._score += (this._tiles[this._tiles.Count - 1].GetTravelCost(t));
            else
                this._score += t.GetCost();
            this._tiles.Add(t);
        }

        public Path DeepCopy()
        {
            var path = new Path();
            path.SetScore(this.Score);
            var newTiles = new List<MTile>();
            foreach (var tile in this._tiles)
                newTiles.Add(tile);
            path.SetTiles(newTiles);
            return path;
        }
    }
}
