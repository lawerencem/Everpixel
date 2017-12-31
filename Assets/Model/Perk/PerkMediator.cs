using Assets.Model.Character;
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
                    perk.Init();
                    c.AddPerk(perk);
                }
            }
        }
    }
}
