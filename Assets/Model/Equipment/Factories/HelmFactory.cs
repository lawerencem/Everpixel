using Generics;
using Model.Equipment;

namespace Assets.Model.Equipment.Factories
{
    public class HelmFactory : AbstractSingleton<HelmFactory>
    {
        private HelmBuilder _helmBuilder;

        public HelmFactory() { this._helmBuilder = new HelmBuilder(); }

        public GenericHelm CreateNewObject(string name, EquipmentTierEnum tier)
        {
            var helm = this._helmBuilder.Build(name + "_" + tier.ToString());
            helm.Name = name;
            return helm;
        }
    }
}
