namespace Model.Perks
{
    public class GenericAbilityModData
    {
        public double BaseDamage { get; set; }

        public GenericAbilityModData()
        {
            this.BaseDamage = 0;
        }

        public void Reset()
        {
            this.BaseDamage = 0;
        }
    }
}
