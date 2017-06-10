using Assets.Generics;
using Characters.Params;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Perks
{
    public class GenericAbilityModPerk : GenericPerk
    {
        public GenericAbilityModPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryModAbility(GenericAbility ability)
        {

        }
    }
}
