using Assets.Model.Character.Param;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.AbilityMod
{
    public class MAbilityModPerk : MPerk
    {
        public MAbilityModPerk(EPerk type) : base(type) { }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetAbilityModPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetAbilityModPerks().Add(this);
        }

        public virtual void TryModAbility(MHit hit)
        {

        }
    }
}
