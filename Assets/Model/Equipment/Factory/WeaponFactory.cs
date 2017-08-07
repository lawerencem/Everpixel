using Assets.Model.Equipment.Builder;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Type;
using Template.Other;

namespace Assets.Model.Equipment.Factory
{
    public class WeaponFactory : ASingleton<WeaponFactory>
    {
        private WeaponBuilder _weaponBuilder;

        public WeaponFactory() { this._weaponBuilder = new WeaponBuilder(); }

        public MWeapon CreateNewObject(string name, EEquipmentTier tier)
        {
            var weapon = this._weaponBuilder.Build(name + "_" + tier.ToString());
            weapon.Name = name;
            return weapon;
        }
    }
}
