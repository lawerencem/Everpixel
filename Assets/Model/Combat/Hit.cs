using Assets.Controller.Character;
using Assets.Model.Ability;
using Assets.Model.Effect;
using Controller.Map;
using System.Collections.Generic;

namespace Assets.Model.Combat
{
    public class Hit
    {
        private Callback _callBack;
        public delegate void Callback();

        private List<MEffect> _effects;

        public MAbility Ability { get; set; }
        public ChancePrediction Chances { get; set; }
        public FHit Flags { get; set; }
        public bool FXProcessed { get; set; }
        public HitModData ModData { get; set; }
        public bool IsFinished { get; set; }
        public bool IsHeal { get; set; }
        public int Dmg { get; set; }
        public CharController Source { get; set; }
        public CharController Target { get; set; }
        public TileController TargetTile { get; set; }

        public Hit(AbilityArgContainer arg)
        {
            //this._effects = new List<Effect>();

            //this.Chances = new ChancePrediction();
            //this.IsFinished = false;
            //this.ModData = new HitModData();
            //this.Source = s;
            //if (t.Model.Current != null && t.Model.Current.GetType().Equals(typeof(CharController)))
            //    this.Target = t.Model.Current as CharController;
            //this.TargetTile = t;
            //this.Ability = a;
            //this._callBack = callback;
            //this.Flags = new FHit();
        }

        public void Done()
        {
            this.Ability.TryApplyInjury(this);

            if (this._callBack != null)
            {
                this.IsFinished = true;
                this._callBack();
            }
        }

        public void AddEffect(MEffect e) { this._effects.Add(e); }
    }
}
