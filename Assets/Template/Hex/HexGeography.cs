using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public class HexGeography
    {
        public List<IHex> GetTiles(EDirection direction, int dist, IHex source)
        {
            switch(direction)
            {
                case (EDirection.N): { return this.GetNTiles(dist, source); }
                case (EDirection.NE): { return this.GetNETiles(dist, source); }
                case (EDirection.SE): { return this.GetSETiles(dist, source); }
                case (EDirection.S): { return this.GetSTiles(dist, source); }
                case (EDirection.SW): { return this.GetSWTiles(dist, source); }
                case (EDirection.NW): { return this.GetNWTiles(dist, source); }
                default: return null;
            }
        }

        private List<IHex> GetNTiles(int dist, IHex source)
        {
            var tiles = new List<IHex>() { source };
            var current = source;
            for(int i = 0; i < dist; i++)
            {
                if (source.GetN() != null)
                {
                    current = source.GetN();
                    tiles.Add(current);
                }
            }
            return tiles;
        }

        private List<IHex> GetNETiles(int dist, IHex source)
        {
            var tiles = new List<IHex>() { source };
            var current = source;
            for (int i = 0; i < dist; i++)
            {
                if (source.GetNE() != null)
                {
                    current = source.GetN();
                    tiles.Add(current);
                }
            }
            return tiles;
        }

        private List<IHex> GetSETiles(int dist, IHex source)
        {
            var tiles = new List<IHex>() { source };
            var current = source;
            for (int i = 0; i < dist; i++)
            {
                if (source.GetSE() != null)
                {
                    current = source.GetSE();
                    tiles.Add(current);
                }
            }
            return tiles;
        }

        private List<IHex> GetSTiles(int dist, IHex source)
        {
            var tiles = new List<IHex>() { source };
            var current = source;
            for (int i = 0; i < dist; i++)
            {
                if (source.GetS() != null)
                {
                    current = source.GetS();
                    tiles.Add(current);
                }
            }
            return tiles;
        }

        private List<IHex> GetSWTiles(int dist, IHex source)
        {
            var tiles = new List<IHex>() { source };
            var current = source;
            for (int i = 0; i < dist; i++)
            {
                if (source.GetSW() != null)
                {
                    current = source.GetSW();
                    tiles.Add(current);
                }
            }
            return tiles;
        }

        private List<IHex> GetNWTiles(int dist, IHex source)
        {
            var tiles = new List<IHex>() { source };
            var current = source;
            for (int i = 0; i < dist; i++)
            {
                if (source.GetNW() != null)
                {
                    current = source.GetNW();
                    tiles.Add(current);
                }
            }
            return tiles;
        }
    }
}
