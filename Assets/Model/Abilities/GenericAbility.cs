using Model.Characters;
using Model.Combat;
using Model.Injuries;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class GenericAbility
    {
        protected bool _hostile = true;
        protected object _type;
        protected string _typeStr = "";

        public bool Hostile { get { return this._hostile; } }
        public object Type { get { return this._type; } }
        public string TypeStr { get { return this._typeStr; } }

        public int APCost { get; set; }
        public GenericAbilityModData ModData { get; set; }
        public List<InjuryEnum> Injuries { get; set; }
        public int StaminaCost { get; set; }

        public virtual void ProcessAbility(HitInfo hit) { }

        public double GetAPCost() { return this.APCost; }

        public GenericAbility()
        {
            this.Injuries = new List<InjuryEnum>();
            this.ModData = new GenericAbilityModData();
        }
    }
}
