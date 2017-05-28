using Generics;
using Model.Equipment;

namespace Assets.Model.Equipment.Factories
{
    public class ArmorFactory : AbstractSingleton<ArmorFactory>
    {
        private ArmorBuilder _armorBuilder;

        public ArmorFactory() { this._armorBuilder = new ArmorBuilder(); }

        public GenericArmor CreateNewObject(string name, EquipmentTierEnum tier)
        {
            var armor = this._armorBuilder.Build(name + "_" + tier.ToString());
            armor.Name = name;
            return armor;
        }
    }
}
