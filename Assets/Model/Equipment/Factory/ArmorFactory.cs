using Assets.Model.Equipment.Builder;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Type;
using Assets.Template.Other;

namespace Assets.Model.Equipment.Factory
{
    public class ArmorFactory : ASingleton<ArmorFactory>
    {
        private ArmorBuilder _armorBuilder;

        public ArmorFactory() { this._armorBuilder = new ArmorBuilder(); }

        public MArmor CreateNewObject(string name, EEquipmentTier tier)
        {
            var armor = this._armorBuilder.Build(name + "_" + tier.ToString());
            armor.Name = name;
            return armor;
        }
    }
}
