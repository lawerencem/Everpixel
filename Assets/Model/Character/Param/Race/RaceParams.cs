using Assets.Model.Perk;
using System.Collections.Generic;

namespace Assets.Model.Character.Param.Race
{
    public class RaceParams
    {
        public List<EPerk> DefaultPerks { get; set; }
        public PStats PrimaryStats { get; set; }
        public RaceSprites Sprites { get; set; }

        public RaceParams()
        {
            this.DefaultPerks = new List<EPerk>();
            this.PrimaryStats = new PStats();
            this.Sprites = new RaceSprites();
        }
    }
}
