using Assets.Model.Character.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class MEquipmentSStatPerk : MPerk
    {
        public MEquipmentSStatPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetEquipmentSStatPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetEquipmentSStatPerks().Add(this);
        }

        public virtual void TryModEquipmentMod(Pair<object, List<StatMod>> mods)
        {

        }
    }
}
