using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Action;
using Assets.Model.Effect;
using Assets.Model.Event.Combat;
using System.Collections.Generic;

namespace Assets.Model.Combat.Hit
{
    public class HitData
    {
        public MAbility Ability { get; set; }
        public MAction Action { get; set; }
        public HitPrediction Chances { get; set; }
        public List<MEvCombat> Events { get; set; }
        public FHit Flags { get; set; }
        public HitMod ModData { get; set; }
        protected HitPrediction Prediction { get; set; }
        public bool IsFatality { get; set; }
        public bool IsHeal { get; set; }
        public bool IsLWeapon { get; set; }
        public bool IsWeapon { get; set; }
        public int Dmg { get; set; }
        public CChar Source { get; set; }
        public CTile Target { get; set; }

        public HitData()
        {
            this.Chances = new HitPrediction();
            this.Events = new List<MEvCombat>();
            this.Flags = new FHit();
            this.ModData = new HitMod();
        }
    }
}
