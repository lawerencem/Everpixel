using Model.Abilities;
using Model.Events;
using Model.Slot;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class GenericHelm : GenericEquipment
    {
        public double APReduce { get; set; }
        public double BlockReduce { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageReduction { get; set; }
        public double DodgeMod { get; set; }
        public double FatigueCost { get; set; }
        public double InitativeReduce { get; set; }
        public string Name { get; set; }
        public double ParryReduce { get; set; }
        public double StaminaReduce { get; set; }


        public GenericHelm() : base(SlotEnum.Head, EquipmentTypeEnum.Worn)
        {

        }
    }
}

