using Assets.Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Spells
{
    public class SpellsByLevel
    {
        public Dictionary<int, Dictionary<ActiveAbilitiesEnum, Pair<int, GenericActiveAbility>>> Spells;

        public SpellsByLevel()
        {
            this.Spells = new Dictionary<int, Dictionary<ActiveAbilitiesEnum, Pair<int, GenericActiveAbility>>>();
        }
    }
}
