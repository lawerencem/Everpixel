using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Abilities;
using Model.Equipment;

namespace Model.Perks
{
    public class TRexBite : GenericAbilityModPerk
    {
        protected const double VALUE = 1.25;

        public TRexBite() : base(PerkEnum.T_Rex_Bite) { }

        public override void TryModAbility(GenericAbility ability)
        {
            base.TryModAbility(ability);
            ability.ModData.BaseDamage += 75;
        }
    }
}
