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
                case (PerkEnum.Bash_Buddy): { return new BashBuddy(); }
                case (PerkEnum.Colossus): { return new Colossus(); }
                case (PerkEnum.Dino_Bite): { return new DinoBite(); }
                case (PerkEnum.Hulk): { return new Hulk(); }
                case (PerkEnum.Gargantuan): { return new Gargantuan(); }
                case (PerkEnum.Iron_Hide): { return new IronHide(); }
                case (PerkEnum.Massive): { return new Massive(); }
                case (PerkEnum.Scaly): { return new Scaly(); }
                case (PerkEnum.Squishy): { return new Squishy(); }
                case (PerkEnum.Weightlifter): { return new Weightlifter(); }
                case (PerkEnum.T_Rex_Bite): { return new TRexBite(); }
                default: { return null; }
            }
        } 
    }
}
