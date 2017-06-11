using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Abilities;
using Model.Equipment;

namespace Model.Perks
{
    public class DinoBite : GenericAbilityModPerk
    {
        public DinoBite() : base(PerkEnum.Dino_Bite) { }

        public override void TryModAbility(GenericAbility ability)
        {
            base.TryModAbility(ability);
            ability.ModData.BaseDamage += 50;
        }
    }
}
