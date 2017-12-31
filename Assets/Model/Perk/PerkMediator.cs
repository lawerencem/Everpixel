using Assets.Model.Character;
using Assets.Model.Perk.Equipment;
using Assets.Model.Perk.EquipmentSStat;
using Assets.Model.Perk.OnAction;
using Assets.Model.Perk.PostHit;
using Assets.Model.Perk.PreHit;
using Assets.Model.Perk.SStatMod;
using Assets.Model.Perk.WhenHit;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk
{
    public class PerkMediator: ASingleton<PerkMediator>
    {
        public void SetCharacterPerks(MChar c, List<EPerk> perks)
        {
            foreach (var proto in perks)
            {
                var perk = PerkFactory.Instance.CreateNewObject(proto);
                if (perk != null)
                {
                    switch (perk.Type)
                    {
                        case (EPerk.Barbarism): { c.AddPerk(perk as Barbarism); } break;
                        case (EPerk.Bash_Buddy): { c.AddPerk(perk as BashBuddy); } break;
                        case (EPerk.Bastion): { c.AddPerk(perk as Bastion); } break;
                        case (EPerk.Bully): { c.AddPerk(perk as Bully); } break;
                        case (EPerk.Colossus): { c.AddPerk(perk as Colossus); } break;
                        case (EPerk.Enrage): { c.AddPerk(perk as Enrage); } break;
                        case (EPerk.Executioner): { c.AddPerk(perk as Executioner); } break;
                        case (EPerk.Gargantuan): { c.AddPerk(perk as Gargantuan); } break;
                        case (EPerk.Hulk): { c.AddPerk(perk as Hulk); } break;
                        case (EPerk.Iron_Hide): { c.AddPerk(perk as IronHide); } break;
                        case (EPerk.Massive): { c.AddPerk(perk as Massive); } break;
                        case (EPerk.Medium_Shield_Expert): { c.AddPerk(perk as MediumShieldExpert); } break;
                        case (EPerk.Predator): { c.AddPerk(perk as Predator); } break;
                        case (EPerk.Savage_Soul): { c.AddPerk(perk as SavageSoul); } break;
                        case (EPerk.Savage_Visage): { c.AddPerk(perk as SavageVisage); } break;
                        case (EPerk.Scaly): { c.AddPerk(perk as Scaly); } break;
                        case (EPerk.Shield_Brother): { c.AddPerk(perk as ShieldBrother); } break;
                        case (EPerk.Shield_Happy): { c.AddPerk(perk as ShieldHappy); } break;
                        case (EPerk.Shield_Pro): { c.AddPerk(perk as ShieldPro); } break;
                        case (EPerk.Small_Shield_Expert): { c.AddPerk(perk as SmallShieldExpert); } break;
                        case (EPerk.Squishy): { c.AddPerk(perk as Squishy); } break;
                        case (EPerk.Weightlifter): { c.AddPerk(perk as Weightlifter); } break;
                        case (EPerk.Violence_Fetish): { c.AddPerk(perk as ViolenceFetish); } break;
                        case (EPerk.Two_Handed_Expert): { c.AddPerk(perk as TwoHandedExpert); } break;
                    }
                }
            }
        }
    }
}
