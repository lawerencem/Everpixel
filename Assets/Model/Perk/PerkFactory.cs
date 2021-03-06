﻿using Assets.Model.Perk.AbilityMod;
using Assets.Model.Perk.Equipment;
using Assets.Model.Perk.EquipmentSStat;
using Assets.Model.Perk.OnAction;
using Assets.Model.Perk.PostHit;
using Assets.Model.Perk.PreHit;
using Assets.Model.Perk.SStatMod;
using Assets.Model.Perk.WhenHit;
using Assets.Template.Other;

namespace Assets.Model.Perk
{
    public class PerkFactory : ASingleton<PerkFactory>
    {
        public PerkFactory() { }

        public MPerk CreateNewObject(EPerk toCreate)
        {
            switch(toCreate)
            {
                case (EPerk.Barbarism): { return new Barbarism(); }
                case (EPerk.Bash_Buddy): { return new BashBuddy(); }
                case (EPerk.Bastion): { return new Bastion(); }
                case (EPerk.Bully): { return new Bully(); }
                case (EPerk.Colossus): { return new Colossus(); }
                case (EPerk.Enrage): { return new Enrage(); }
                case (EPerk.Executioner): { return new Executioner(); }
                case (EPerk.Hulk): { return new Hulk(); }
                case (EPerk.Gargantuan): { return new Gargantuan(); }
                case (EPerk.Iron_Hide): { return new IronHide(); }
                case (EPerk.Massive): { return new Massive(); }
                case (EPerk.Medium_Shield_Expert): { return new MediumShieldExpert(); }
                case (EPerk.Predator): { return new Predator(); }
                case (EPerk.Savage_Soul): { return new SavageSoul(); }
                case (EPerk.Savage_Visage): { return new SavageVisage(); }
                case (EPerk.Scaly): { return new Scaly(); }
                case (EPerk.Shield_Brother): { return new ShieldBrother(); }
                case (EPerk.Shield_Happy): { return new ShieldHappy(); }
                case (EPerk.Shield_Pro): { return new ShieldPro(); }
                case (EPerk.Small_Shield_Expert): { return new SmallShieldExpert(); }
                case (EPerk.Squishy): { return new Squishy(); }
                case (EPerk.Weightlifter): { return new Weightlifter(); }
                case (EPerk.Violence_Fetish): { return new ViolenceFetish(); }
                case (EPerk.Two_Handed_Expert): { return new TwoHandedExpert(); }
                default: { return null; }
            }
        } 
    }
}
