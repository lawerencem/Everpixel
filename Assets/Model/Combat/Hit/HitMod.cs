namespace Assets.Model.Combat.Hit
{
    public class HitMod
    {
        public double BaseDamage { get; set; }
        public double SrcArmorIgnoreMod { get; set; }
        public double SrcArmorPierceMod { get; set; }
        public double TgtDodgeMod { get; set; }

        public HitMod()
        {
            this.BaseDamage = 0;
            this.SrcArmorPierceMod = 1;
            this.SrcArmorIgnoreMod = 1;
            this.TgtDodgeMod = 1;
        }

        public void Reset()
        {
            this.BaseDamage = 0;
        }
    }
}
