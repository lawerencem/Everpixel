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
                    case (PerkEnum.Weightlifter): { c.Perks.EquipmentSStatPerks.Add(genericPerk as Weightlifter);} break;
                }
            }
        }
    }
}
