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
            //var abilityModPerks = c.GetPerks().GetAbilityModPerks();
            var equipmentPerks = c.GetPerks().GetEquipmentPerks();
            var equipmentSStatPerks = c.GetPerks().GetEquipmentSStatPerks();
            var onActionPerks = c.GetPerks().GetOnActionPerks();
            var postHitPerks = c.GetPerks().GetPostHitPerks();
            var preHitPerks = c.GetPerks().GetPreHitPerks();
            var sStatModPerks = c.GetPerks().GetSStatModPerks();
            var whenHitPerks = c.GetPerks().GetWhenHitPerks();

            foreach (var perk in perks)
            {
                var gPerk = PerkFactory.Instance.CreateNewObject(perk);
                if (gPerk != null)
                {
                    gPerk.Init(c);

                    switch (gPerk.Type)
                    {
                        case (EPerk.Barbarism): { postHitPerks.Add(gPerk as Barbarism); } break;
                        case (EPerk.Bash_Buddy): { onActionPerks.Add(gPerk as BashBuddy); } break;
                        case (EPerk.Bastion): { postHitPerks.Add(gPerk as Bastion); } break;
                        case (EPerk.Bully): { preHitPerks.Add(gPerk as Bully); } break;
                        case (EPerk.Colossus): { equipmentSStatPerks.Add(gPerk as Colossus); } break;
                        case (EPerk.Enrage): { whenHitPerks.Add(gPerk as Enrage); } break;
                        case (EPerk.Executioner): { preHitPerks.Add(gPerk as Executioner); } break;
                        case (EPerk.Gargantuan): { sStatModPerks.Add(gPerk as Gargantuan); } break;
                        case (EPerk.Hulk): { equipmentSStatPerks.Add(gPerk as Hulk); } break;
                        case (EPerk.Iron_Hide): { sStatModPerks.Add(gPerk as IronHide); } break;
                        case (EPerk.Massive): { sStatModPerks.Add(gPerk as Massive); } break;
                        case (EPerk.Medium_Shield_Expert): { postHitPerks.Add(gPerk as MediumShieldExpert); } break;
                        case (EPerk.Predator): { preHitPerks.Add(gPerk as Predator); } break;
                        case (EPerk.Savage_Soul): { whenHitPerks.Add(gPerk as SavageSoul); } break;
                        case (EPerk.Savage_Visage): { postHitPerks.Add(gPerk as SavageVisage); } break;
                        case (EPerk.Scaly): { sStatModPerks.Add(gPerk as Scaly); } break;
                        case (EPerk.Shield_Brother): { preHitPerks.Add(gPerk as ShieldBrother); } break;
                        case (EPerk.Shield_Happy): { preHitPerks.Add(gPerk as ShieldHappy); } break;
                        case (EPerk.Shield_Pro): { sStatModPerks.Add(gPerk as ShieldPro); } break;
                        case (EPerk.Small_Shield_Expert): { postHitPerks.Add(gPerk as SmallShieldExpert); } break;
                        case (EPerk.Squishy): { whenHitPerks.Add(gPerk as Squishy); } break;
                        case (EPerk.Weightlifter): { equipmentSStatPerks.Add(gPerk as Weightlifter); } break;
                        case (EPerk.Violence_Fetish): { postHitPerks.Add(gPerk as ViolenceFetish); } break;
                        case (EPerk.Two_Handed_Expert): { equipmentPerks.Add(gPerk as TwoHandedExpert); } break;
                    }
                }
            }
        }
    }
}
