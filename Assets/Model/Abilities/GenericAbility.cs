using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class GenericAbility
    {
        protected object _type;
        public object Type { get { return this._type; } }

        public virtual void ProcessAbility(HitInfo hit) { }
    }
}
