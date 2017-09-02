using Assets.Model.Equipment.Builder;
using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Type;
using Assets.Template.Other;

namespace Assets.Model.Equipment.Factory
{
    public class HelmFactory : ASingleton<HelmFactory>
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
