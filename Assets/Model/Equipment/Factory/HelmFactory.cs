using Assets.Model.Equipment.Builder;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Type;
using Generics;

namespace Assets.Model.Equipment.Factory
{
    public class HelmFactory : AbstractSingleton<HelmFactory>
    {
        private HelmBuilder _helmBuilder;

        public HelmFactory() { this._helmBuilder = new HelmBuilder(); }

        public MHelm CreateNewObject(string name, EEquipmentTier tier)
        {
            var helm = this._helmBuilder.Build(name + "_" + tier.ToString());
            helm.Name = name;
            return helm;
        }
    }
}
