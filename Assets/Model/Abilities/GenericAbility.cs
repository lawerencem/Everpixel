using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class GenericAbility
    {
        protected object _type;
        public object Type { get { return this._type; } }

        public virtual void ProcessAbility(GenericCharacter s, GenericCharacter t) { }

        public virtual void ProcessStats(GenericCharacter s, GenericCharacter t, AttackEventFlags f)
        {

        }
    }
}
