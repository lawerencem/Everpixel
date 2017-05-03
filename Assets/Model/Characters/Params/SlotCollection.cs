using Model.Slot;
using System.Collections.Generic;

namespace Model.Characters
{
    public class SlotCollection
    {
        public SlotCollection()
        {
            this.Slots = new List<GenericSlot>();
        }

        public List<GenericSlot> Slots { get; set; }
        public void Add(GenericSlot s) { this.Slots.Add(s); }
    }
}
