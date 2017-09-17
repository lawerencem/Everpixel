using Assets.Model.Character;

namespace Assets.Model.Perk.Equipment
{
    public class MEquipmentPerk : MPerk
    {
        public MEquipmentPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryProcessAdd(PChar character, object equipment)
        {

        }

        public virtual void TryProcessRemove(PChar character, object equipment)
        {

        }
    }
}
