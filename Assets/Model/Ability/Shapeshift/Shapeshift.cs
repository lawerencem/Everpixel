using Assets.Model.Ability;
using Assets.Model.Ability.Enum;

namespace Model.Abilities.Shapeshift
{
    public class Shapeshift : Ability
    {
        public ShapeshiftInfo Info { get; set; }

        public Shapeshift(EnumAbility type) : base(type)
        {
            this.Info = new ShapeshiftInfo();
        }
    }
}
