using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.View.Fatality
{
    public class FatalityData
    {
        public MAction Action { get; set; }
        public List<Hit> FatalHits { get; set; }
        public List<string> CustomPostFatalityBarks { get; set; }
        public List<string> CustomPreFatalityBarks { get; set; }
        public List<Hit> NonFatalHits { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }

        public FatalityData()
        {
            this.FatalHits = new List<Hit>();
            this.CustomPostFatalityBarks = new List<string>();
            this.CustomPreFatalityBarks = new List<string>();
            this.NonFatalHits = new List<Hit>();
        }
    }
}
