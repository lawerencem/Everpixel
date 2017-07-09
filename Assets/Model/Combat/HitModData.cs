namespace Model.Combat
{
    public class HitModData
    {
        public double BaseDamage { get; set; }
        public double BlockMod { get; set; }

        public HitModData()
        {
            this.BaseDamage = 0;
            this.BlockMod = 1;
        }

        public void Reset()
        {
            this.BaseDamage = 0;
        }
    }
}
