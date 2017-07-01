using Model.Events.Combat;

namespace Model.Abilities
{
    public class GenericSummonAbility : GenericAbility
    {
        public string toSummon = "";

        public GenericSummonAbility(AbilitiesEnum type) : base(type)
        {

        }
    }
}
