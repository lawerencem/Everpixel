using Generics;
using Model.Characters;
using System.Collections.Generic;

namespace Model.Perks
{
    public class PerkMediator: AbstractSingleton<PerkMediator>
    {
        public void SetCharacterPerks(GenericCharacter c, List<PerkEnum> perks)
        {
            foreach(var perk in perks)
            {
                try
                {
                    var gPerk = PerkFactory.Instance.CreateNewObject(perk);
                    gPerk.Init(c);

                    switch (gPerk.Type)
                    {
                        case (PerkEnum.Barbarism): { c.Perks.PostHitPerks.Add(gPerk as Barbarism); } break;
                        case (PerkEnum.Bash_Buddy): { c.Perks.OnActionPerks.Add(gPerk as BashBuddy); } break;
                        case (PerkEnum.Bastion): { c.Perks.PostHitPerks.Add(gPerk as Bastion); } break;
                        case (PerkEnum.Colossus): { c.Perks.EquipmentSStatPerks.Add(gPerk as Colossus); } break;
                        case (PerkEnum.Dino_Bite): { c.Perks.AbilityModPerks.Add(gPerk as DinoBite); } break;
                        case (PerkEnum.Enrage): { c.Perks.WhenHitPerks.Add(gPerk as Enrage); } break;
                        case (PerkEnum.Executioner): { c.Perks.PreHitPerks.Add(gPerk as Executioner); } break;
                        case (PerkEnum.Gargantuan): { c.Perks.SStatModPerks.Add(gPerk as Gargantuan); } break;
                        case (PerkEnum.Hulk): { c.Perks.EquipmentSStatPerks.Add(gPerk as Hulk); } break;
                        case (PerkEnum.Iron_Hide): { c.Perks.SStatModPerks.Add(gPerk as IronHide); } break;
                        case (PerkEnum.Massive): { c.Perks.SStatModPerks.Add(gPerk as Massive); } break;
                        case (PerkEnum.Medium_Shield_Expert): { c.Perks.PostHitPerks.Add(gPerk as MediumShieldExpert); } break;
                        case (PerkEnum.Predator): { c.Perks.PreHitPerks.Add(gPerk as Predator); } break;
                        case (PerkEnum.Savage_Soul): { c.Perks.WhenHitPerks.Add(gPerk as SavageSoul); } break;
                        case (PerkEnum.Savage_Visage): { c.Perks.PostHitPerks.Add(gPerk as SavageVisage); } break;
                        case (PerkEnum.Scaly): { c.Perks.SStatModPerks.Add(gPerk as Scaly); } break;
                        case (PerkEnum.Shield_Happy): { c.Perks.PreHitPerks.Add(gPerk as ShieldHappy); } break;
                        case (PerkEnum.Shield_Pro): { c.Perks.SStatModPerks.Add(gPerk as ShieldPro); } break;
                        case (PerkEnum.Small_Shield_Expert): { c.Perks.PostHitPerks.Add(gPerk as SmallShieldExpert); } break;
                        case (PerkEnum.Squishy): { c.Perks.WhenHitPerks.Add(gPerk as Squishy); } break;
                        case (PerkEnum.Weightlifter): { c.Perks.EquipmentSStatPerks.Add(gPerk as Weightlifter); } break;
                        case (PerkEnum.Violence_Fetish): { c.Perks.PostHitPerks.Add(gPerk as ViolenceFetish); } break;
                        case (PerkEnum.T_Rex_Bite): { c.Perks.AbilityModPerks.Add(gPerk as TRexBite); } break;
                    }
                }
                catch(KeyNotFoundException e)
                {

                }
            }
        }
    }
}
