using Model.Characters;
using Model.Combat;
using Model.Injuries;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class GenericAbility
    {
        protected object _type;
        public object Type { get { return this._type; } }

        public int APCost { get; set; }
        public List<InjuryEnum> Injuries { get; set; }
        public int StaminaCost { get; set; }

        public virtual void ProcessAbility(HitInfo hit) { }

        public double GetAPCost() { return this.APCost; }

        public GenericAbility()
        {
            this.Injuries = new List<InjuryEnum>();
        }
    }
}
