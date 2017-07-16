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
                case (PerkEnum.Barbarism): { return new Barbarism(); }
                case (PerkEnum.Bash_Buddy): { return new BashBuddy(); }
                case (PerkEnum.Colossus): { return new Colossus(); }
                case (PerkEnum.Dino_Bite): { return new DinoBite(); }
                case (PerkEnum.Enrage): { return new Enrage(); }
                case (PerkEnum.Executioner): { return new Executioner(); }
                case (PerkEnum.Hulk): { return new Hulk(); }
                case (PerkEnum.Gargantuan): { return new Gargantuan(); }
                case (PerkEnum.Iron_Hide): { return new IronHide(); }
                case (PerkEnum.Massive): { return new Massive(); }
                case (PerkEnum.Predator): { return new Predator(); }
                case (PerkEnum.Savage_Soul): { return new SavageSoul(); }
                case (PerkEnum.Savage_Visage): { return new SavageVisage(); }
                case (PerkEnum.Scaly): { return new Scaly(); }
                case (PerkEnum.Shield_Happy): { return new ShieldHappy(); }
                case (PerkEnum.Squishy): { return new Squishy(); }
                case (PerkEnum.Weightlifter): { return new Weightlifter(); }
                case (PerkEnum.Violence_Fetish): { return new ViolenceFetish(); }
                case (PerkEnum.T_Rex_Bite): { return new TRexBite(); }
                default: { return null; }
            }
        } 
    }
}
