using Assets.Generics;
using Assets.Model.Combat;
using Assets.Model.Perks;
using Characters.Params;
using Model.Abilities;
using Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Perk.AbilityMod
{
    public class MAbilityModPerk : MPerk
    {
        public MAbilityModPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryModAbility(Hit hit)
        {

        }
    }
}
