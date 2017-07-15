using Model.Characters;
using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Characters.Params
{
    public class RaceParams
    {
        public List<PerkEnum> DefaultPerks { get; set; }
        public PrimaryStats PrimaryStats { get; set; }
        public RaceSprites Sprites { get; set; }

        public RaceParams()
        {
            this.DefaultPerks = new List<PerkEnum>();
            this.PrimaryStats = new PrimaryStats();
            this.Sprites = new RaceSprites();
        }
    }
}
