using Model.Equipment;

namespace Model.Slot
{
    abstract public class AbstractSlot
    {
        protected GenericEquipment _current;
        protected SlotEnum _type;

        public GenericEquipment Current { get { return this._current; } }
        public SlotEnum Type { get { return this._type; } }
    }
}
