using Model.Characters;
using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Character.Param.Race
{
    public class RaceParams
    {
        public List<EPerk> DefaultPerks { get; set; }
        public PrimaryStats PrimaryStats { get; set; }
        public RaceSprites Sprites { get; set; }

        public RaceParams()
        {
            this.DefaultPerks = new List<EPerk>();
            this.PrimaryStats = new PrimaryStats();
            this.Sprites = new RaceSprites();
        }
    }
}
