using Model.Equipment;
using UnityEngine;

namespace View.Equipment
{
    public class WeaponView : EquipmentView
    {
        public EWeaponSkill Skill { get; set; }
        public EWeaponUse Use { get; set; }
    }
}
