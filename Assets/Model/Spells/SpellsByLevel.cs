using Assets.Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Spells
{
    public class SpellsByLevel
    {
        public Dictionary<int, Dictionary<AbilitiesEnum, Pair<int, GenericAbility>>> Spells;

        public SpellsByLevel()
        {
            this.Spells = new Dictionary<int, Dictionary<AbilitiesEnum, Pair<int, GenericAbility>>>();
        }
    }
}
