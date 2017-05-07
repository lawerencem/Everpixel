using Model.Abilities;
using Model.Events;
using Model.Slot;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class GenericWeapon : GenericEquipment
    {
        public string Name { get; set; }
        public List<WeaponAbility> Abilities { get; set; }
        public double Accuracy { get; set; }
        public double APReduce { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }
        public double FatigueCost { get; set; }
        public double FatigueReduce { get; set; }
        public double InitiativeReduce { get; set; }
        public double ParryMod { get; set; }
        public double Range { get; set; }
        public double ShieldDamage { get; set; }


        public GenericWeapon() : base(SlotEnum.Weapon, EquipmentTypeEnum.Held)
        {
            this.Abilities = new List<WeaponAbility>();
        }
    }
}
