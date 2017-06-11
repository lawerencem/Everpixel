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
                var genericPerk = PerkFactory.Instance.CreateNewObject(perk);

                switch(genericPerk.Type)
                {
                    case (PerkEnum.Colossus): { c.Perks.EquipmentSStatPerks.Add(genericPerk as Colossus); } break;
                    case (PerkEnum.Dino_Bite): { c.Perks.AbilityModPerks.Add(genericPerk as DinoBite); } break;
                    case (PerkEnum.Hulk): { c.Perks.EquipmentSStatPerks.Add(genericPerk as Hulk); } break;
                    case (PerkEnum.Iron_Hide): { c.Perks.SStatModPerks.Add(genericPerk as IronHide); } break;
                    case (PerkEnum.Scaly): { c.Perks.SStatModPerks.Add(genericPerk as Scaly); } break;
                    case (PerkEnum.Weightlifter): { c.Perks.EquipmentSStatPerks.Add(genericPerk as Weightlifter);} break;
                    case (PerkEnum.T_Rex_Bite): { c.Perks.AbilityModPerks.Add(genericPerk as TRexBite); } break;
                }
            }
        }
    }
}
