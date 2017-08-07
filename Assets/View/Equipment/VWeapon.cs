using Assets.Model.Equipment.Enum;

namespace Assets.View.Equipment
{
    public class VWeapon : VEquipment
    {
        public EWeaponSkill Skill { get; set; }
        public EWeaponUse Use { get; set; }
    }
}
