using Generics;

namespace Model.Perks
{
    public class PerkFactory : AbstractSingleton<PerkFactory>
    {
        public PerkFactory() { }

        public GenericPerk CreateNewObject(PerkEnum toCreate)
        {
            switch(toCreate)
            {
                case (PerkEnum.Colossus): { return new Colossus(); }
                case (PerkEnum.Weightlifter): { return new Weightlifter(); }
                case (PerkEnum.T_Rex_Bite): { return new TRexBite(); }
                default: { return null; }
            }
        } 
    }
}
