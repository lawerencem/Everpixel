using Assets.Generics;
using System.Collections.Generic;

namespace Model.Map
{
    public class ColRowPairPath
    {
        public List<Pair<int, int>> Path { get; set; }

        public ColRowPairPath()
        {
            this.Path = new List<Pair<int, int>>() { };
        }

        public ColRowPairPath DeepCopy()
        {
            var copy = new ColRowPairPath();
            foreach (var pair in this.Path) { copy.Path.Add(pair); }
            return copy;
        }
    }
}
