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
                default: { return null; }
            }
        } 
    }
}
