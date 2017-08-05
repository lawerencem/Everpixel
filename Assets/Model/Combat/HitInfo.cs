using Assets.Model.Ability;
using Controller.Characters;
using Controller.Map;
using Model.Abilities;
using Model.Effects;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Combat
{
    public class Hit
    {
        private Callback _callBack;
        public delegate void Callback();

        private List<Effect> _effects;

        public GenericAbility Ability { get; set; }
        public ChancePrediction Chances { get; set; }
        public AttackEventFlags Flags { get; set; }
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
            this._effects = new List<Effect>();

            this.Chances = new ChancePrediction();
            this.IsFinished = false;
            this.ModData = new HitModData();
            this.Source = s;
            if (t.Model.Current != null && t.Model.Current.GetType().Equals(typeof(CharController)))
                this.Target = t.Model.Current as CharController;
            this.TargetTile = t;
            this.Ability = a;
            this._callBack = callback;
            this.Flags = new AttackEventFlags();
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

        public void AddEffect(Effect e) { this._effects.Add(e); }
    }
}
