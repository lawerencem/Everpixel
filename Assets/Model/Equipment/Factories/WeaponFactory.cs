using Generics;
using Model.Equipment;

namespace Assets.Model.Equipment.Factories
{
    public class WeaponFactory : AbstractSingleton<WeaponFactory>
    {
        private WeaponBuilder _weaponBuilder;

        public WeaponFactory() { this._weaponBuilder = new WeaponBuilder(); }

        public GenericWeapon CreateNewObject(string name, EquipmentTierEnum tier)
        {
            var weapon = this._weaponBuilder.Build(name + "_" + tier.ToString());
            weapon.Name = name;
            return weapon;
        }
    }
}
