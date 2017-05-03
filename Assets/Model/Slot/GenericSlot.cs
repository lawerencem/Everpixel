using Model.Equipment;

namespace Model.Slot
{
    public class GenericSlot : AbstractSlot
    {
        public GenericSlot(SlotEnum s) { this._type = s; }

        public void Set(GenericEquipment g)
        {
            if (this._current.Type == g.Type)
                this._current = g;
        }

        public GenericEquipment Swap(GenericEquipment g)
        {
            if (this._current.Type == g.Type)
            {
                var old = this._current;
                this._current = g;
                return old;
            }
            else
                return null;
        }
    }
}
