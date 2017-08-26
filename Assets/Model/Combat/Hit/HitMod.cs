namespace Assets.Model.Combat.Hit
{
    public class HitMod
    {
        public double BaseDamage { get; set; }
        public double BlockMod { get; set; }

        public HitMod()
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
