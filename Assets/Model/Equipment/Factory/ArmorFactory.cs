using Assets.Model.Equipment.Armor;
using Assets.Model.Equipment.Builder;
using Assets.Model.Equipment.Enum;
using Assets.Template.Other;

namespace Assets.Model.Equipment.Factory
{
    public class ArmorFactory : ASingleton<ArmorFactory>
    {
        private ArmorBuilder _armorBuilder;

        public ArmorFactory() { this._armorBuilder = new ArmorBuilder(); }

        public CArmor CreateNewObject(string name, EEquipmentTier tier)
        {
            var armor = this._armorBuilder.Build(name + "_" + tier.ToString());
            armor.Model.Data.Name = name;
            return armor;
        }
    }
}
