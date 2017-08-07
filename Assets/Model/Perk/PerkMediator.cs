using Assets.Model.Character;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Perk
{
    public class PerkMediator: ASingleton<PerkMediator>
    {
        public void SetCharacterPerks(MChar c, List<EPerk> perks)
        {
            foreach(var perk in perks)
            {
                try
                {
                    var gPerk = PerkFactory.Instance.CreateNewObject(perk);
                    gPerk.Init(c);

                    switch (gPerk.Type)
                    {
                        //case (EPerk.Barbarism): { c.Perks.PostHitPerks.Add(gPerk as Barbarism); } break;
                        //case (EPerk.Bash_Buddy): { c.Perks.OnActionPerks.Add(gPerk as BashBuddy); } break;
                        //case (EPerk.Bastion): { c.Perks.PostHitPerks.Add(gPerk as Bastion); } break;
                        //case (EPerk.Bully): { c.Perks.PreHitPerks.Add(gPerk as Bully); } break;
                        //case (EPerk.Colossus): { c.Perks.EquipmentSStatPerks.Add(gPerk as Colossus); } break;
                        //case (EPerk.Dino_Bite): { c.Perks.AbilityModPerks.Add(gPerk as DinoBite); } break;
                        //case (EPerk.Enrage): { c.Perks.WhenHitPerks.Add(gPerk as Enrage); } break;
                        //case (EPerk.Executioner): { c.Perks.PreHitPerks.Add(gPerk as Executioner); } break;
                        //case (EPerk.Gargantuan): { c.Perks.SStatModPerks.Add(gPerk as Gargantuan); } break;
                        //case (EPerk.Hulk): { c.Perks.EquipmentSStatPerks.Add(gPerk as Hulk); } break;
                        //case (EPerk.Iron_Hide): { c.Perks.SStatModPerks.Add(gPerk as IronHide); } break;
                        //case (EPerk.Massive): { c.Perks.SStatModPerks.Add(gPerk as Massive); } break;
                        //case (EPerk.Medium_Shield_Expert): { c.Perks.PostHitPerks.Add(gPerk as MediumShieldExpert); } break;
                        //case (EPerk.Predator): { c.Perks.PreHitPerks.Add(gPerk as Predator); } break;
                        //case (EPerk.Savage_Soul): { c.Perks.WhenHitPerks.Add(gPerk as SavageSoul); } break;
                        //case (EPerk.Savage_Visage): { c.Perks.PostHitPerks.Add(gPerk as SavageVisage); } break;
                        //case (EPerk.Scaly): { c.Perks.SStatModPerks.Add(gPerk as Scaly); } break;
                        //case (EPerk.Shield_Brother): { c.Perks.PreHitPerks.Add(gPerk as ShieldBrother); } break;
                        //case (EPerk.Shield_Happy): { c.Perks.PreHitPerks.Add(gPerk as ShieldHappy); } break;
                        //case (EPerk.Shield_Pro): { c.Perks.SStatModPerks.Add(gPerk as ShieldPro); } break;
                        //case (EPerk.Small_Shield_Expert): { c.Perks.PostHitPerks.Add(gPerk as SmallShieldExpert); } break;
                        //case (EPerk.Squishy): { c.Perks.WhenHitPerks.Add(gPerk as Squishy); } break;
                        //case (EPerk.Weightlifter): { c.Perks.EquipmentSStatPerks.Add(gPerk as Weightlifter); } break;
                        //case (EPerk.Violence_Fetish): { c.Perks.PostHitPerks.Add(gPerk as ViolenceFetish); } break;
                        //case (EPerk.T_Rex_Bite): { c.Perks.AbilityModPerks.Add(gPerk as TRexBite); } break;
                        //case (EPerk.Two_Handed_Expert): { c.Perks.EquipmentPerks.Add(gPerk as TwoHandedExpert); } break;
                    }
                }
                catch(KeyNotFoundException e)
                {

                }
            }
        }
    }
}
