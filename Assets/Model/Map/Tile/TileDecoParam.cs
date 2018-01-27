using System.Collections.Generic;

namespace Assets.Model.Map.Tile
{
    public class EnvironmentParam
    {
        private EEnvironment _type;

        public List<int> Sprites { get; set; }
        
        public EnvironmentParam(EEnvironment type)
        {
            this._type = type;
            this.Sprites = new List<int>();
        }
    }
}
