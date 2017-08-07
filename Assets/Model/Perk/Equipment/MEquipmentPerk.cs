using Assets.Model.Character;

namespace Assets.Model.Perk.Equipment
{
    public class MEquipmentPerk : MPerk
    {
        public MEquipmentPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryProcessAdd(MChar character, object equipment)
        {

        }

        public virtual void TryProcessRemove(MChar character, object equipment)
        {

        }
    }
}
