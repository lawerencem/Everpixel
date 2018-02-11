namespace Assets.Model.Ability.Logic.Calculator
{
    public class AbilityCalcContainer
    {
        public BlockCalculator BlockCalc { get; set; }
        public CritCalculator CritCalc { get; set; }
        public DamageCalculator DmgCalc { get; set; }
        public DodgeCalculator DodgeCalc { get; set; }
        public HeadShotCalculator HeadShotCalc { get; set; }
        public ParryCalculator ParryCalc { get; set; }
        public ResistCalculator ResistCalc { get; set; }

        public AbilityCalcContainer()
        {
            this.BlockCalc = new BlockCalculator();
            this.CritCalc = new CritCalculator();
            this.DmgCalc = new DamageCalculator();
            this.DodgeCalc = new DodgeCalculator();
            this.HeadShotCalc = new HeadShotCalculator();
            this.ParryCalc = new ParryCalculator();
            this.ResistCalc = new ResistCalculator();
        }
    }
}
