namespace Assets.Model.Character.Param
{
    public class PrimaryStats
    {
        public int Agility { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Might { get; set; }
        public int Perception { get; set; }
        public int Resolve { get; set; }

        public PrimaryStats(int agi, int cons, int intel, int might, int per, int res)
        {
            this.Agility = agi;
            this.Constitution = cons;
            this.Intelligence = intel;
            this.Might = might;
            this.Perception = per;
            this.Resolve = res;
        }

        public PrimaryStats()
        {
            this.Agility = 0;
            this.Constitution = 0;
            this.Intelligence = 0;
            this.Might = 0;
            this.Perception = 0;
            this.Resolve = 0;
        }

        public PrimaryStats Clone()
        {
            var stats = new PrimaryStats();
            stats.Agility = this.Agility;
            stats.Constitution = this.Agility;
            stats.Intelligence = this.Intelligence;
            stats.Might = this.Might;
            stats.Perception = this.Perception;
            stats.Resolve = this.Resolve;
            return stats;
        }
    }
}

