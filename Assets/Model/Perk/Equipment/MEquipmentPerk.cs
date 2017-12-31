using Assets.Model.Character;
using Assets.Model.Character.Param;

namespace Assets.Model.Perk.Equipment
{
    public class MEquipmentPerk : MPerk
    {
        public MEquipmentPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetEquipmentPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetEquipmentPerks().Add(this);
        }

        public virtual void TryProcessAdd(PChar character, object equipment)
        {

        }

        public virtual void TryProcessRemove(PChar character, object equipment)
        {

        }


    }
}
