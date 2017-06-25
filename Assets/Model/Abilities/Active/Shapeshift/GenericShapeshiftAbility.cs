namespace Model.Abilities.Shapeshift
{
    public class GenericShapeshiftAbility : GenericActiveAbility
    {
        public ShapeshiftInfo Info { get; set; }

        public GenericShapeshiftAbility(ActiveAbilitiesEnum type) : base(type)
        {
            this.Info = new ShapeshiftInfo();
        }
    }
}
