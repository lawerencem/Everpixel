namespace Assets.Model.Combat
{
    public class ChancePrediction
    {
        public double Block { get; set; }
        public double Crit { get; set; }
        public double Damage { get; set; }
        public double Dodge { get; set; }
        public double Parry { get; set; }
        public double Resist { get; set; }

        public ChancePrediction()
        {
            this.Block = 1;
            this.Crit = 1;
            this.Damage = 1;
            this.Dodge = 1;
            this.Parry = 1;
            this.Resist = 1;
        }
    }
}
