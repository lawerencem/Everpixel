using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Effect;
using Assets.Model.Event.Combat;
using System.Collections.Generic;

namespace Assets.Model.Combat.Hit
{
    public class HitData
    {
        public MAbility Ability { get; set; }
        public HitPrediction Chances { get; set; }
        public List<MEffect> Effects { get; set; }
        public List<MEvCombat> Events { get; set; }
        public FHit Flags { get; set; }
        public HitMod ModData { get; set; }
        protected HitPrediction Prediction { get; set; }
        public bool IsHeal { get; set; }
        public int Dmg { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }

        public HitData()
        {
            this.Chances = new HitPrediction();
            this.Effects = new List<MEffect>();
            this.Events = new List<MEvCombat>();
            this.Flags = new FHit();
            this.ModData = new HitMod();
        }
    }
}
