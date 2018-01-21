//using Assets.Controller.Map.Tile;
//using System.Collections.Generic;

//namespace Assets.Model.Map.Combat.Tile
//{
//    public class Path
//    {
//        private int _score;
//        private List<MTile> _tiles;

//        public int Score { get { return this._score; } }

//        public List<MTile> GetTiles() { return this._tiles; }

//        public void SetScore(int s) { this._score = s; }
//        public void SetTiles(List<MTile> t) { this._tiles = t; }

//        public Path()
//        {
//            this._score = 0;
//            this._tiles = new List<MTile>();
//        }

//        public CTile GetNextTile(CTile t)
//        {
//            if (this._tiles.Contains(t.Model))
//            {
//                int index = this._tiles.IndexOf(t.Model) + 1;
//                if (this._tiles.Count > index)
//                    return this._tiles[index].Controller;
//            }
//            return null;
//        }

//        public CTile GetFirstTile()
//        {
//            if (this._tiles.Count > 0)
//                return this._tiles[0].Controller;
//            else
//                return null;
//        }

//        public void AddTile(MTile t)
//        {
//            if (this._tiles.Count > 0)
//                this._score += (this._tiles[this._tiles.Count - 1].GetTravelCost(t));
//            else
//                this._score += t.GetCost();
//            this._tiles.Add(t);
//        }

//        public Path DeepCopy()
//        {
//            var path = new Path();
//            path.SetScore(this.Score);
//            var newTiles = new List<MTile>();
//            foreach (var tile in this._tiles)
//                newTiles.Add(tile);
//            path.SetTiles(newTiles);
//            return path;
//        }
//    }
//}
