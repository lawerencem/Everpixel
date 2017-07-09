using Model.Abilities;

namespace Model.Perks
{
    public class DinoBite : GenericAbilityModPerk
    {
        private const int MOD = 50;

        public DinoBite() : base(PerkEnum.Dino_Bite) { }

        public override void TryModAbility(GenericAbility ability)
        {
            base.TryModAbility(ability);
            if (ability.Type == AbilitiesEnum.Bite)
                ability.ModData.BaseDamage += MOD;
        }
    }
}
