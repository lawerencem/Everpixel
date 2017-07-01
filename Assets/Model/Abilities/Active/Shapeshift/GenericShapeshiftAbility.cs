namespace Model.Abilities.Shapeshift
{
    public class GenericShapeshiftAbility : GenericAbility
    {
        public ShapeshiftInfo Info { get; set; }

        public GenericShapeshiftAbility(AbilitiesEnum type) : base(type)
        {
            this.Info = new ShapeshiftInfo();
        }
    }
}
