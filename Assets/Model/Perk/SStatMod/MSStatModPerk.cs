using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;

namespace Assets.Model.Perk.SStatMod
{
    public class MSStatModPerk : MPerk
    {
        public MSStatModPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetSStatModPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetSStatModPerks().Add(this);
        }

        public virtual void TryModSStat(ESecondaryStat stat, ref double value)
        {

        }
    }
}
