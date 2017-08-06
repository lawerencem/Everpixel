using Assets.Model.Ability;
using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability.Shapeshift
{
    public class Shapeshift : MAbility
    {
        public ShapeshiftInfo Info { get; set; }

        public Shapeshift(EAbility type) : base(type)
        {
            this.Info = new ShapeshiftInfo();
        }
    }
}
