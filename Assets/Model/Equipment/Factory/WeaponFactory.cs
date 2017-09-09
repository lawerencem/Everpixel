using Assets.Model.Equipment.Armor;
using Assets.Model.Equipment.Builder;
using Assets.Model.Equipment.Enum;
using Assets.Template.Other;

namespace Assets.Model.Equipment.Factory
{
    public class WeaponFactory : ASingleton<WeaponFactory>
    {
        private WeaponBuilder _weaponBuilder;

        public WeaponFactory() { this._weaponBuilder = new WeaponBuilder(); }

        public CWeapon CreateNewObject(string name, EEquipmentTier tier)
        {
            var weapon = this._weaponBuilder.Build(name + "_" + tier.ToString());
            weapon.Model.Data.Name = name;
            return weapon;
        }
    }
}
