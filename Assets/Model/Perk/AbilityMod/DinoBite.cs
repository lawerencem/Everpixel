using Assets.Model.Perks;

namespace Assets.Mode.Perk.AbilityMod
{
    public class DinoBite : AbilityModPerk
    {
        public DinoBite() : base(EPerk.Dino_Bite) { }

        public override void TryModAbility(Hit hit)
        {
            base.TryModAbility(hit);
            if (hit.Ability.Type == EAbility.Bite)
                hit.ModData.BaseDamage += this.Val;
        }
    }
}
