using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Abilities;
using Model.Equipment;
using Model.Combat;

namespace Model.Perks
{
    public class TRexBite : GenericAbilityModPerk
    {
        public TRexBite() : base(PerkEnum.T_Rex_Bite) { }

        public override void TryModAbility(HitInfo hit)
        {
            base.TryModAbility(hit);
            if (hit.Ability.Type == AbilitiesEnum.Bite)
                hit.ModData.BaseDamage += 100;
        }
    }
}
