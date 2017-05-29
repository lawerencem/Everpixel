using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class GenericAbility
    {
        protected object _type;
        public object Type { get { return this._type; } }

        public int APCost { get; set; }
        public int StaminaCost { get; set; }

        public virtual void ProcessAbility(HitInfo hit) { }

        public double GetAPCost() { return this.APCost; }
    }
}
